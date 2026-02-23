using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using NModbus;
using OJT1_Smart_IO.Managers;
using OJT1_Smart_IO.Models;
using OJT1_Smart_IO.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;

namespace OJT1_Smart_IO
{
    public partial class Form1 : Form
    {
        private readonly ModbusTcpService _modbus;
        private readonly ModuleManager _moduleManager;
        //  Apply 전(설정모드) UI 편집용 리스트
        private readonly BindingList<IOModule> _uiModulesBinding = new BindingList<IOModule>();
        //  Channels 바인딩
        private BindingList<IOChannel> _channelsBinding = new BindingList<IOChannel>();
        private const int DefaultPort = 502;
        private const byte DefaultUnitId = 1;
        private readonly SmartIOService _smart;
        private bool _disconnectNotified = false;
        private bool _configLocked = false; // false=설정모드(UI편집), true=운전모드(Apply됨)

        // Drag & Drop
        private int _dragRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        private bool _isDragging = false;

        private bool _m = false;
        private int _DI = -1;
        private int _DO = 1;
        private int _DITotalChannels = 0;
        private int _DOTotalChannels = 0;

        public Form1()
        {
            InitializeComponent();

            // ---- 서비스/매니저 ----
            _modbus = new ModbusTcpService();
            _moduleManager = new ModuleManager(_modbus)
            {
                SlaveId = DefaultUnitId,
                DoBaseAddress = 0
            };

            // ---- 바인딩 ----
            // ✅ 처음엔 UI 편집용 리스트를 보여줌
            gridModules.DataSource = _uiModulesBinding;
            gridChannels.DataSource = _channelsBinding;
            // ---- 기본 UI ----
            txtIp.Text = "192.168.0.132";
            spinPollMs.Value = 500;
            // TCP 통신 시간 설정
            TcpEditTime.EditValue = 2000; // 2초
            TcpEditTime.Properties.MinValue = 200;
            TcpEditTime.Properties.MaxValue = 10000; // 10초
            TcpEditTime.Properties.Increment = 100; // 100ms
            TcpEditTime.Properties.IsFloatValue = false;
            _modbus.TimeoutMs = (int)TcpEditTime.Value;
            // DI 기능을 위한 smartIOService 설정 초기화
            _smart = new SmartIOService(_modbus);
            // 연결 상태 UI
            _smart.ConnectionChanged += connected =>
            {
                if (!IsHandleCreated) return;
                BeginInvoke(new Action(() =>
                {
                    lblConn.Text = connected ? "Connected" : "Disconnected";
                    if (connected) _disconnectNotified = false;
                }));
            };
            // 에러 표시
            _smart.ErrorOccurred += msg =>
            {
                if (!IsHandleCreated) return;
                BeginInvoke(new Action(() =>
                {
                    if (_disconnectNotified) return;
                    _disconnectNotified = true;
                    lblConn.Text = "Disconnected";
                    MessageBox.Show("연결이 끊겼습니다.\n" + msg);
                }));
            };

            // DI 업데이트 -> UI 반영
            _smart.DIUpdated += di =>
            {
                if (IsHandleCreated)
                    BeginInvoke(new Action(() => ApplyDIToSelectedModule(di)));
            };
            gridModules.AllowDrop = true;
            // 오른쪽 비우기
            BindChannels(null);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetupChannelsGrid();
            // 콤보 기본 선택
            if (cmbModuleType.SelectedIndex < 0) cmbModuleType.SelectedIndex = 1; // 기본 DO
            // 시작은 비어있는 상태
            InitEmptyModules();
            // 그룹 패널 숨기기
            modules.OptionsView.ShowGroupPanel = false;
            channels.OptionsView.ShowGroupPanel = false;
            // 그룹핑 막기
            modules.OptionsCustomization.AllowGroup = false;
            channels.OptionsCustomization.AllowGroup = false;
            // 설정 전 까지 막기 
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = false;
            TcpEditTime.Enabled = false;
            // 시작은 설정모드(모듈 편집 가능, 채널 비활성)
            SetConfigLocked(false);
            LockModulesGridColumns();
        }
        private void SetupChannelsGrid()
        {
            var repoCheck = repositoryItemCheckEdit1;
            if (repoCheck == null)
            {
                repoCheck = new RepositoryItemCheckEdit();
                gridChannels.RepositoryItems.Add(repoCheck);
            }
            repoCheck.AllowGrayed = false;
            repoCheck.ValueChecked = true;
            repoCheck.ValueUnchecked = false;

            // Value 컬럼
            var colValue = channels.Columns.ColumnByFieldName("Value");
            if (colValue != null)
            {
                colValue.Caption = "VALUE";
                colValue.ColumnEdit = repoCheck;
                colValue.OptionsColumn.AllowEdit = true;
                colValue.OptionsColumn.ReadOnly = false;
            }

            var colDisp = channels.Columns.ColumnByFieldName("DisplayIndex");
            if (colDisp != null)
            {
                colDisp.Caption = "Index";
                colDisp.OptionsColumn.AllowEdit = false;
                colDisp.OptionsColumn.ReadOnly = true;
            }

            lblMode.Visible = false;

            channels.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            channels.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        // ---------------------------
        // ✅ UI/Runtime 모두 초기화
        // ---------------------------
        private void InitEmptyModules()
        {
            _moduleManager.ClearModules();
            _uiModulesBinding.Clear();

            modules.RefreshData();
            modules.FocusedRowHandle = -1; // 아무것도 안 가리키게
            BindChannels(null);
        }

        // ---------------------------
        // 모듈 선택 변경 => 오른쪽 채널 바인딩
        // ---------------------------
        private void modules_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var module = GetSelectedModule();
            BindChannels(module);
        }

