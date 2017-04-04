using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Finisar.SQLite;
using System.IO.Ports;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Threading;
using System.Net.Sockets;
using mshtml;

namespace SMOvietnam
{
    public partial class mailPending : Form
    {
        main mainForm;
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;
        public static IHTMLDocument2 doc;

        public mailPending(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();

            
            sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");

            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM email_campaign WHERE type_save = 0 ORDER BY id DESC";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            int _i_mail = 0;
            ListMailPending.Rows.Clear();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                ListMailPending.Rows.Add();
                ListMailPending.Rows[_i_mail].Cells[0].Value = (_i_mail + 1);
                ListMailPending.Rows[_i_mail].Cells[1].Value = sqlite_datareader["email_name"].ToString();
                ListMailPending.Rows[_i_mail].Cells[4].Value = sqlite_datareader["id"].ToString();
                _i_mail++;
            }
            sqlite_conn.Close();

        }

        private void ListMailPending_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int _id = 0;
                _id = Convert.ToInt32(ListMailPending.Rows[e.RowIndex].Cells[4].Value);
                if (e.ColumnIndex == 2)
                {
                    try
                    {
                        sqlite_conn.Open();
                        sqlite_cmd = sqlite_conn.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT * FROM mail_list_campaign WHERE id_mail_campaign = " + _id;
                        sqlite_datareader = sqlite_cmd.ExecuteReader();

                        int _i_mail = 0;
                        mainForm.dgvEmailListSend.Rows.Clear();
                        while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                        {
                            mainForm.dgvEmailListSend.Rows.Add();
                            mainForm.dgvEmailListSend.Rows[_i_mail].Cells[4].Value = _i_mail;
                            mainForm.dgvEmailListSend.Rows[_i_mail].Cells[0].Value = true;
                            mainForm.dgvEmailListSend.Rows[_i_mail].Cells[1].Value = sqlite_datareader["email_name"].ToString();
                            mainForm.dgvEmailListSend.Rows[_i_mail].Cells[2].Value = sqlite_datareader["fullname"].ToString();

                            String lbl_status_send = "Chưa gửi";

                            if (Convert.ToInt32(sqlite_datareader["type_mail"]) != 1)
                            {
                                lbl_status_send = "Đã gửi";
                                mainForm.dgvEmailListSend.Rows[_i_mail].Cells[3].Style.BackColor = System.Drawing.Color.Green;
                                mainForm.dgvEmailListSend.Rows[_i_mail].Cells[3].Style.ForeColor = System.Drawing.Color.White;

                            }

                            mainForm.dgvEmailListSend.Rows[_i_mail].Cells[3].Value = lbl_status_send;
                            _i_mail++;
                        }
                        sqlite_conn.Close();

                        sqlite_conn.Open();
                        sqlite_cmd = sqlite_conn.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT * FROM content_mail_campaign WHERE id_mail_campaign = " + _id;
                        sqlite_datareader = sqlite_cmd.ExecuteReader();
                        
                        while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                        {
                            doc = mainForm.HTMLEditors.Document.DomDocument as IHTMLDocument2;
                            doc.body.innerHTML = "";
                            mainForm.txt_subject_email.Text = sqlite_datareader["mail_object"].ToString();
                            Library._name_send_mail = sqlite_datareader["mail_fullname"].ToString();

                            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                            keyt123.SetValue("typesmtp", sqlite_datareader["typesmtp"].ToString());
                            keyt123.SetValue("chkenablereport", sqlite_datareader["open_mail"].ToString());
                            keyt123.SetValue("chkdennymail", sqlite_datareader["denny_mail"].ToString());
                            Library._mail_add = Convert.ToInt32(sqlite_datareader["id_mail_add"]);
                            doc.body.innerHTML = Base64Decode(sqlite_datareader["content"].ToString());
                            doc.designMode = "On";

                        }

                        sqlite_conn.Close();
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        sqlite_conn.Close();
                        //MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string txtSQLQuery = "DELETE FROM email_campaign WHERE id = '" + _id + "'";
                        ExecuteQuery(txtSQLQuery);

                        string _txtSQLQuery = "DELETE FROM mail_list_campaign WHERE id_mail_campaign = '" + _id + "'";
                        ExecuteQuery(_txtSQLQuery);

                        string __txtSQLQuery = "DELETE FROM content_mail_campaign WHERE id_mail_campaign = '" + _id + "'";
                        ExecuteQuery(__txtSQLQuery);

                        sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");

                        sqlite_conn.Open();
                        sqlite_cmd = sqlite_conn.CreateCommand();
                        sqlite_cmd.CommandText = "SELECT * FROM email_campaign WHERE type_save = 0 ORDER BY id DESC";
                        sqlite_datareader = sqlite_cmd.ExecuteReader();

                        int _i_mail = 0;
                        ListMailPending.Rows.Clear();
                        while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                        {
                            ListMailPending.Rows.Add();
                            ListMailPending.Rows[_i_mail].Cells[0].Value = (_i_mail + 1);
                            ListMailPending.Rows[_i_mail].Cells[1].Value = sqlite_datareader["email_name"].ToString();
                            ListMailPending.Rows[_i_mail].Cells[4].Value = sqlite_datareader["id"].ToString();
                            _i_mail++;
                        }
                        sqlite_conn.Close();
                    }
                }
            }
            catch {
                sqlite_conn.Close();
                this.Close();
            }
        }

        private void ExecuteQuery(string txtQuery)
        {
            SQLiteConnection __sqlite_conn;
            SQLiteCommand __sqlite_cmd;
            __sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");
            __sqlite_conn.Open();
            __sqlite_cmd = __sqlite_conn.CreateCommand();
            __sqlite_cmd.CommandText = txtQuery;
            __sqlite_cmd.ExecuteNonQuery();
            __sqlite_conn.Close();
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
