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
            gridChannels = new DevExpress.XtraGrid.GridControl();
            gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridModules = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnSend = new DevExpress.XtraEditors.SimpleButton();
            lblMode = new DevExpress.XtraEditors.HyperlinkLabelControl();
            pnlconn = new DevExpress.XtraEditors.PanelControl();
            btnDisconnect = new DevExpress.XtraEditors.SimpleButton();
            btnConnect = new DevExpress.XtraEditors.SimpleButton();
            lblConn = new DevExpress.XtraEditors.LabelControl();
            txtIp = new DevExpress.XtraEditors.TextEdit();
            spinPollMs = new DevExpress.XtraEditors.SpinEdit();
            btnAddModule = new DevExpress.XtraEditors.SimpleButton();
            btnRemoveModule = new DevExpress.XtraEditors.SimpleButton();
            btnUp = new DevExpress.XtraEditors.SimpleButton();
            btnDown = new DevExpress.XtraEditors.SimpleButton();
            cmbModuleType = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)gridChannels).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridModules).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pnlconn).BeginInit();
            pnlconn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtIp.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)spinPollMs.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbModuleType.Properties).BeginInit();
            SuspendLayout();
            // 
            // gridChannels
            // 
            gridChannels.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            gridChannels.Location = new System.Drawing.Point(609, 381);
            gridChannels.MainView = gridView2;
            gridChannels.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            gridChannels.Name = "gridChannels";
            gridChannels.Size = new System.Drawing.Size(486, 242);
            gridChannels.TabIndex = 1;
            gridChannels.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView2 });
            // 
            // gridView2
            // 
            gridView2.DetailHeight = 500;
            gridView2.GridControl = gridChannels;
            gridView2.Name = "gridView2";
            gridView2.OptionsEditForm.PopupEditFormWidth = 1028;
            gridView2.CellValueChanging += gridView2_CellValueChanging;
            // 
            // gridView3
            // 
            gridView3.DetailHeight = 500;
            gridView3.GridControl = gridModules;
            gridView3.Name = "gridView3";
            gridView3.OptionsEditForm.PopupEditFormWidth = 1028;
            // 
            // gridModules
            // 
            gridModules.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            gridModules.Location = new System.Drawing.Point(-38, 307);
            gridModules.MainView = gridView1;
            gridModules.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            gridModules.Name = "gridModules";
            gridModules.Size = new System.Drawing.Size(665, 436);
            gridModules.TabIndex = 0;
            gridModules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1, gridView3 });
            // 
            // gridView1
            // 
            gridView1.DetailHeight = 500;
            gridView1.GridControl = gridModules;
            gridView1.Name = "gridView1";
            gridView1.OptionsEditForm.PopupEditFormWidth = 1028;
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            // 
            // btnSend
            // 
            btnSend.Location = new System.Drawing.Point(39, 650);
            btnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnSend.Name = "btnSend";
            btnSend.Size = new System.Drawing.Size(151, 41);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.Click += btnSend_Click;
            // 
            // lblMode
            // 
            lblMode.Appearance.ForeColor = System.Drawing.Color.Red;
            lblMode.Appearance.Options.UseForeColor = true;
            lblMode.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblMode.Location = new System.Drawing.Point(781, 280);
            lblMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            lblMode.Name = "lblMode";
            lblMode.Size = new System.Drawing.Size(117, 24);
            lblMode.TabIndex = 3;
            lblMode.Text = "READ ONLY (DI)";
            lblMode.Visible = false;
            // 
            // pnlconn
            // 
            pnlconn.Controls.Add(btnDisconnect);
            pnlconn.Controls.Add(btnConnect);
            pnlconn.Controls.Add(lblConn);
            pnlconn.Controls.Add(txtIp);
            pnlconn.Location = new System.Drawing.Point(39, 17);
            pnlconn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            pnlconn.Name = "pnlconn";
            pnlconn.Size = new System.Drawing.Size(703, 79);
            pnlconn.TabIndex = 4;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new System.Drawing.Point(402, 34);
            btnDisconnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new System.Drawing.Size(101, 37);
            btnDisconnect.TabIndex = 8;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new System.Drawing.Point(280, 34);
            btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new System.Drawing.Size(99, 37);
            btnConnect.TabIndex = 6;
            btnConnect.Text = "connect";
            btnConnect.Click += simpleButton1_Click;
            // 
            // lblConn
            // 
            lblConn.Location = new System.Drawing.Point(561, 47);
            lblConn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            lblConn.Name = "lblConn";
            lblConn.Size = new System.Drawing.Size(86, 18);
            lblConn.TabIndex = 7;
            lblConn.Text = "Disconnected";
            // 
            // txtIp
            // 
            txtIp.Location = new System.Drawing.Point(7, 39);
            txtIp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtIp.Name = "txtIp";
            txtIp.Size = new System.Drawing.Size(267, 24);
            txtIp.TabIndex = 5;
            // 
            // spinPollMs
            // 
            spinPollMs.EditValue = new decimal(new int[] { 0, 0, 0, 0 });
            spinPollMs.Location = new System.Drawing.Point(98, 162);
            spinPollMs.Name = "spinPollMs";
            spinPollMs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            spinPollMs.Size = new System.Drawing.Size(127, 24);
            spinPollMs.TabIndex = 5;
            spinPollMs.EditValueChanged += spinPollMs_EditValueChanged;
            // 
            // btnAddModule
            // 
            btnAddModule.Location = new System.Drawing.Point(245, 155);
            btnAddModule.Name = "btnAddModule";
            btnAddModule.Size = new System.Drawing.Size(145, 37);
            btnAddModule.TabIndex = 6;
            btnAddModule.Text = "Add";
            btnAddModule.Click += btnAddModule_Click;
            // 
            // btnRemoveModule
            // 
            btnRemoveModule.Location = new System.Drawing.Point(410, 155);
            btnRemoveModule.Name = "btnRemoveModule";
            btnRemoveModule.Size = new System.Drawing.Size(145, 37);
            btnRemoveModule.TabIndex = 7;
            btnRemoveModule.Text = "Remove";
            btnRemoveModule.Click += btnRemoveModule_Click;
            // 
            // btnUp
            // 
            btnUp.Location = new System.Drawing.Point(576, 157);
            btnUp.Name = "btnUp";
            btnUp.Size = new System.Drawing.Size(145, 37);
            btnUp.TabIndex = 8;
            btnUp.Text = "▲";
            btnUp.Click += btnUp_Click;
            // 
            // btnDown
            // 
            btnDown.Location = new System.Drawing.Point(740, 155);
            btnDown.Name = "btnDown";
            btnDown.Size = new System.Drawing.Size(145, 37);
            btnDown.TabIndex = 9;
            btnDown.Text = "▼";
            btnDown.Click += btnDown_Click;
            // 
            // cmbModuleType
            // 
            cmbModuleType.Location = new System.Drawing.Point(98, 232);
            cmbModuleType.Name = "cmbModuleType";
            cmbModuleType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbModuleType.Properties.Items.AddRange(new object[] { "DI", "DO" });
            cmbModuleType.Size = new System.Drawing.Size(141, 24);
            cmbModuleType.TabIndex = 10;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1069, 796);
            Controls.Add(cmbModuleType);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(btnRemoveModule);
            Controls.Add(btnAddModule);
            Controls.Add(spinPollMs);
            Controls.Add(pnlconn);
            Controls.Add(lblMode);
            Controls.Add(btnSend);
            Controls.Add(gridChannels);
            Controls.Add(gridModules);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)gridChannels).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridModules).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlconn).EndInit();
            pnlconn.ResumeLayout(false);
            pnlconn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)txtIp.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)spinPollMs.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbModuleType.Properties).EndInit();
            ResumeLayout(false);


        }

        #endregion
        private DevExpress.XtraGrid.GridControl gridChannels;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.GridControl gridModules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.HyperlinkLabelControl lblMode;
        private DevExpress.XtraEditors.PanelControl pnlconn;
        private DevExpress.XtraEditors.TextEdit txtIp;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.LabelControl lblConn;
        private DevExpress.XtraEditors.SimpleButton btnDisconnect;
        private DevExpress.XtraEditors.SpinEdit spinPollMs;
        private DevExpress.XtraEditors.SimpleButton btnAddModule;
        private DevExpress.XtraEditors.SimpleButton btnRemoveModule;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.ComboBoxEdit cmbModuleType;
    }
}

