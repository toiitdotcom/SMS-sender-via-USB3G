namespace SMOvietnam
{
    partial class ManageAttackMail
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ListfileAttack = new System.Windows.Forms.DataGridView();
            this.btnSelectFile1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.selected_sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_sms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete_mail_pending = new System.Windows.Forms.DataGridViewButtonColumn();
            this._id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListfileAttack)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListfileAttack);
            this.groupBox1.Location = new System.Drawing.Point(12, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(697, 302);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách file đính kèm";
            // 
            // ListfileAttack
            // 
            this.ListfileAttack.AllowUserToAddRows = false;
            this.ListfileAttack.AllowUserToDeleteRows = false;
            this.ListfileAttack.BackgroundColor = System.Drawing.Color.White;
            this.ListfileAttack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListfileAttack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ListfileAttack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selected_sms,
            this.device_sms,
            this.delete_mail_pending,
            this._id});
            this.ListfileAttack.Location = new System.Drawing.Point(6, 19);
            this.ListfileAttack.Name = "ListfileAttack";
            this.ListfileAttack.RowHeadersVisible = false;
            this.ListfileAttack.Size = new System.Drawing.Size(685, 277);
            this.ListfileAttack.TabIndex = 3;
            this.ListfileAttack.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ListfileAttack_CellContentClick);
            // 
            // btnSelectFile1
            // 
            this.btnSelectFile1.BackgroundImage = global::SMOvietnam.Properties.Resources.list_add;
            this.btnSelectFile1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSelectFile1.Location = new System.Drawing.Point(586, 80);
            this.btnSelectFile1.Name = "btnSelectFile1";
            this.btnSelectFile1.Size = new System.Drawing.Size(124, 23);
            this.btnSelectFile1.TabIndex = 2;
            this.btnSelectFile1.Text = "Thêm file đính kèm";
            this.btnSelectFile1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectFile1.UseVisualStyleBackColor = true;
            this.btnSelectFile1.Click += new System.EventHandler(this.btnSelectFile1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(79, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(249, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quản lý file đính kèm";
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
            this.device_sms.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.device_sms.HeaderText = "Tên file";
            this.device_sms.Name = "device_sms";
            this.device_sms.ReadOnly = true;
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
            // ManageAttackMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 411);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectFile1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "ManageAttackMail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListfileAttack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectFile1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView ListfileAttack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn selected_sms;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_sms;
        private System.Windows.Forms.DataGridViewButtonColumn delete_mail_pending;
        private System.Windows.Forms.DataGridViewTextBoxColumn _id;
    }
}