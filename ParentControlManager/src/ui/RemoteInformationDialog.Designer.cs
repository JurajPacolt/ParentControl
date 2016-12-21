namespace ParentControlManager.UI
{
    partial class RemoteInformationDialog
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblActualDate = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtActualDate = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(212, 75);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 75);
            this.btnCancel.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblActualDate, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDuration, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtActualDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDuration, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 52);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // lblActualDate
            // 
            this.lblActualDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblActualDate.AutoSize = true;
            this.lblActualDate.Location = new System.Drawing.Point(3, 6);
            this.lblActualDate.Name = "lblActualDate";
            this.lblActualDate.Size = new System.Drawing.Size(61, 13);
            this.lblActualDate.TabIndex = 0;
            this.lblActualDate.Text = "Actual date";
            // 
            // lblDuration
            // 
            this.lblDuration.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(17, 32);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(47, 13);
            this.lblDuration.TabIndex = 1;
            this.lblDuration.Text = "Duration";
            // 
            // txtActualDate
            // 
            this.txtActualDate.Location = new System.Drawing.Point(70, 3);
            this.txtActualDate.Name = "txtActualDate";
            this.txtActualDate.ReadOnly = true;
            this.txtActualDate.Size = new System.Drawing.Size(103, 20);
            this.txtActualDate.TabIndex = 2;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(70, 29);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.ReadOnly = true;
            this.txtDuration.Size = new System.Drawing.Size(134, 20);
            this.txtDuration.TabIndex = 3;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(13, 75);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(114, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset duration";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RemoteInformationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 110);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RemoteInformationDialog";
            this.Text = "Remote Information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteInformationDialog_FormClosed);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.btnReset, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblActualDate;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtActualDate;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Timer timer1;
    }
}