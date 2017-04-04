using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Finisar.SQLite;
namespace SMOvietnam
{
    public partial class addEmailToListSend : Form
    {
        main mainForm;
        public addEmailToListSend(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
        }

        private void addEmailToListSend_Load(object sender, EventArgs e)
        {
            
        }
        private void ExecuteQuery(string txtQuery)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = txtQuery;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
        private void btnAddEmailPopup_Click(object sender, EventArgs e)
        {
            try
            {
                int ok = 1;
                if (this.txt_popup_email.Text != string.Empty)
                {
                    int fv = mainForm.dgvEmailListSend.Rows.Count;
                    int _fv = 0;
                    string emailAddress = "";
                    string content = txt_popup_email.Text;
                    string[] words = content.Split(',');

                    foreach (string word in words)
                    {
                         emailAddress = word;
                         if (IsValid(emailAddress))
                         {
                             _fv = fv + 1;
                             mainForm.dgvEmailListSend.Rows.Add();
                             mainForm.dgvEmailListSend.Rows[fv].Cells[1].Value = _fv;
                             mainForm.dgvEmailListSend.Rows[fv].Cells[0].Value = true;
                             mainForm.dgvEmailListSend.Rows[fv].Cells[2].Value = emailAddress;
                             mainForm.dgvEmailListSend.Rows[fv].Cells[3].Value = "";
                             mainForm.dgvEmailListSend.Rows[fv].Cells[4].Value = "Chưa gửi";
                             fv++;
                             string txtSQLQuery = "INSERT INTO emails (email_name, type_list, fullname) VALUES ('" + emailAddress + "', 1, '');";
                             ExecuteQuery(txtSQLQuery);
                         }
                         else {
                             ok = 0;
                         }
                    }
                    if (ok == 0)
                    {
                        MessageBox.Show("Địa chỉ email nhập vào không hợp lệ, vui lòng kiểm tra lại");
                    }
                    else {
                        txt_popup_email.Text = "";
                        MessageBox.Show("Thêm thành công");
                    }
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
