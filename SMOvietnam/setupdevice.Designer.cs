namespace SMOvietnam
{
    partial class setupdevice
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
            this.btn_close_setupdevice = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_networl = new System.Windows.Forms.Label();
            this.cbnetwork = new System.Windows.Forms.ComboBox();
            this.btn_save_setup = new System.Windows.Forms.Button();
            this.cbdeelay = new System.Windows.Forms.ComboBox();
            this.cbamount = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thiết bị:";
            // 
            // btn_close_setupdevice
            // 
            this.btn_close_setupdevice.BackgroundImage = global::SMOvietnam.Properties.Resources.closewin;
            this.btn_close_setupdevice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_close_setupdevice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close_setupdevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_setupdevice.ForeColor = System.Drawing.Color.Transparent;
            this.btn_close_setupdevice.Location = new System.Drawing.Point(329, -2);
            this.btn_close_setupdevice.Name = "btn_close_setupdevice";
            this.btn_close_setupdevice.Size = new System.Drawing.Size(35, 20);
            this.btn_close_setupdevice.TabIndex = 14;
            this.btn_close_setupdevice.UseVisualStyleBackColor = false;
            this.btn_close_setupdevice.Visible = false;
            this.btn_close_setupdevice.Click += new System.EventHandler(this.btn_close_setupdevice_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Mạng di động:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Độ trễ ( giây ):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Giới hạn số tin: ";
            // 
            // lbl_networl
            // 
            this.lbl_networl.AutoSize = true;
            this.lbl_networl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_networl.ForeColor = System.Drawing.Color.Green;
            this.lbl_networl.Location = new System.Drawing.Point(163, 33);
            this.lbl_networl.Name = "lbl_networl";
            this.lbl_networl.Size = new System.Drawing.Size(96, 13);
            this.lbl_networl.TabIndex = 18;
            this.lbl_networl.Text = "Không xác định";
            // 
            // cbnetwork
            // 
            this.cbnetwork.BackColor = System.Drawing.SystemColors.Window;
            this.cbnetwork.FormattingEnabled = true;
            this.cbnetwork.Items.AddRange(new object[] {
            "Mobifone",
            "Vinaphone",
            "Beeline",
            "Viettel",
            "Vietnamobile"});
            this.cbnetwork.Location = new System.Drawing.Point(165, 55);
            this.cbnetwork.Name = "cbnetwork";
            this.cbnetwork.Size = new System.Drawing.Size(121, 21);
            this.cbnetwork.TabIndex = 19;
            // 
            // btn_save_setup
            // 
            this.btn_save_setup.Location = new System.Drawing.Point(144, 151);
            this.btn_save_setup.Name = "btn_save_setup";
            this.btn_save_setup.Size = new System.Drawing.Size(75, 23);
            this.btn_save_setup.TabIndex = 20;
            this.btn_save_setup.Text = "Lưu cài đặt";
            this.btn_save_setup.UseVisualStyleBackColor = true;
            this.btn_save_setup.Click += new System.EventHandler(this.btn_save_setup_Click);
            // 
            // cbdeelay
            // 
            this.cbdeelay.BackColor = System.Drawing.SystemColors.Window;
            this.cbdeelay.FormattingEnabled = true;
            this.cbdeelay.Location = new System.Drawing.Point(164, 84);
            this.cbdeelay.Name = "cbdeelay";
            this.cbdeelay.Size = new System.Drawing.Size(121, 21);
            this.cbdeelay.TabIndex = 21;
            this.cbdeelay.Text = "Chọn";
            // 
            // cbamount
            // 
            this.cbamount.BackColor = System.Drawing.SystemColors.Window;
            this.cbamount.FormattingEnabled = true;
            this.cbamount.Location = new System.Drawing.Point(164, 112);
            this.cbamount.Name = "cbamount";
            this.cbamount.Size = new System.Drawing.Size(121, 21);
            this.cbamount.TabIndex = 22;
            this.cbamount.Text = "Chọn";
            // 
            // setupdevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 186);
            this.Controls.Add(this.cbamount);
            this.Controls.Add(this.cbdeelay);
            this.Controls.Add(this.btn_save_setup);
            this.Controls.Add(this.cbnetwork);
            this.Controls.Add(this.lbl_networl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_close_setupdevice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "setupdevice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_close_setupdevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_networl;
        private System.Windows.Forms.ComboBox cbnetwork;
        private System.Windows.Forms.Button btn_save_setup;
        private System.Windows.Forms.ComboBox cbdeelay;
        private System.Windows.Forms.ComboBox cbamount;
    }
}