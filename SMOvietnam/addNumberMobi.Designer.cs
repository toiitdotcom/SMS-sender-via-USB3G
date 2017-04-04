namespace SMOvietnam
{
    partial class addNumberMobi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addNumberMobi));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_add_mobile = new System.Windows.Forms.TextBox();
            this.btn_popup_add_mobile = new System.Windows.Forms.Button();
            this.btn_close_addmobile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_close_add_numbermobile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số điện thoại";
            // 
            // txt_add_mobile
            // 
            this.txt_add_mobile.Location = new System.Drawing.Point(92, 45);
            this.txt_add_mobile.Multiline = true;
            this.txt_add_mobile.Name = "txt_add_mobile";
            this.txt_add_mobile.Size = new System.Drawing.Size(291, 105);
            this.txt_add_mobile.TabIndex = 1;
            // 
            // btn_popup_add_mobile
            // 
            this.btn_popup_add_mobile.Location = new System.Drawing.Point(128, 205);
            this.btn_popup_add_mobile.Name = "btn_popup_add_mobile";
            this.btn_popup_add_mobile.Size = new System.Drawing.Size(75, 28);
            this.btn_popup_add_mobile.TabIndex = 4;
            this.btn_popup_add_mobile.Text = "Thêm";
            this.btn_popup_add_mobile.UseVisualStyleBackColor = true;
            this.btn_popup_add_mobile.Click += new System.EventHandler(this.btn_popup_add_mobile_Click);
            // 
            // btn_close_addmobile
            // 
            this.btn_close_addmobile.BackgroundImage = global::SMOvietnam.Properties.Resources.closewin;
            this.btn_close_addmobile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_close_addmobile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close_addmobile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_addmobile.ForeColor = System.Drawing.Color.Transparent;
            this.btn_close_addmobile.Location = new System.Drawing.Point(374, -2);
            this.btn_close_addmobile.Name = "btn_close_addmobile";
            this.btn_close_addmobile.Size = new System.Drawing.Size(35, 20);
            this.btn_close_addmobile.TabIndex = 13;
            this.btn_close_addmobile.UseVisualStyleBackColor = false;
            this.btn_close_addmobile.Visible = false;
            this.btn_close_addmobile.Click += new System.EventHandler(this.btn_close_addmobile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(346, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Gợi ý: số điện thoại cách nhau bởi dấu \",\" . Ví dụ: 098xxxxxx,098xxxxxx";
            // 
            // btn_close_add_numbermobile
            // 
            this.btn_close_add_numbermobile.Location = new System.Drawing.Point(209, 205);
            this.btn_close_add_numbermobile.Name = "btn_close_add_numbermobile";
            this.btn_close_add_numbermobile.Size = new System.Drawing.Size(75, 28);
            this.btn_close_add_numbermobile.TabIndex = 15;
            this.btn_close_add_numbermobile.Text = "Đóng lại";
            this.btn_close_add_numbermobile.UseVisualStyleBackColor = true;
            this.btn_close_add_numbermobile.Click += new System.EventHandler(this.btn_close_add_numbermobile_Click);
            // 
            // addNumberMobi
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(416, 249);
            this.Controls.Add(this.btn_close_add_numbermobile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_close_addmobile);
            this.Controls.Add(this.btn_popup_add_mobile);
            this.Controls.Add(this.txt_add_mobile);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addNumberMobi";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Number Mobile - Smart Marketing Online [ 1.0.0 ]";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_add_mobile;
        private System.Windows.Forms.Button btn_popup_add_mobile;
        private System.Windows.Forms.Button btn_close_addmobile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_close_add_numbermobile;
    }
}