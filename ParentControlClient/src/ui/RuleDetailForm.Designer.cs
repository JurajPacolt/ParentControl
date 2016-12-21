namespace ParentControlClient.UI
{
    partial class RuleDetailForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblFromDateAndTime = new System.Windows.Forms.Label();
            this.lblToDateAndTime = new System.Windows.Forms.Label();
            this.dtpFromDateAndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpToDateAndTime = new System.Windows.Forms.DateTimePicker();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDayOfWeek = new System.Windows.Forms.Label();
            this.lblEnabled = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.cboDayOfWeek = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(251, 177);
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(332, 177);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFromDateAndTime, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblToDateAndTime, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dtpFromDateAndTime, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpToDateAndTime, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblDuration, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDayOfWeek, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblEnabled, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.nudDuration, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkEnabled, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.cboDayOfWeek, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(397, 159);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(65, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(106, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(288, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblFromDateAndTime
            // 
            this.lblFromDateAndTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFromDateAndTime.AutoSize = true;
            this.lblFromDateAndTime.Location = new System.Drawing.Point(3, 32);
            this.lblFromDateAndTime.Name = "lblFromDateAndTime";
            this.lblFromDateAndTime.Size = new System.Drawing.Size(97, 13);
            this.lblFromDateAndTime.TabIndex = 2;
            this.lblFromDateAndTime.Text = "From date and time";
            // 
            // lblToDateAndTime
            // 
            this.lblToDateAndTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblToDateAndTime.AutoSize = true;
            this.lblToDateAndTime.Location = new System.Drawing.Point(13, 58);
            this.lblToDateAndTime.Name = "lblToDateAndTime";
            this.lblToDateAndTime.Size = new System.Drawing.Size(87, 13);
            this.lblToDateAndTime.TabIndex = 3;
            this.lblToDateAndTime.Text = "To date and time";
            // 
            // dtpFromDateAndTime
            // 
            this.dtpFromDateAndTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpFromDateAndTime.Checked = false;
            this.dtpFromDateAndTime.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpFromDateAndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDateAndTime.Location = new System.Drawing.Point(106, 29);
            this.dtpFromDateAndTime.Name = "dtpFromDateAndTime";
            this.dtpFromDateAndTime.ShowCheckBox = true;
            this.dtpFromDateAndTime.Size = new System.Drawing.Size(178, 20);
            this.dtpFromDateAndTime.TabIndex = 4;
            // 
            // dtpToDateAndTime
            // 
            this.dtpToDateAndTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpToDateAndTime.Checked = false;
            this.dtpToDateAndTime.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpToDateAndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDateAndTime.Location = new System.Drawing.Point(106, 55);
            this.dtpToDateAndTime.Name = "dtpToDateAndTime";
            this.dtpToDateAndTime.ShowCheckBox = true;
            this.dtpToDateAndTime.Size = new System.Drawing.Size(178, 20);
            this.dtpToDateAndTime.TabIndex = 5;
            // 
            // lblDuration
            // 
            this.lblDuration.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(8, 84);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(92, 13);
            this.lblDuration.TabIndex = 6;
            this.lblDuration.Text = "Duration [minutes]";
            // 
            // lblDayOfWeek
            // 
            this.lblDayOfWeek.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDayOfWeek.AutoSize = true;
            this.lblDayOfWeek.Location = new System.Drawing.Point(33, 111);
            this.lblDayOfWeek.Name = "lblDayOfWeek";
            this.lblDayOfWeek.Size = new System.Drawing.Size(67, 13);
            this.lblDayOfWeek.TabIndex = 7;
            this.lblDayOfWeek.Text = "Day of week";
            // 
            // lblEnabled
            // 
            this.lblEnabled.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEnabled.AutoSize = true;
            this.lblEnabled.Location = new System.Drawing.Point(54, 138);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(46, 13);
            this.lblEnabled.TabIndex = 8;
            this.lblEnabled.Text = "Enabled";
            // 
            // nudDuration
            // 
            this.nudDuration.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nudDuration.Location = new System.Drawing.Point(106, 81);
            this.nudDuration.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(82, 20);
            this.nudDuration.TabIndex = 9;
            // 
            // chkEnabled
            // 
            this.chkEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(106, 134);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(21, 21);
            this.chkEnabled.TabIndex = 11;
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // cboDayOfWeek
            // 
            this.cboDayOfWeek.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboDayOfWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDayOfWeek.FormattingEnabled = true;
            this.cboDayOfWeek.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.cboDayOfWeek.Location = new System.Drawing.Point(106, 107);
            this.cboDayOfWeek.Name = "cboDayOfWeek";
            this.cboDayOfWeek.Size = new System.Drawing.Size(121, 21);
            this.cboDayOfWeek.TabIndex = 12;
            // 
            // RuleDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(418, 212);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RuleDetailForm";
            this.Text = "RuleDetailForm";
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblFromDateAndTime;
        private System.Windows.Forms.Label lblToDateAndTime;
        private System.Windows.Forms.DateTimePicker dtpFromDateAndTime;
        private System.Windows.Forms.DateTimePicker dtpToDateAndTime;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDayOfWeek;
        private System.Windows.Forms.Label lblEnabled;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.ComboBox cboDayOfWeek;
    }
}