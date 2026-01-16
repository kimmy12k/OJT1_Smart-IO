using DevExpress.XtraGrid.Views.Grid;
using OJT1_Smart_IO.Managers;
using OJT1_Smart_IO.Models;
using OJT1_Smart_IO.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace OJT1_Smart_IO
{
    public partial class Form1 : Form
    {
        private readonly ModbusTcpService _modbus;
        private readonly SmartIOService _smart;
        private readonly ModuleManager _moduleManager;

        // grid 바인딩용
        private readonly BindingList<IOModule> _modulesBinding = new BindingList<IOModule>();
        private BindingList<IOChannel> _channelsBinding = new BindingList<IOChannel>();

        // 기본 통신 설정 (필요시 UI로 확장)
        private const int DefaultPort = 1502;
        private const byte DefaultUnitId = 1;

        public Form1()
        {
            InitializeComponent();


            cmbModuleType.Properties.Items.Clear();
            cmbModuleType.Properties.Items.AddRange(new object[] { "DI", "DO" });
            cmbModuleType.SelectedIndex = 0;
            cmbModuleType.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            cmbModuleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            // 1) Modbus 인스턴스 1개만 생성해서 공유
            _modbus = new ModbusTcpService();
            _smart = new SmartIOService(_modbus);
            _moduleManager = new ModuleManager(_modbus);

            // 2) 폴링 기본값
            spinPollMs.Value = 500;
            _smart.PollIntervalMs = 500;

            // 테스트 모드 기본 true/false는 상황에 맞게
            _smart.TestMode = false;

            // 3) 이벤트 연결
            _smart.ConnectionChanged += OnConnectionChanged;
            _smart.ErrorOccurred += msg => SafeUI(() => MessageBox.Show(msg));
            _smart.DIUpdated += OnDIUpdated;
            _smart.AIUpdated += _ => { /* 지금은 AI UI 없으니 보류 */ };

            // 4) Grid 바인딩
            gridModules.DataSource = _modulesBinding;
            gridChannels.DataSource = _channelsBinding;


            gridView2.OptionsBehavior.Editable = true;
            gridView2.OptionsBehavior.ReadOnly = false;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = true;



            // 채널은 선택된 모듈 기준으로 바인딩됨
            BindChannels(null);

            this.Load += Form1_Load;

            txtIp.Text = "127.0.0.1";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // gridView2.PopulateColumns();
            gridView2.BestFitColumns();

            var repoCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
            {
                AllowGrayed = false,
                ValueChecked = true,
                ValueUnchecked = false
            };
            gridChannels.RepositoryItems.Add(repoCheck);

            var colValue = gridView2.Columns.ColumnByFieldName(nameof(IOChannel.Value));
            if (colValue == null)
            {
                colValue = gridView2.Columns.AddField(nameof(IOChannel.Value));
                colValue.Visible = true;
            }

            colValue.ColumnEdit = repoCheck;

            colValue.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;

            colValue.Caption = "Value";
            colValue.OptionsColumn.AllowEdit = true;
            colValue.OptionsColumn.ReadOnly = false;

            gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        // ---------------------------
        // Connect / Disconnect
        // ---------------------------
        private void simpleButton1_Click(object sender, EventArgs e) // Connect
        {
            string ip = txtIp.Text.Trim();
            if (string.IsNullOrWhiteSpace(ip))
            {
                MessageBox.Show("IP를 입력하세요.");
                return;
            }

            // 폴링 설정 반영
            _smart.PollIntervalMs = (int)spinPollMs.Value;

            // ✅ 테스트 슬레이브가 Coil을 쓰고 있으니,
            // TestMode=false로 진짜 읽을 때는 DI를 ReadCoils로 맞추는게 가장 빠름.
            // (아래 'SmartIOService 수정' 섹션 참고)

            _smart.Connect(ip, DefaultPort, DefaultUnitId);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _smart.Disconnect();
        }

        private void OnConnectionChanged(bool connected)
        {
            SafeUI(() =>lblConn.Text = connected ? "Connected" : "Disconnected");
        }

        // ---------------------------
        // Poll Interval
        // ---------------------------
        private void spinPollMs_EditValueChanged(object sender, EventArgs e)
        {
            _smart.PollIntervalMs = (int)spinPollMs.Value;
        }

        // ---------------------------
        // Module Add/Remove/Up/Down
        // ---------------------------
        private void btnAddModule_Click(object sender, EventArgs e)
        {
            if (_moduleManager.Modules.Count >= 8)
            {
                MessageBox.Show("모듈은 최대 8개까지 추가할 수 있습니다.");
                return;
            }

            ModuleType type = (ModuleType)cmbModuleType.SelectedIndex; // DI=0, DO=1


            var module = new IOModule
            {
                SlotIndex = _moduleManager.Modules.Count,
                Type = type,
            };
          _moduleManager.Modules.Add(module);


            _modulesBinding.Add(module);



            // 새로 추가된 모듈로 포커스 이동
            gridView1.FocusedRowHandle = _modulesBinding.Count - 1;
        }

        private void btnRemoveModule_Click(object sender, EventArgs e)
        {
            var module = GetSelectedModule();
            if (module == null) return;

            int removeIndex = _moduleManager.Modules.IndexOf(module);
            if (removeIndex < 0) return;

            _moduleManager.Modules.RemoveAt(removeIndex);
            _modulesBinding.Remove(module);

            _moduleManager.ReindexSlots();
            RefreshModuleSlotIndexes();

            BindChannels(GetSelectedModule());
        }



        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveModule(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveModule(+1);
        }

        private void MoveModule(int delta)
        {
            var module = GetSelectedModule();
            if (module == null) return;

            int idx = _moduleManager.Modules.IndexOf(module);
            int target = idx + delta;

            if (idx < 0 || target < 0) return;
            if (target >= _moduleManager.Modules.Count) return;

            var list = _moduleManager.Modules;
            (list[idx], list[target]) = (list[target], list[idx]);

            (_modulesBinding[idx], _modulesBinding[target]) = (_modulesBinding[target], _modulesBinding[idx]);

            _moduleManager.ReindexSlots();
            RefreshModuleSlotIndexes();

            gridView1.FocusedRowHandle = target;
            BindChannels(GetSelectedModule());
        }

        private void RefreshModuleSlotIndexes()
        {
            gridView1.RefreshData();
        }
        // ---------------------------
        // Grid Selection
        // ---------------------------
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            BindChannels(GetSelectedModule());
        }

        private IOModule GetSelectedModule()
        {

            return gridView1.GetFocusedRow()as IOModule;
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

            // DI면 ReadOnly 안내
            
            
            bool isDI = module.Type == ModuleType.DI;
            lblMode.Visible = isDI;

            gridView2.OptionsBehavior.Editable = !isDI;
            gridView2.OptionsBehavior.ReadOnly = isDI;
          
        }


        
        // ---------------------------
        // DI Updated -> 모델 반영
        // ---------------------------
        private void OnDIUpdated(bool[] di)
        {
            // 구성된 DI 모듈들에 순서대로 채워넣기 (slot 순서 기준)
            SafeUI(() =>
            {
                int cursor = 0;

                foreach (var m in _moduleManager.Modules)
                {
                    if (m.Type != ModuleType.DI) continue;

                    for (int ch = 0; ch < m.Channels.Count && cursor < di.Length; ch++)
                    {
                        m.Channels[ch].Value = di[cursor++];
                    }
                }

                // 화면 갱신
                gridView2.RefreshData();
            });
        }

        // ---------------------------
        // Channel 값 변경 (DO 토글)
        // ---------------------------
        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var module = GetSelectedModule();
            if (module == null) return;

          

            // DO인 경우 Value 컬럼만 처리
            if (e.Column.FieldName != nameof(IOChannel.Value)) return;

            int row = e.RowHandle;
            if (row < 0 || row >= module.Channels.Count) return;
            // DI면 변경 막기
            if (module.Type == ModuleType.DI)
            {
                // 원래값으로 돌려놓기 위해 Refresh
                gridView2.RefreshRow(row);
                return;
            }

            bool newValue = Convert.ToBoolean(e.Value);

            bool ok = _moduleManager.SetOutput(module.SlotIndex, module.Channels[row].ChannelIndex, newValue);
            if (!ok)
            {
                // 실패 시 원래대로 되돌리기
                gridView2.RefreshRow(row);
            }
            else 
            {
                module.Channels[row].Value = newValue;
                gridView2.RefreshRow(row);
            }
           
        }

        // ---------------------------
        // 기타 (Send 버튼은 지금 당장 필요 없으면 비워도 됨)
        // ---------------------------
        private void btnSend_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Send는 현재 미사용. 필요하면 기능 정의 후 구현.");
        }

        // UI 스레드 안전 호출
        private void SafeUI(Action act)
        {
            if (IsDisposed) return;
            if (InvokeRequired) BeginInvoke(act);
            else act();
        }
    }
}
