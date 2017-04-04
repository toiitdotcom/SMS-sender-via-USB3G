namespace SMOvietnam
{
    partial class addImg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addImg));
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddImageContentMail = new System.Windows.Forms.Button();
            this.txt_link_img = new System.Windows.Forms.TextBox();
            this.lbl_link_upload_img = new System.Windows.Forms.LinkLabel();
            this.btn_close_addimg = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.locateimg = new System.Windows.Forms.TextBox();
            this.btn_selectimg = new System.Windows.Forms.Button();
            this.btn_upload_img = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link ảnh";
            // 
            // btnAddImageContentMail
            // 
            this.btnAddImageContentMail.Location = new System.Drawing.Point(139, 237);
            this.btnAddImageContentMail.Name = "btnAddImageContentMail";
            this.btnAddImageContentMail.Size = new System.Drawing.Size(75, 23);
            this.btnAddImageContentMail.TabIndex = 2;
            this.btnAddImageContentMail.Text = "Đồng ý";
            this.btnAddImageContentMail.UseVisualStyleBackColor = true;
            this.btnAddImageContentMail.Click += new System.EventHandler(this.btnAddImageContentMail_Click);
            // 
            // txt_link_img
            // 
            this.txt_link_img.Location = new System.Drawing.Point(69, 111);
            this.txt_link_img.Name = "txt_link_img";
            this.txt_link_img.Size = new System.Drawing.Size(263, 20);
            this.txt_link_img.TabIndex = 3;
            // 
            // lbl_link_upload_img
            // 
            this.lbl_link_upload_img.AutoSize = true;
            this.lbl_link_upload_img.Location = new System.Drawing.Point(247, 240);
            this.lbl_link_upload_img.Name = "lbl_link_upload_img";
            this.lbl_link_upload_img.Size = new System.Drawing.Size(86, 13);
            this.lbl_link_upload_img.TabIndex = 4;
            this.lbl_link_upload_img.TabStop = true;
            this.lbl_link_upload_img.Text = "Up ảnh miễn phí";
            this.lbl_link_upload_img.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_link_upload_img_LinkClicked);
            // 
            // btn_close_addimg
            // 
            this.btn_close_addimg.BackgroundImage = global::SMOvietnam.Properties.Resources.closewin;
            this.btn_close_addimg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_close_addimg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close_addimg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_addimg.ForeColor = System.Drawing.Color.Transparent;
            this.btn_close_addimg.Location = new System.Drawing.Point(317, -2);
            this.btn_close_addimg.Name = "btn_close_addimg";
            this.btn_close_addimg.Size = new System.Drawing.Size(35, 20);
            this.btn_close_addimg.TabIndex = 13;
            this.btn_close_addimg.UseVisualStyleBackColor = false;
            this.btn_close_addimg.Visible = false;
            this.btn_close_addimg.Click += new System.EventHandler(this.btn_close_addimg_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(18, 36);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(326, 52);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "Gợi ý: Bạn có thể copy nguồn hình ảnh từ các nguồn trên internet, sau dó bạn chỉ " +
                "việc dán link bạn copy được vào ô nhập \"Link ảnh\" bên phía dưới.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Hoặc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Up ảnh";
            // 
            // locateimg
            // 
            this.locateimg.Location = new System.Drawing.Point(69, 169);
            this.locateimg.Name = "locateimg";
            this.locateimg.Size = new System.Drawing.Size(145, 20);
            this.locateimg.TabIndex = 17;
            // 
            // btn_selectimg
            // 
            this.btn_selectimg.Location = new System.Drawing.Point(208, 168);
            this.btn_selectimg.Name = "btn_selectimg";
            this.btn_selectimg.Size = new System.Drawing.Size(56, 23);
            this.btn_selectimg.TabIndex = 18;
            this.btn_selectimg.Text = "Chọn file";
            this.btn_selectimg.UseVisualStyleBackColor = true;
            this.btn_selectimg.Click += new System.EventHandler(this.btn_selectimg_Click);
            // 
            // btn_upload_img
            // 
            this.btn_upload_img.Location = new System.Drawing.Point(271, 168);
            this.btn_upload_img.Name = "btn_upload_img";
            this.btn_upload_img.Size = new System.Drawing.Size(62, 23);
            this.btn_upload_img.TabIndex = 19;
            this.btn_upload_img.Text = "UPLOAD";
            this.btn_upload_img.UseVisualStyleBackColor = true;
            this.btn_upload_img.Click += new System.EventHandler(this.btn_upload_img_Click);
            // 
            // addImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 301);
            this.Controls.Add(this.btn_upload_img);
            this.Controls.Add(this.btn_selectimg);
            this.Controls.Add(this.locateimg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_close_addimg);
            this.Controls.Add(this.lbl_link_upload_img);
            this.Controls.Add(this.txt_link_img);
            this.Controls.Add(this.btnAddImageContentMail);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "addImg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm hình ảnh vào nội dung mail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddImageContentMail;
        private System.Windows.Forms.TextBox txt_link_img;
        private System.Windows.Forms.LinkLabel lbl_link_upload_img;
        private System.Windows.Forms.Button btn_close_addimg;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox locateimg;
        private System.Windows.Forms.Button btn_selectimg;
        private System.Windows.Forms.Button btn_upload_img;
    }
}