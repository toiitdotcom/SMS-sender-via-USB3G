namespace SMOvietnam
{
    partial class addEmailToListCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addEmailToListCheck));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_popup_email_check2 = new System.Windows.Forms.TextBox();
            this.btnAddEmailPopup = new System.Windows.Forms.Button();
            this.btn_close_addserversmtp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Địa chỉ Email";
            // 
            // txt_popup_email_check2
            // 
            this.txt_popup_email_check2.Location = new System.Drawing.Point(93, 33);
            this.txt_popup_email_check2.Multiline = true;
            this.txt_popup_email_check2.Name = "txt_popup_email_check2";
            this.txt_popup_email_check2.Size = new System.Drawing.Size(403, 127);
            this.txt_popup_email_check2.TabIndex = 1;
            // 
            // btnAddEmailPopup
            // 
            this.btnAddEmailPopup.Location = new System.Drawing.Point(421, 211);
            this.btnAddEmailPopup.Name = "btnAddEmailPopup";
            this.btnAddEmailPopup.Size = new System.Drawing.Size(75, 23);
            this.btnAddEmailPopup.TabIndex = 4;
            this.btnAddEmailPopup.Text = "Đồng ý";
            this.btnAddEmailPopup.UseVisualStyleBackColor = true;
            this.btnAddEmailPopup.Click += new System.EventHandler(this.btnAddEmailPopup_Click);
            // 
            // btn_close_addserversmtp
            // 
            this.btn_close_addserversmtp.BackgroundImage = global::SMOvietnam.Properties.Resources.closewin;
            this.btn_close_addserversmtp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_close_addserversmtp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close_addserversmtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_addserversmtp.ForeColor = System.Drawing.Color.Transparent;
            this.btn_close_addserversmtp.Location = new System.Drawing.Point(461, -2);
            this.btn_close_addserversmtp.Name = "btn_close_addserversmtp";
            this.btn_close_addserversmtp.Size = new System.Drawing.Size(35, 20);
            this.btn_close_addserversmtp.TabIndex = 13;
            this.btn_close_addserversmtp.UseVisualStyleBackColor = false;
            this.btn_close_addserversmtp.Visible = false;
            this.btn_close_addserversmtp.Click += new System.EventHandler(this.btn_close_addserversmtp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(428, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Gợi ý: Địa chỉ email cách nahu bằng dấu phẩy, Ví dụ: abc@domain.com,cde@gmail.com" +
                "";
            // 
            // addEmailToListCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(508, 246);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_close_addserversmtp);
            this.Controls.Add(this.btnAddEmailPopup);
            this.Controls.Add(this.txt_popup_email_check2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addEmailToListCheck";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Email Vào Danh Sách Verify - Smart Marketing Online [ Version 1.0.0 ]";
            this.Load += new System.EventHandler(this.addEmailToListSend_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_popup_email_check2;
        private System.Windows.Forms.Button btnAddEmailPopup;
        private System.Windows.Forms.Button btn_close_addserversmtp;
        private System.Windows.Forms.Label label2;
    }
}