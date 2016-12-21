namespace ParentControlManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnAddStation = new System.Windows.Forms.ToolStripButton();
            this.btnEditStation = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteStation = new System.Windows.Forms.ToolStripButton();
            this.listViewMain = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.cmsMenuItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditStation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteStation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoteInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain.SuspendLayout();
            this.cmsMenuItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddStation,
            this.btnEditStation,
            this.btnDeleteStation});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(84, 442);
            this.toolStripMain.TabIndex = 0;
            // 
            // btnAddStation
            // 
            this.btnAddStation.Image = global::ParentControlManager.Properties.Resources.add;
            this.btnAddStation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddStation.Name = "btnAddStation";
            this.btnAddStation.Size = new System.Drawing.Size(81, 35);
            this.btnAddStation.Text = "Add station";
            this.btnAddStation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddStation.Click += new System.EventHandler(this.btnAddStation_Click);
            // 
            // btnEditStation
            // 
            this.btnEditStation.Image = global::ParentControlManager.Properties.Resources.modify;
            this.btnEditStation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditStation.Name = "btnEditStation";
            this.btnEditStation.Size = new System.Drawing.Size(81, 35);
            this.btnEditStation.Text = "Edit station";
            this.btnEditStation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditStation.Click += new System.EventHandler(this.btnEditStation_Click);
            // 
            // btnDeleteStation
            // 
            this.btnDeleteStation.Image = global::ParentControlManager.Properties.Resources.delete;
            this.btnDeleteStation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteStation.Name = "btnDeleteStation";
            this.btnDeleteStation.Size = new System.Drawing.Size(81, 35);
            this.btnDeleteStation.Text = "Delete station";
            this.btnDeleteStation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteStation.Click += new System.EventHandler(this.btnDeleteStation_Click);
            // 
            // listViewMain
            // 
            this.listViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMain.LargeImageList = this.imageList;
            this.listViewMain.Location = new System.Drawing.Point(84, 0);
            this.listViewMain.MultiSelect = false;
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(540, 442);
            this.listViewMain.SmallImageList = this.imageList;
            this.listViewMain.TabIndex = 1;
            this.listViewMain.UseCompatibleStateImageBehavior = false;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "workstation.png");
            // 
            // cmsMenuItems
            // 
            this.cmsMenuItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditStation,
            this.tsmiDeleteStation,
            this.toolStripSeparator1,
            this.tsmiRemoteInformation});
            this.cmsMenuItems.Name = "cmsMenuItems";
            this.cmsMenuItems.Size = new System.Drawing.Size(182, 98);
            // 
            // tsmiEditStation
            // 
            this.tsmiEditStation.Image = global::ParentControlManager.Properties.Resources.modify;
            this.tsmiEditStation.Name = "tsmiEditStation";
            this.tsmiEditStation.Size = new System.Drawing.Size(181, 22);
            this.tsmiEditStation.Tag = "EDIT";
            this.tsmiEditStation.Text = "Edit Station";
            // 
            // tsmiDeleteStation
            // 
            this.tsmiDeleteStation.Image = global::ParentControlManager.Properties.Resources.delete;
            this.tsmiDeleteStation.Name = "tsmiDeleteStation";
            this.tsmiDeleteStation.Size = new System.Drawing.Size(181, 22);
            this.tsmiDeleteStation.Tag = "DELETE";
            this.tsmiDeleteStation.Text = "Delete station";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // tsmiRemoteInformation
            // 
            this.tsmiRemoteInformation.Name = "tsmiRemoteInformation";
            this.tsmiRemoteInformation.Size = new System.Drawing.Size(181, 22);
            this.tsmiRemoteInformation.Tag = "REMOTE_INFORMATION";
            this.tsmiRemoteInformation.Text = "Remote information";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.listViewMain);
            this.Controls.Add(this.toolStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Parent Control Manager";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.cmsMenuItems.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.ToolStripButton btnAddStation;
        private System.Windows.Forms.ToolStripButton btnEditStation;
        private System.Windows.Forms.ToolStripButton btnDeleteStation;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip cmsMenuItems;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditStation;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteStation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoteInformation;
    }
}

