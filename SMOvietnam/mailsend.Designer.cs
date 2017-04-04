namespace SMOvietnam
{
    partial class mailsend
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
            this.ListMailsendok = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.selected_sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.setup = new System.Windows.Forms.DataGridViewButtonColumn();
            this.delete_mail_sended = new System.Windows.Forms.DataGridViewButtonColumn();
            this._id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ListMailsendok)).BeginInit();
            this.SuspendLayout();
            // 
            // ListMailsendok
            // 
            this.ListMailsendok.AllowUserToAddRows = false;
            this.ListMailsendok.AllowUserToDeleteRows = false;
            this.ListMailsendok.BackgroundColor = System.Drawing.Color.White;
            this.ListMailsendok.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListMailsendok.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListMailsendok.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selected_sms,
            this.device_sms,
            this.setup,
            this.delete_mail_sended,
            this._id});
            this.ListMailsendok.Location = new System.Drawing.Point(12, 62);
            this.ListMailsendok.Name = "ListMailsendok";
            this.ListMailsendok.RowHeadersVisible = false;
            this.ListMailsendok.Size = new System.Drawing.Size(651, 275);
            this.ListMailsendok.TabIndex = 2;
            this.ListMailsendok.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListMailsendok_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(213, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Danh sách Mail đã gửi";
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
            // delete_mail_sended
            // 
            this.delete_mail_sended.HeaderText = "";
            this.delete_mail_sended.Name = "delete_mail_sended";
            this.delete_mail_sended.Text = "Xóa";
            this.delete_mail_sended.UseColumnTextForButtonValue = true;
            this.delete_mail_sended.Width = 60;
            // 
            // _id
            // 
            this._id.HeaderText = "_id";
            this._id.Name = "_id";
            this._id.ReadOnly = true;
            this._id.Visible = false;
            // 
            // mailsend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 349);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListMailsendok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mailsend";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.ListMailsendok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ListMailsendok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn selected_sms;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_sms;
        private System.Windows.Forms.DataGridViewButtonColumn setup;
        private System.Windows.Forms.DataGridViewButtonColumn delete_mail_sended;
        private System.Windows.Forms.DataGridViewTextBoxColumn _id;
    }
}