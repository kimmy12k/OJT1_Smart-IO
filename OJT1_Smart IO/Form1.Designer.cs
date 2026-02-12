namespace OJT1_Smart_IO
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition1 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.ColumnDefinition columnDefinition2 = new DevExpress.XtraLayout.ColumnDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition1 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition2 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition3 = new DevExpress.XtraLayout.RowDefinition();
            DevExpress.XtraLayout.RowDefinition rowDefinition4 = new DevExpress.XtraLayout.RowDefinition();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            gridChannels = new DevExpress.XtraGrid.GridControl();
            channels = new DevExpress.XtraGrid.Views.Grid.GridView();
            DISPLAYINDEX = new DevExpress.XtraGrid.Columns.GridColumn();
            Value = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            btnApplyConfig = new DevExpress.XtraEditors.SimpleButton();
            lblMode = new DevExpress.XtraEditors.HyperlinkLabelControl();
            btnAddModule = new DevExpress.XtraEditors.SimpleButton();
            btnRemoveModule = new DevExpress.XtraEditors.SimpleButton();
            ms = new DevExpress.XtraEditors.LabelControl();
            btnUp = new DevExpress.XtraEditors.SimpleButton();
            btnDown = new DevExpress.XtraEditors.SimpleButton();
            ChannelsLabel = new DevExpress.XtraEditors.LabelControl();
            ModuleLabel = new DevExpress.XtraEditors.LabelControl();
            spinPollMs = new DevExpress.XtraEditors.SpinEdit();
            ModulesLabel = new DevExpress.XtraEditors.LabelControl();
            cmbModuleType = new DevExpress.XtraEditors.ComboBoxEdit();
            gridModules = new DevExpress.XtraGrid.GridControl();
            modules = new DevExpress.XtraGrid.Views.Grid.GridView();
            Index = new DevExpress.XtraGrid.Columns.GridColumn();
            Type = new DevExpress.XtraGrid.Columns.GridColumn();
            gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            pnlconn = new DevExpress.XtraEditors.PanelControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            TcpEditTime = new DevExpress.XtraEditors.SpinEdit();
            IPLabel = new DevExpress.XtraEditors.LabelControl();
            txtIp = new DevExpress.XtraEditors.TextEdit();
            btnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            btnConnect = new DevExpress.XtraEditors.SimpleButton();
            lblConn = new DevExpress.XtraEditors.LabelControl();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridChannels).BeginInit();
            ((System.ComponentModel.ISupportInitialize)channels).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spinPollMs.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbModuleType.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridModules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)modules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pnlconn).BeginInit();
            pnlconn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TcpEditTime.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtIp.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(gridChannels);
            layoutControl1.Controls.Add(panelControl1);
            layoutControl1.Controls.Add(gridModules);
            layoutControl1.Controls.Add(pnlconn);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(879, 183, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new System.Drawing.Size(938, 441);
            layoutControl1.TabIndex = 14;
            layoutControl1.Text = "layoutControl1";
            // 
            // gridChannels
            // 
            gridChannels.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            gridLevelNode1.RelationName = "Level1";
            gridChannels.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1 });
            gridChannels.Location = new System.Drawing.Point(471, 222);
            gridChannels.MainView = channels;
            gridChannels.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            gridChannels.Name = "gridChannels";
            gridChannels.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemCheckEdit1 });
            gridChannels.Size = new System.Drawing.Size(455, 137);
            gridChannels.TabIndex = 17;
            gridChannels.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { channels });
            // 
            // channels
            // 
            channels.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { DISPLAYINDEX, Value });
            channels.DetailHeight = 375;
            channels.GridControl = gridChannels;
            channels.Name = "channels";
            channels.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
            channels.OptionsSelection.EnableAppearanceFocusedCell = false;
            channels.CellValueChanging += channels_CellValueChanging;
            // 
            // DISPLAYINDEX
            // 
            DISPLAYINDEX.Caption = "DisplayIndex";
            DISPLAYINDEX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            DISPLAYINDEX.FieldName = "DisplayIndex";
            DISPLAYINDEX.MinWidth = 19;
            DISPLAYINDEX.Name = "DISPLAYINDEX";
            DISPLAYINDEX.Visible = true;
            DISPLAYINDEX.VisibleIndex = 0;
            DISPLAYINDEX.Width = 73;
            // 
            // Value
            // 
            Value.Caption = "VALUE";
            Value.ColumnEdit = repositoryItemCheckEdit1;
            Value.FieldName = "Value";
            Value.Name = "Value";
            Value.Visible = true;
            Value.VisibleIndex = 1;
            // 
            // repositoryItemCheckEdit1
            // 
            repositoryItemCheckEdit1.AutoHeight = false;
            repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(btnApplyConfig);
            panelControl1.Controls.Add(lblMode);
            panelControl1.Controls.Add(btnAddModule);
            panelControl1.Controls.Add(btnRemoveModule);
            panelControl1.Controls.Add(ms);
            panelControl1.Controls.Add(btnUp);
            panelControl1.Controls.Add(btnDown);
            panelControl1.Controls.Add(ChannelsLabel);
            panelControl1.Controls.Add(ModuleLabel);
            panelControl1.Controls.Add(spinPollMs);
            panelControl1.Controls.Add(ModulesLabel);
            panelControl1.Controls.Add(cmbModuleType);
            panelControl1.Location = new System.Drawing.Point(12, 122);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(914, 96);
            panelControl1.TabIndex = 18;
            // 
            // btnApplyConfig
            // 
            btnApplyConfig.Location = new System.Drawing.Point(604, 11);
            btnApplyConfig.Name = "btnApplyConfig";
            btnApplyConfig.Size = new System.Drawing.Size(113, 28);
            btnApplyConfig.TabIndex = 14;
            btnApplyConfig.Text = "Settings";
            btnApplyConfig.Click += btnApplyConfig_Click;
            // 
            // lblMode
            // 
            lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            lblMode.Appearance.Options.UseForeColor = true;
            lblMode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblMode.Location = new System.Drawing.Point(577, 67);
            lblMode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            lblMode.Name = "lblMode";
            lblMode.Size = new System.Drawing.Size(105, 29);
            lblMode.TabIndex = 19;
            lblMode.Text = "READ ONLY (DI)";
            lblMode.Visible = false;
            // 
            // btnAddModule
            // 
            btnAddModule.Location = new System.Drawing.Point(178, 11);
            btnAddModule.Margin = new System.Windows.Forms.Padding(2);
            btnAddModule.Name = "btnAddModule";
            btnAddModule.Size = new System.Drawing.Size(113, 28);
            btnAddModule.TabIndex = 19;
            btnAddModule.Text = "Add";
            btnAddModule.Click += btnAddModule_Click;
            // 
            // btnRemoveModule
            // 
            btnRemoveModule.Location = new System.Drawing.Point(307, 11);
            btnRemoveModule.Margin = new System.Windows.Forms.Padding(2);
            btnRemoveModule.Name = "btnRemoveModule";
            btnRemoveModule.Size = new System.Drawing.Size(113, 28);
            btnRemoveModule.TabIndex = 18;
            btnRemoveModule.Text = "Remove";
            btnRemoveModule.Click += btnRemoveModule_Click;
            // 
            // ms
            // 
            ms.Location = new System.Drawing.Point(468, 2);
            ms.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ms.Name = "ms";
            ms.Size = new System.Drawing.Size(95, 14);
            ms.TabIndex = 11;
            ms.Text = "DI Polling Interval";
            // 
            // btnUp
            // 
            btnUp.Location = new System.Drawing.Point(178, 54);
            btnUp.Margin = new System.Windows.Forms.Padding(2);
            btnUp.Name = "btnUp";
            btnUp.Size = new System.Drawing.Size(113, 28);
            btnUp.TabIndex = 17;
            btnUp.Text = "▲";
            btnUp.Click += btnUp_Click;
            // 
            // btnDown
            // 
            btnDown.Location = new System.Drawing.Point(307, 54);
            btnDown.Margin = new System.Windows.Forms.Padding(2);
            btnDown.Name = "btnDown";
            btnDown.Size = new System.Drawing.Size(113, 28);
            btnDown.TabIndex = 16;
            btnDown.Text = "▼";
            btnDown.Click += btnDown_Click;
            // 
            // ChannelsLabel
            // 
            ChannelsLabel.Location = new System.Drawing.Point(852, 77);
            ChannelsLabel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ChannelsLabel.Name = "ChannelsLabel";
            ChannelsLabel.Size = new System.Drawing.Size(48, 14);
            ChannelsLabel.TabIndex = 15;
            ChannelsLabel.Text = "Channels";
            // 
            // ModuleLabel
            // 
            ModuleLabel.Location = new System.Drawing.Point(46, 18);
            ModuleLabel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ModuleLabel.Name = "ModuleLabel";
            ModuleLabel.Size = new System.Drawing.Size(71, 14);
            ModuleLabel.TabIndex = 14;
            ModuleLabel.Text = "Module Type";
            // 
            // spinPollMs
            // 
            spinPollMs.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            spinPollMs.Location = new System.Drawing.Point(451, 19);
            spinPollMs.Margin = new System.Windows.Forms.Padding(2);
            spinPollMs.Name = "spinPollMs";
            spinPollMs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            spinPollMs.Size = new System.Drawing.Size(137, 20);
            spinPollMs.TabIndex = 5;
            // 
            // ModulesLabel
            // 
            ModulesLabel.Location = new System.Drawing.Point(451, 77);
            ModulesLabel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            ModulesLabel.Name = "ModulesLabel";
            ModulesLabel.Size = new System.Drawing.Size(48, 14);
            ModulesLabel.TabIndex = 13;
            ModulesLabel.Text = "Moudles ";
            // 
            // cmbModuleType
            // 
            cmbModuleType.Location = new System.Drawing.Point(21, 37);
            cmbModuleType.Margin = new System.Windows.Forms.Padding(2);
            cmbModuleType.Name = "cmbModuleType";
            cmbModuleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbModuleType.Properties.Items.AddRange(new object[] { "DI", "DO" });
            cmbModuleType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbModuleType.Size = new System.Drawing.Size(144, 20);
            cmbModuleType.TabIndex = 10;
            // 
            // gridModules
            // 
            gridModules.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            gridModules.Location = new System.Drawing.Point(12, 222);
            gridModules.MainView = modules;
            gridModules.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            gridModules.MinimumSize = new System.Drawing.Size(450, 120);
            gridModules.Name = "gridModules";
            gridModules.Size = new System.Drawing.Size(455, 137);
            gridModules.TabIndex = 17;
            gridModules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { modules, gridView3 });
            gridModules.DragDrop += gridModules_DragDrop;
            gridModules.DragOver += gridModules_DragOver;
            // 
            // modules
            // 
            modules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { Index, Type });
            modules.DetailHeight = 375;
            modules.GridControl = gridModules;
            modules.Name = "modules";
            modules.OptionsDetail.EnableMasterViewMode = false;
            modules.FocusedRowChanged += modules_FocusedRowChanged;
            modules.MouseDown += modules_MouseDown;
            modules.MouseMove += modules_MouseMove;
            // 
            // Index
            // 
            Index.AppearanceCell.Options.UseTextOptions = true;
            Index.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Index.Caption = "Slot Index";
            Index.FieldName = "SlotIndex";
            Index.Name = "Index";
            Index.Visible = true;
            Index.VisibleIndex = 0;
            // 
            // Type
            // 
            Type.AppearanceCell.Options.UseTextOptions = true;
            Type.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            Type.Caption = "Type";
            Type.FieldName = "Type";
            Type.Name = "Type";
            Type.Visible = true;
            Type.VisibleIndex = 1;
            // 
            // gridView3
            // 
            gridView3.DetailHeight = 375;
            gridView3.GridControl = gridModules;
            gridView3.Name = "gridView3";
            // 
            // pnlconn
            // 
            pnlconn.Controls.Add(labelControl1);
            pnlconn.Controls.Add(TcpEditTime);
            pnlconn.Controls.Add(IPLabel);
            pnlconn.Controls.Add(txtIp);
            pnlconn.Controls.Add(btnDisconnect);
            pnlconn.Controls.Add(btnConnect);
            pnlconn.Controls.Add(lblConn);
            pnlconn.FireScrollEventOnMouseWheel = true;
            pnlconn.Location = new System.Drawing.Point(12, 12);
            pnlconn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            pnlconn.Name = "pnlconn";
            pnlconn.Size = new System.Drawing.Size(914, 106);
            pnlconn.TabIndex = 17;
            // 
            // labelControl1
            // 
            labelControl1.Location = new System.Drawing.Point(414, 9);
            labelControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(72, 14);
            labelControl1.TabIndex = 13;
            labelControl1.Text = "TCP Timeout";
            // 
            // TcpEditTime
            // 
            TcpEditTime.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            TcpEditTime.Location = new System.Drawing.Point(405, 29);
            TcpEditTime.Name = "TcpEditTime";
            TcpEditTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            TcpEditTime.Size = new System.Drawing.Size(100, 20);
            TcpEditTime.TabIndex = 12;
            TcpEditTime.EditValueChanged += TcpEditTime_EditValueChanged;
            // 
            // IPLabel
            // 
            IPLabel.Location = new System.Drawing.Point(46, 10);
            IPLabel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            IPLabel.Name = "IPLabel";
            IPLabel.Size = new System.Drawing.Size(11, 14);
            IPLabel.TabIndex = 9;
            IPLabel.Text = "IP";
            // 
            // txtIp
            // 
            txtIp.Location = new System.Drawing.Point(21, 29);
            txtIp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            txtIp.Name = "txtIp";
            txtIp.Size = new System.Drawing.Size(144, 20);
            txtIp.TabIndex = 5;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new System.Drawing.Point(291, 29);
            btnDisconnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new System.Drawing.Size(82, 22);
            btnDisconnect.TabIndex = 8;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new System.Drawing.Point(197, 30);
            btnConnect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new System.Drawing.Size(81, 22);
            btnConnect.TabIndex = 6;
            btnConnect.Text = "connect";
            btnConnect.Click += btnConnect_Click;
            // 
            // lblConn
            // 
            lblConn.Location = new System.Drawing.Point(235, 10);
            lblConn.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            lblConn.Name = "lblConn";
            lblConn.Size = new System.Drawing.Size(74, 14);
            lblConn.TabIndex = 7;
            lblConn.Text = "Disconnected";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem1, layoutControlItem3, layoutControlItem4 });
            Root.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table;
            Root.Name = "Root";
            columnDefinition1.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition1.Width = 50D;
            columnDefinition2.SizeType = System.Windows.Forms.SizeType.Percent;
            columnDefinition2.Width = 50D;
            Root.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(new DevExpress.XtraLayout.ColumnDefinition[] { columnDefinition1, columnDefinition2 });
            rowDefinition1.Height = 110D;
            rowDefinition1.SizeType = System.Windows.Forms.SizeType.Absolute;
            rowDefinition2.Height = 100D;
            rowDefinition2.SizeType = System.Windows.Forms.SizeType.Absolute;
            rowDefinition3.Height = 180D;
            rowDefinition3.SizeType = System.Windows.Forms.SizeType.Percent;
            rowDefinition4.Height = 70D;
            rowDefinition4.SizeType = System.Windows.Forms.SizeType.Absolute;
            Root.OptionsTableLayoutGroup.RowDefinitions.AddRange(new DevExpress.XtraLayout.RowDefinition[] { rowDefinition1, rowDefinition2, rowDefinition3, rowDefinition4 });
            Root.Size = new System.Drawing.Size(938, 441);
            Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = pnlconn;
            layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.OptionsTableLayoutItem.ColumnSpan = 2;
            layoutControlItem2.Size = new System.Drawing.Size(918, 110);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = gridModules;
            layoutControlItem1.Location = new System.Drawing.Point(0, 210);
            layoutControlItem1.MinSize = new System.Drawing.Size(454, 124);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem1.Size = new System.Drawing.Size(459, 141);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Bottom;
            layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = panelControl1;
            layoutControlItem3.Location = new System.Drawing.Point(0, 110);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.OptionsTableLayoutItem.ColumnSpan = 2;
            layoutControlItem3.OptionsTableLayoutItem.RowIndex = 1;
            layoutControlItem3.Size = new System.Drawing.Size(918, 100);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = gridChannels;
            layoutControlItem4.Location = new System.Drawing.Point(459, 210);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.OptionsTableLayoutItem.ColumnIndex = 1;
            layoutControlItem4.OptionsTableLayoutItem.RowIndex = 2;
            layoutControlItem4.Size = new System.Drawing.Size(459, 141);
            layoutControlItem4.TextVisible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(938, 441);
            Controls.Add(layoutControl1);
            Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            MinimumSize = new System.Drawing.Size(700, 400);
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridChannels).EndInit();
            ((System.ComponentModel.ISupportInitialize)channels).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCheckEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)spinPollMs.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbModuleType.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridModules).EndInit();
            ((System.ComponentModel.ISupportInitialize)modules).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlconn).EndInit();
            pnlconn.ResumeLayout(false);
            pnlconn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TcpEditTime.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtIp.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.PanelControl pnlconn;
        private DevExpress.XtraEditors.LabelControl IPLabel;
        private DevExpress.XtraEditors.TextEdit txtIp;
        private DevExpress.XtraEditors.LabelControl ms;
        private DevExpress.XtraEditors.SimpleButton btnDisconnect;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.ComboBoxEdit cmbModuleType;
        private DevExpress.XtraEditors.LabelControl lblConn;
        private DevExpress.XtraEditors.SpinEdit spinPollMs;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gridModules;
        private DevExpress.XtraGrid.Views.Grid.GridView modules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.LabelControl ChannelsLabel;
        private DevExpress.XtraEditors.LabelControl ModuleLabel;
        private DevExpress.XtraEditors.LabelControl ModulesLabel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.HyperlinkLabelControl lblMode;
        private DevExpress.XtraEditors.SimpleButton btnAddModule;
        private DevExpress.XtraEditors.SimpleButton btnRemoveModule;
        private DevExpress.XtraGrid.GridControl gridChannels;
        private DevExpress.XtraGrid.Views.Grid.GridView channels;
        private DevExpress.XtraGrid.Columns.GridColumn DISPLAYINDEX;
        private DevExpress.XtraGrid.Columns.GridColumn Value;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SpinEdit TcpEditTime;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnApplyConfig;
        private DevExpress.XtraGrid.Columns.GridColumn Index;
        private DevExpress.XtraGrid.Columns.GridColumn Type;
    }
}

