namespace SMOvietnam
{
    partial class welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(welcome));
            this.panel1 = new System.Windows.Forms.Panel();
            this.notofication_login = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.lbl_Matkhau = new System.Windows.Forms.Label();
            this.lbl_Taikhoan = new System.Windows.Forms.Label();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.txt_acc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.saveaccount = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.notofication_login);
            this.panel1.Controls.Add(this.saveaccount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Controls.Add(this.btn_login);
            this.panel1.Controls.Add(this.lbl_Matkhau);
            this.panel1.Controls.Add(this.lbl_Taikhoan);
            this.panel1.Controls.Add(this.txt_pass);
            this.panel1.Controls.Add(this.txt_acc);
            this.panel1.Location = new System.Drawing.Point(233, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 213);
            this.panel1.TabIndex = 0;
            // 
            // notofication_login
            // 
            this.notofication_login.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.notofication_login.BackgroundImage = global::SMOvietnam.Properties.Resources.m2GbSfN;
            this.notofication_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.notofication_login.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notofication_login.Location = new System.Drawing.Point(0, 0);
            this.notofication_login.Name = "notofication_login";
            this.notofication_login.Size = new System.Drawing.Size(439, 213);
            this.notofication_login.TabIndex = 5;
            this.notofication_login.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(164, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "ĐĂNG NHẬP";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancel.Location = new System.Drawing.Point(235, 169);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Hủy";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_login
            // 
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Location = new System.Drawing.Point(154, 169);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(75, 23);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "Đăng nhập";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // lbl_Matkhau
            // 
            this.lbl_Matkhau.AutoSize = true;
            this.lbl_Matkhau.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Matkhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Matkhau.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Matkhau.Location = new System.Drawing.Point(80, 108);
            this.lbl_Matkhau.Name = "lbl_Matkhau";
            this.lbl_Matkhau.Size = new System.Drawing.Size(75, 20);
            this.lbl_Matkhau.TabIndex = 3;
            this.lbl_Matkhau.Text = "Mật khẩu";
            // 
            // lbl_Taikhoan
            // 
            this.lbl_Taikhoan.AutoSize = true;
            this.lbl_Taikhoan.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Taikhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Taikhoan.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Taikhoan.Location = new System.Drawing.Point(80, 70);
            this.lbl_Taikhoan.Name = "lbl_Taikhoan";
            this.lbl_Taikhoan.Size = new System.Drawing.Size(78, 20);
            this.lbl_Taikhoan.TabIndex = 2;
            this.lbl_Taikhoan.Text = "Tài khoản";
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(185, 108);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.PasswordChar = '*';
            this.txt_pass.Size = new System.Drawing.Size(183, 20);
            this.txt_pass.TabIndex = 1;
            this.txt_pass.Text = "123123";
            this.txt_pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_pass_KeyDown);
            // 
            // txt_acc
            // 
            this.txt_acc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_acc.Location = new System.Drawing.Point(185, 72);
            this.txt_acc.Name = "txt_acc";
            this.txt_acc.Size = new System.Drawing.Size(183, 20);
            this.txt_acc.TabIndex = 0;
            this.txt_acc.Text = "username";
            this.txt_acc.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txt_acc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_acc_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 529);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Copyright by BEECOM - 2015";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(582, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Viet Nam";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::SMOvietnam.Properties.Resources.closewin;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(872, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 20);
            this.button1.TabIndex = 4;
            this.button1.UseMnemonic = false;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(13, 554);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Phát triển bởi: xappvietnam.com";
            // 
            // saveaccount
            // 
            this.saveaccount.AutoSize = true;
            this.saveaccount.Checked = true;
            this.saveaccount.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveaccount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.saveaccount.Location = new System.Drawing.Point(185, 138);
            this.saveaccount.Name = "saveaccount";
            this.saveaccount.Size = new System.Drawing.Size(93, 17);
            this.saveaccount.TabIndex = 7;
            this.saveaccount.Text = "Nhớ tài khoản";
            this.saveaccount.UseVisualStyleBackColor = true;
            // 
            // welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SMOvietnam.Properties.Resources.kftutW0;
            this.ClientSize = new System.Drawing.Size(909, 576);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "welcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Marketing Online [Ver 1.0.0]";
            this.Load += new System.EventHandler(this.welcome_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.welcome_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.welcome_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.welcome_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_Matkhau;
        private System.Windows.Forms.Label lbl_Taikhoan;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.TextBox txt_acc;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel notofication_login;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox saveaccount;

    }
}