        private IOModule GetSelectedModule()
        {
            return modules.GetFocusedRow() as IOModule;
        }

        private void BindChannels(IOModule module)
        {
            if (module == null)
            {
                _channelsBinding = new BindingList<IOChannel>();
                gridChannels.DataSource = _channelsBinding;
                lblMode.Visible = false;
                return;
            }
            _channelsBinding = new BindingList<IOChannel>(module.Channels);
            gridChannels.DataSource = _channelsBinding;
            bool isDI = module.Type == ModuleType.DI;
            lblMode.Visible = isDI;

            // DI는 read-only
            channels.OptionsBehavior.Editable = !isDI;
            channels.OptionsBehavior.ReadOnly = isDI;
        }

        // ---------------------------
        // Connect / Disconnect
        // ---------------------------
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {

                _disconnectNotified = false;

                string ip = txtIp.Text.Trim();
                if (string.IsNullOrWhiteSpace(ip))
                {
                    MessageBox.Show("IP를 입력하세요.");
                    return;
                }
                // DI 주소 정해주는 곳
                _modbus.TimeoutMs = (int)TcpEditTime.Value;     // 통신 타임아웃
                _smart.PollIntervalMs = (int)spinPollMs.Value;  // 폴링 주기
                _smart.DiStart = 0; // 항상 첫번째 
                _smart.DiCount = (ushort)(_DITotalChannels); // DI 읽기 주소 -> DiChannels
                if (_DI > 0)
                {
                    _smart.EnableDIPolling = true;
                }
                _smart.Disconnect();
                _smart.Connect(ip, DefaultPort, DefaultUnitId);
            }
            catch (Exception ex)
            {
                lblConn.Text = "Disconnected";
                Debug.WriteLine(ex);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _smart.Disconnect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Disconnect failed: {ex.Message}");
            }
            finally
            {
                lblConn.Text = "Disconnected";
            }
        }

        private void spinPollMs_EditValueChanged(object sender, EventArgs e)
        {
            // DI 폴링 주기 UI
        }

        private void txtIp_EditValueChanged(object sender, EventArgs e) { }

        private void TcpEditTime_EditValueChanged(object sender, EventArgs e)
        {
            _modbus.TimeoutMs = (int)TcpEditTime.Value;
        }

        // ---------------------------
        // ✅ 채널 체크 변경 => 운전모드(Apply)에서만 DO 쓰기
        // ---------------------------
        private void channels_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // ✅ Apply 전(설정모드)에는 장비 쓰기 금지
            if (!_configLocked) return;

            var module = GetSelectedModule();
            if (module == null) return;
            if (e.Column.FieldName != "Value") return;

