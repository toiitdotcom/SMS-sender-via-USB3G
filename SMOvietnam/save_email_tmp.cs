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

namespace SMOvietnam
{
    public partial class save_email_tmp : Form
    {
        main mainForm;

        // modify proccess background
        private BackgroundWorker saveTmpMailBackground = new BackgroundWorker();

        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;

        private void saveTmpMail()
        {
            saveTmpMailBackground.DoWork += new DoWorkEventHandler(saveTmpMailBackground_DoWork);
            saveTmpMailBackground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(saveTmpMailBackground_RunWorkerCompleted);
            saveTmpMailBackground.ProgressChanged += new ProgressChangedEventHandler(saveTmpMailBackground_ProgressChanged);
            saveTmpMailBackground.WorkerReportsProgress = true; saveTmpMailBackground.WorkerSupportsCancellation = true;
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        protected void saveTmpMailBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event

                panel_save_tmp_mail.Invoke(new MethodInvoker(delegate { panel_save_tmp_mail.Visible = true; }));
                btn_save_mail_tmp.Invoke(new MethodInvoker(delegate { btn_save_mail_tmp.Text = "Đang lưu..."; }));
                btn_save_mail_tmp.Invoke(new MethodInvoker(delegate { btn_save_mail_tmp.Enabled = false; }));
                lbl_show_ss.Invoke(new MethodInvoker(delegate { lbl_show_ss.Visible = true; }));
                
                int id_next = _getNextID();
                string txtSQLQuery = "INSERT INTO email_campaign (id, email_name, type_save) VALUES (" + id_next + ", '" + txt_name_tmp.Text + "', 0);";
                ExecuteQuery(txtSQLQuery);

                RegistryKey get_regedit = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
                string keysmtp = get_regedit.GetValue("typesmtp").ToString();
                string chkenablereport = get_regedit.GetValue("chkenablereport").ToString();
                if (chkenablereport == "")
                {
                    chkenablereport = "0";
                }
                string chkdennymail = get_regedit.GetValue("chkdennymail").ToString();
                if (chkdennymail == "")
                {
                    chkdennymail = "0";
                }

                string mail_object = "";
                if (mainForm.txt_subject_email.InvokeRequired)
                {
                    mainForm.txt_subject_email.Invoke(new MethodInvoker(delegate
                    {
                         mail_object = mainForm.txt_subject_email.Text;
                    }));
                }


                string _HTMLEditors = "";
                if (mainForm.HTMLEditors.InvokeRequired)
                {
                    mainForm.HTMLEditors.Invoke(new MethodInvoker(delegate
                    {
                         _HTMLEditors = Base64Encode(mainForm.HTMLEditors.DocumentText);
                    }));
                }

                string _txtSQLQuery_file_attack_mail = "UPDATE file_attack SET id_mail_campaign = " + id_next + " WHERE id_mail_add = " + Library._mail_add + ";";
                ExecuteQuery(_txtSQLQuery_file_attack_mail);


                string _txtSQLQuery = "INSERT INTO content_mail_campaign (mail_object, mail_fullname, typesmtp, content, id_mail_campaign, open_mail, denny_mail, id_mail_add) VALUES ('" + mail_object + "', '" + Library._name_send_mail + "','" + keysmtp + "','" + _HTMLEditors + "', " + id_next + ", " + chkenablereport + ", " + chkdennymail + ", " + Library._mail_add + ");";
                ExecuteQuery(_txtSQLQuery);

                if (mainForm.dgvEmailListSend.InvokeRequired)
                {
                    mainForm.dgvEmailListSend.Invoke(new MethodInvoker(delegate
                    {
                        sqlite_conn.Open();
                        sqlite_cmd = sqlite_conn.CreateCommand();
                        List<DataGridViewRow> listEmailSend = (from rows_ in mainForm.dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                               where Convert.ToBoolean(rows_.Cells["stt_sendmail"].Value) == true
                                                               select rows_).ToList();
                        foreach (DataGridViewRow rowemailEmailSendy in listEmailSend)
                        {
                            string __txtSQLQuery = "INSERT INTO mail_list_campaign (email_name, fullname, type_mail, id_mail_campaign) VALUES ('" + rowemailEmailSendy.Cells["email"].Value.ToString() + "', '" + rowemailEmailSendy.Cells["fullname_list_email"].Value.ToString() + "', 1, " + id_next + ");";
                            //ExecuteQuery(__txtSQLQuery);
                            sqlite_cmd.CommandText = __txtSQLQuery;
                            sqlite_cmd.ExecuteNonQuery();
                        }
                        sqlite_conn.Close();
                    }));
                }


                MessageBox.Show("Lưu nháp hoàn thành.");
                this.Invoke(new MethodInvoker(delegate { this.Close(); }));
                
            }
            catch (Exception ex)
            {
                sqlite_conn.Close();
                MessageBox.Show(ex.Message);
                panel_save_tmp_mail.Invoke(new MethodInvoker(delegate { panel_save_tmp_mail.Visible = false; }));
                btn_save_mail_tmp.Invoke(new MethodInvoker(delegate { btn_save_mail_tmp.Text = "Lưu lại"; }));
                btn_save_mail_tmp.Invoke(new MethodInvoker(delegate { btn_save_mail_tmp.Enabled = true; }));
                saveTmpMailBackground.CancelAsync();
            }
        }
        protected void saveTmpMailBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void saveTmpMailBackground_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        public save_email_tmp(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
            saveTmpMail();
            sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");
        }
        private void ExecuteQuery(string txtQuery)
        {
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = txtQuery;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        private int _getNextID()
        {
            SQLiteDataReader sqlite_datareader;

            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT max(id) AS ids FROM email_campaign";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            int _id_next = 0;
            while (sqlite_datareader.Read())
            {
                _id_next = Convert.ToInt32(sqlite_datareader["ids"]) + 1;
            }
            sqlite_conn.Close();
            return _id_next;
        }
        
        


        private void btn_save_mail_tmp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_name_tmp.Text != "")
                {
                    if (!saveTmpMailBackground.IsBusy)
                    {
                        saveTmpMailBackground.RunWorkerAsync();
                    }
                }
                else {
                    MessageBox.Show("Vui lòng nhập tên nháp");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
