using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMOvietnam
{
    public partial class addEmailToListCheck : Form
    {
        main mainForm;
        public addEmailToListCheck(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
        }

        private void addEmailToListSend_Load(object sender, EventArgs e)
        {
            
        }

        private void btnAddEmailPopup_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_popup_email_check2.Text != string.Empty)
                {
                    int fv = mainForm.dataGridListMailCheck.Rows.Count;
                    string emailAddress = "";
                    string content = txt_popup_email_check2.Text;
                    string[] words = content.Split(',');

                    foreach (string word in words)
                    {
                         emailAddress = word;
                         //if (IsValid(this.txt_popup_email.Text))
                         //{
                         mainForm.dataGridListMailCheck.Rows.Add();
                         mainForm.dataGridListMailCheck.Rows[fv].Cells[0].Value = true;
                         mainForm.dataGridListMailCheck.Rows[fv].Cells[1].Value = emailAddress;
                         mainForm.dataGridListMailCheck.Rows[fv].Cells[2].Value = "";
                         mainForm.dataGridListMailCheck.Rows[fv].Cells[3].Value = "Chưa gửi";
                             fv++;
                        // }
                    }
                    txt_popup_email_check2.Text = "";
                    MessageBox.Show("Thêm thành công");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private bool IsValid(string emailaddress)
        {
            try
            {
                var m = new System.Net.Mail.MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void btn_close_addserversmtp_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
