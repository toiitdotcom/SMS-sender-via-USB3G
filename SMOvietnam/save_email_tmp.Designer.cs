namespace SMOvietnam
{
    partial class save_email_tmp
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_name_tmp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_save_mail_tmp = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_show_ss = new System.Windows.Forms.Label();
            this.panel_save_tmp_mail = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel_save_tmp_mail.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên nháp";
            // 
            // txt_name_tmp
            // 
            this.txt_name_tmp.Location = new System.Drawing.Point(80, 82);
            this.txt_name_tmp.Name = "txt_name_tmp";
            this.txt_name_tmp.Size = new System.Drawing.Size(385, 20);
            this.txt_name_tmp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(75, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(340, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lưu nháp mail đang lên chiến dịch";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btn_save_mail_tmp);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(-11, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 60);
            this.panel1.TabIndex = 3;
            // 
            // btn_save_mail_tmp
            // 
            this.btn_save_mail_tmp.Location = new System.Drawing.Point(324, 9);
            this.btn_save_mail_tmp.Name = "btn_save_mail_tmp";
            this.btn_save_mail_tmp.Size = new System.Drawing.Size(85, 38);
            this.btn_save_mail_tmp.TabIndex = 1;
            this.btn_save_mail_tmp.Text = "Lưu lại";
            this.btn_save_mail_tmp.UseVisualStyleBackColor = true;
            this.btn_save_mail_tmp.Click += new System.EventHandler(this.btn_save_mail_tmp_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(413, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Hủy bỏ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_show_ss
            // 
            this.lbl_show_ss.AutoSize = true;
            this.lbl_show_ss.ForeColor = System.Drawing.Color.Red;
            this.lbl_show_ss.Location = new System.Drawing.Point(64, 13);
            this.lbl_show_ss.Name = "lbl_show_ss";
            this.lbl_show_ss.Size = new System.Drawing.Size(310, 13);
            this.lbl_show_ss.TabIndex = 4;
            this.lbl_show_ss.Text = "Vui lòng không tắt phần mềm khi quá trình lưu chưa hoàn thành.";
            this.lbl_show_ss.Visible = false;
            // 
            // panel_save_tmp_mail
            // 
            this.panel_save_tmp_mail.Controls.Add(this.lbl_show_ss);
            this.panel_save_tmp_mail.Location = new System.Drawing.Point(24, 76);
            this.panel_save_tmp_mail.Name = "panel_save_tmp_mail";
            this.panel_save_tmp_mail.Size = new System.Drawing.Size(463, 41);
            this.panel_save_tmp_mail.TabIndex = 5;
            this.panel_save_tmp_mail.Visible = false;
            // 
            // save_email_tmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 207);
            this.Controls.Add(this.panel_save_tmp_mail);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_name_tmp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "save_email_tmp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel_save_tmp_mail.ResumeLayout(false);
            this.panel_save_tmp_mail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_name_tmp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_save_mail_tmp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_show_ss;
        private System.Windows.Forms.Panel panel_save_tmp_mail;
    }
}