            // DI는 막기
            if (module.Type == ModuleType.DI)
            {
                channels.RefreshRow(e.RowHandle);
                return;
            }
            //  RowHandle을 인덱스로 쓰지 말고 실제 Row 객체로 가져오기
            var ch = channels.GetRow(e.RowHandle) as IOChannel;
            if (ch == null) return;
            bool newValue = Convert.ToBoolean(e.Value);
            //if(module.DOIndex==0)module.DOIndex = 1;// DO 모듈이 하나도 없을 경우
            bool ok = _moduleManager.SetOutput(module.SlotIndex, ch.ChannelIndex,module.HistoryIndex, newValue);// DO부분 SlotIndex -> DOIndex -1
            if (!ok)
            {
                channels.RefreshRow(e.RowHandle);
                return;
            }
            ch.Value = newValue;
            channels.RefreshRow(e.RowHandle);
        }
        // ---------------------------
        // Add / Remove / Up / Down / Combo
        // (✅ Apply 전에는 UI 리스트만 수정)
        // ---------------------------
        private void cmbModuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // DI/DO 선택만, Add 버튼에서 읽어서 생성
        }
        private void btnAddModule_Click(object sender, EventArgs e)
        {
            try
            {
                if (_configLocked) return; // 운전모드면 추가 금지
                var type = (ModuleType)cmbModuleType.SelectedIndex; // DI=0, DO=1 
                if (cmbChannelsCount.SelectedIndex < 0)
                    throw new InvalidOperationException("채널 개수를 선택하세요.");
                int channelsCount = cmbChannelsCount.SelectedIndex+1;
                
                if (type == ModuleType.DI)
                {
                  //  _DI++;
                    _DITotalChannels += channelsCount;
                }
                else
                {
                   // _DO++;
                    _DOTotalChannels += channelsCount;
                }
                var m = new IOModule
                {
                    Type = type,
                    Channels = new System.Collections.Generic.List<IOChannel>()
                }; 
               
                addChannels(m, channelsCount);
                _uiModulesBinding.Add(m);
                // 슬롯/표시 인덱스 재계산(중요)
                RecalcUiIndexes();
                modules.RefreshData();
                modules.FocusedRowHandle = _uiModulesBinding.Count - 1;
                // ✅ Apply 전엔 화면만
                BindChannels(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveModule_Click(object sender, EventArgs e)
        {
            if (_configLocked) return;
            int idx = modules.FocusedRowHandle;
            if (idx < 0 || idx >= _uiModulesBinding.Count) return;
            var module = GetSelectedModule();
            if (module.Type == ModuleType.DO) _DO--;
            else _DI--;
            _uiModulesBinding.RemoveAt(idx);
            RecalcUiIndexes();
            modules.RefreshData();
            if (_uiModulesBinding.Count == 0) BindChannels(null);
            else
            {
                modules.FocusedRowHandle = Math.Min(idx, _uiModulesBinding.Count - 1);
                BindChannels(GetSelectedModule());
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (_configLocked) return;
            int from = modules.FocusedRowHandle;
            if (from <= 0) return;
            var item = _uiModulesBinding[from];
            _uiModulesBinding.RemoveAt(from);
            _uiModulesBinding.Insert(from - 1, item);
            RecalcUiIndexes();
            modules.RefreshData();
            modules.FocusedRowHandle = from - 1;
            BindChannels(GetSelectedModule());
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (_configLocked) return;
            int from = modules.FocusedRowHandle;
            if (from < 0 || from >= _uiModulesBinding.Count - 1) return;
            var item = _uiModulesBinding[from];
            _uiModulesBinding.RemoveAt(from);
            _uiModulesBinding.Insert(from + 1, item);
            RecalcUiIndexes();
            modules.RefreshData();
            modules.FocusedRowHandle = from + 1;
            BindChannels(GetSelectedModule());
        }
        private void RecalcUiIndexes() // slotIdex, DisplayIndex
        {
            int count = 1;
            int DICount = 1;
            for (int s = 0; s < _uiModulesBinding.Count; s++)
            {
                var m = _uiModulesBinding[s];
                m.SlotIndex = s;// 변경 ㄴㄴ
                for (int ch = 0; ch < m.Channels.Count; ch++)
                {                    
                    if (m.Type==ModuleType.DO)
                    {
                        if (ch == 0) m.HistoryIndex = count-1;                       
                        m.Channels[ch].DisplayIndex = count++;
                        continue;
                    }
                    if (ch == 0) m.HistoryIndex = DICount-1;
                    m.Channels[ch].DisplayIndex = DICount++;
                }
            }
        }
        private void btnApplyConfig_Click(object sender, EventArgs e)
        {
            if (!_configLocked)
            {
                // 설정모드 -> 운전모드
                ApplyUiToRuntimeMapping();
                SetConfigLocked(true);
            }
            else
            {
                SetConfigLocked(false);
            }
        }
        private void ApplyUiToRuntimeMapping()
        {
            // ✅ 런타임 모듈 새로 구성 (UI 리스트를 기준으로)
            _moduleManager.ClearModules();
            foreach (var ui in _uiModulesBinding)
            {
                _moduleManager.AddModule(ui);// Di,DO
            }
            modules.RefreshData();
            modules.FocusedRowHandle = _moduleManager.Modules.Count > 0 ? 0 : -1;
            BindChannels(GetSelectedModule());
        }
        private void SetConfigLocked(bool locked)
        {
            _configLocked = locked;
            // 모듈 편집 잠금/해제
            cmbModuleType.Enabled = !locked;
            btnAddModule.Enabled = !locked;
            btnRemoveModule.Enabled = !locked;
            btnUp.Enabled = !locked;
            btnDown.Enabled = !locked;
            // ✅ DI Polling Interval은 Apply 전(설정모드)에서만 변경 가능
            spinPollMs.Enabled = !locked;
            // 채널은 Apply(운전모드)일 때만 활성
            gridChannels.Enabled = locked;
            // 버튼 텍스트
            btnApplyConfig.Text = locked ? "Unlock Settings" : "Apply Settings";
            btnConnect.Enabled = locked;
            btnDisconnect.Enabled = locked;
            TcpEditTime.Enabled = locked;
            // 선택 모듈 기준으로 DI/DO 편집 가능/불가 재적용
            //BindChannels(GetSelectedModule());
        }
        private void ApplyDIToSelectedModule(bool[] diAll)
        {
            int cnt = 0;
            if (diAll == null || diAll.Length == 0) return;
            var list = _moduleManager.Modules;
            foreach (var m in list)
            {
                if (m == null) continue;
                if (m.Type != ModuleType.DI) continue;
                if (m.Channels == null || m.Channels.Count == 0) continue;
                for (int i = 0; i < m.Channels.Count; i++)
                {
                    m.Channels[i].Value = diAll[cnt++];
                }
            }
            // 현재 화면에 보이는 오른쪽 채널 그리드 갱신
            channels.RefreshData();
        }
        private void modules_MouseDown(object sender, MouseEventArgs e)
        {
            if (_configLocked) return;
            var hit = modules.CalcHitInfo(e.Location);
            if (!hit.InRow && !hit.InRowCell) { _dragRowHandle = -1; return; }
            _dragRowHandle = hit.RowHandle;
            _isDragging = false;
        }
        private void modules_MouseMove(object sender, MouseEventArgs e)
        {
            if (_configLocked) return;
            if (e.Button != MouseButtons.Left) return;
            if (_dragRowHandle < 0) return;
            if (!_isDragging)
            {
                _isDragging = true;
                gridModules.DoDragDrop(_dragRowHandle, DragDropEffects.Move);
            }
        }
        private void gridModules_DragOver(object sender, DragEventArgs e)
        {
            if (_configLocked)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (!e.Data.GetDataPresent(typeof(int)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            e.Effect = DragDropEffects.Move;
        }
        private void gridModules_DragDrop(object sender, DragEventArgs e)
        {
            if (_configLocked) return;
            if (!e.Data.GetDataPresent(typeof(int))) return;
            int fromHandle = (int)e.Data.GetData(typeof(int));
            if (fromHandle < 0) return;
            var pt = gridModules.PointToClient(new System.Drawing.Point(e.X, e.Y));
            var hit = modules.CalcHitInfo(pt);
            int toHandle = hit.RowHandle;
            if (toHandle < 0) return;
            // ✅ RowHandle -> DataSource index로 변환 (핵심)
            int fromIndex = modules.GetDataSourceRowIndex(fromHandle);
            int toIndex = modules.GetDataSourceRowIndex(toHandle);
            if (fromIndex < 0 || toIndex < 0) return;
            if (fromIndex == toIndex) return;
            var item = _uiModulesBinding[fromIndex];
            _uiModulesBinding.RemoveAt(fromIndex);

            if (toIndex > fromIndex) toIndex--;
            _uiModulesBinding.Insert(toIndex, item);

            RecalcUiIndexes();
            modules.RefreshData();
            modules.FocusedRowHandle = modules.GetRowHandle(toIndex); // 다시 화면 handle로 맞추기
            BindChannels(GetSelectedModule());
        }
        private void LockModulesGridColumns()
        {
            //그리드 전체 편집도 막아버리면 가장 안전 (드래그/선택은 가능)
            modules.OptionsBehavior.Editable = false;
            modules.OptionsBehavior.ReadOnly = true;
            //특정 컬럼만 확실히 잠금 (SlotIndex, Type)
            var colSlot = modules.Columns.ColumnByFieldName("SlotIndex"); // 변경 ㄴㄴ
            if (colSlot != null)
            {
                colSlot.OptionsColumn.AllowEdit = false;
                colSlot.OptionsColumn.ReadOnly = true;
            }
            var colType = modules.Columns.ColumnByFieldName("Type");
            if (colType != null)
            {
                colType.OptionsColumn.AllowEdit = false;
                colType.OptionsColumn.ReadOnly = true;
            }
            // (선택) 클릭해도 셀 편집 모드로 안 들어가게
            modules.OptionsSelection.EnableAppearanceFocusedCell = false;
        }
        public void addChannels(IOModule m,int ch)
        {
            for (int i = 0; i < ch; i++)// 나중에  채널 수 변경
            {
                m.Channels.Add(new IOChannel { ChannelIndex = i, Value = false });// DI,DO 나누어 주기
            }
           
        }
    }
}

