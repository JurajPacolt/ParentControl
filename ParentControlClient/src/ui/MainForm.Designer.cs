namespace ParentControlClient.UI
{
    partial class MainForm
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridViewRules = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDayOfWeek = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewRule = new System.Windows.Forms.ToolStripButton();
            this.btnModifyRule = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteRule = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnInfo = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewRule,
            this.btnModifyRule,
            this.btnDeleteRule,
            this.toolStripSeparator1,
            this.btnSettings,
            this.toolStripSeparator2,
            this.btnReset,
            this.toolStripSeparator3,
            this.btnInfo});
            this.toolStripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(59, 442);
            this.toolStripMain.TabIndex = 0;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(56, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(56, 6);
            // 
            // dataGridViewRules
            // 
            this.dataGridViewRules.AllowUserToAddRows = false;
            this.dataGridViewRules.AllowUserToDeleteRows = false;
            this.dataGridViewRules.AllowUserToResizeRows = false;
            this.dataGridViewRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colFrom,
            this.colTo,
            this.Duration,
            this.colDayOfWeek,
            this.colEnabled});
            this.dataGridViewRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewRules.Location = new System.Drawing.Point(59, 0);
            this.dataGridViewRules.MultiSelect = false;
            this.dataGridViewRules.Name = "dataGridViewRules";
            this.dataGridViewRules.ReadOnly = true;
            this.dataGridViewRules.RowHeadersVisible = false;
            this.dataGridViewRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRules.Size = new System.Drawing.Size(565, 442);
            this.dataGridViewRules.TabIndex = 1;
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colFrom
            // 
            this.colFrom.HeaderText = "From";
            this.colFrom.Name = "colFrom";
            this.colFrom.ReadOnly = true;
            // 
            // colTo
            // 
            this.colTo.HeaderText = "To";
            this.colTo.Name = "colTo";
            this.colTo.ReadOnly = true;
            // 
            // Duration
            // 
            this.Duration.HeaderText = "Duration";
            this.Duration.Name = "Duration";
            this.Duration.ReadOnly = true;
            // 
            // colDayOfWeek
            // 
            this.colDayOfWeek.HeaderText = "Day of week";
            this.colDayOfWeek.Name = "colDayOfWeek";
            this.colDayOfWeek.ReadOnly = true;
            // 
            // colEnabled
            // 
            this.colEnabled.HeaderText = "Enabled";
            this.colEnabled.Name = "colEnabled";
            this.colEnabled.ReadOnly = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(56, 6);
            // 
            // btnNewRule
            // 
            this.btnNewRule.Image = global::ParentControlClient.Properties.Resources.add;
            this.btnNewRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewRule.Name = "btnNewRule";
            this.btnNewRule.Size = new System.Drawing.Size(56, 35);
            this.btnNewRule.Text = "New rule";
            this.btnNewRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewRule.Click += new System.EventHandler(this.btnNewRule_Click);
            // 
            // btnModifyRule
            // 
            this.btnModifyRule.Image = global::ParentControlClient.Properties.Resources.modify;
            this.btnModifyRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModifyRule.Name = "btnModifyRule";
            this.btnModifyRule.Size = new System.Drawing.Size(56, 35);
            this.btnModifyRule.Text = "Modify";
            this.btnModifyRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModifyRule.Click += new System.EventHandler(this.btnModifyRule_Click);
            // 
            // btnDeleteRule
            // 
            this.btnDeleteRule.Image = global::ParentControlClient.Properties.Resources.delete;
            this.btnDeleteRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteRule.Name = "btnDeleteRule";
            this.btnDeleteRule.Size = new System.Drawing.Size(56, 35);
            this.btnDeleteRule.Text = "Delete";
            this.btnDeleteRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteRule.Click += new System.EventHandler(this.btnDeleteRule_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Image = global::ParentControlClient.Properties.Resources.settings;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(56, 35);
            this.btnSettings.Text = "Settings";
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = global::ParentControlClient.Properties.Resources.reset;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(56, 35);
            this.btnReset.Text = "Reset";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Image = global::ParentControlClient.Properties.Resources.information;
            this.btnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(56, 35);
            this.btnInfo.Text = "Info";
            this.btnInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.dataGridViewRules);
            this.Controls.Add(this.toolStripMain);
            this.Name = "MainForm";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRules)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.DataGridView dataGridViewRules;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDayOfWeek;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnabled;
        private System.Windows.Forms.ToolStripButton btnNewRule;
        private System.Windows.Forms.ToolStripButton btnModifyRule;
        private System.Windows.Forms.ToolStripButton btnDeleteRule;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnInfo;
    }
}

