namespace SMOvietnam
{
    partial class mailPending
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ListMailPending = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.selected_sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.setup = new System.Windows.Forms.DataGridViewButtonColumn();
            this.delete_mail_pending = new System.Windows.Forms.DataGridViewButtonColumn();
            this._id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListMailPending)).BeginInit();
            this.SuspendLayout();
            // 
            // ListMailPending
            // 
            this.ListMailPending.AllowUserToAddRows = false;
            this.ListMailPending.AllowUserToDeleteRows = false;
            this.ListMailPending.BackgroundColor = System.Drawing.Color.White;
            this.ListMailPending.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListMailPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListMailPending.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selected_sms,
            this.device_sms,
            this.setup,
            this.delete_mail_pending,
            this._id});
            this.ListMailPending.Location = new System.Drawing.Point(12, 62);
            this.ListMailPending.Name = "ListMailPending";
            this.ListMailPending.RowHeadersVisible = false;
            this.ListMailPending.Size = new System.Drawing.Size(651, 275);
            this.ListMailPending.TabIndex = 2;
            this.ListMailPending.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListMailPending_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Danh sách Mail đang lên chiến dịch";
            // 
            // selected_sms
            // 
            this.selected_sms.HeaderText = "STT";
            this.selected_sms.Name = "selected_sms";
            this.selected_sms.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.selected_sms.Width = 65;
            // 
            // device_sms
            // 
            this.device_sms.HeaderText = "Tên mail";
            this.device_sms.Name = "device_sms";
            this.device_sms.ReadOnly = true;
            this.device_sms.Width = 420;
            // 
            // setup
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.setup.DefaultCellStyle = dataGridViewCellStyle1;
            this.setup.HeaderText = "";
            this.setup.Name = "setup";
            this.setup.Text = "Soạn tiếp";
            this.setup.UseColumnTextForButtonValue = true;
            this.setup.Width = 90;
            // 
            // delete_mail_pending
            // 
            this.delete_mail_pending.HeaderText = "";
            this.delete_mail_pending.Name = "delete_mail_pending";
            this.delete_mail_pending.Text = "Xóa";
            this.delete_mail_pending.UseColumnTextForButtonValue = true;
            this.delete_mail_pending.Width = 60;
            // 
            // _id
            // 
            this._id.HeaderText = "_id";
            this._id.Name = "_id";
            this._id.ReadOnly = true;
            this._id.Visible = false;
            // 
            // mailPending
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 349);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListMailPending);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mailPending";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.ListMailPending)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ListMailPending;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn selected_sms;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_sms;
        private System.Windows.Forms.DataGridViewButtonColumn setup;
        private System.Windows.Forms.DataGridViewButtonColumn delete_mail_pending;
        private System.Windows.Forms.DataGridViewTextBoxColumn _id;
    }
}