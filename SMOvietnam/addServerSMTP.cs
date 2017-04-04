using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

namespace SMOvietnam
{
    public partial class addServerSMTP : Form
    {
        main mainForm;
        RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
        private BackgroundWorker checkMailBackground = new BackgroundWorker();
        public addServerSMTP(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
            RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
            if (key_regedit12 != null)
            {
                if (key_regedit12.GetValue("chkenablereport").ToString() == "1")
                {
                    chkenablereport.Checked = true;
                }
                if (key_regedit12.GetValue("chkdennymail").ToString() == "1")
                {
                    chkdennymail.Checked = true;
                }

                    if(key_regedit12.GetValue("typesmtp") != null)
                    {
                        string keysmtp = key_regedit12.GetValue("typesmtp").ToString();
                        if (key_regedit12.GetValue("glbl_server_smtp_send") != null)
                        {
                            txtsmtpgmail.Text = key_regedit12.GetValue("glbl_server_smtp").ToString();
                            txtportsmtpgmail.Text = key_regedit12.GetValue("glbl_port_smtp_user").ToString();
                            txtaccountsmtpgmail.Text = key_regedit12.GetValue("glbl_server_smtp_send").ToString();
                            txtsmtpmatkhaugmail.Text = key_regedit12.GetValue("glbl_pass_smtp").ToString();
                            txt_gmail_name_send.Text = key_regedit12.GetValue("gmail_name_send").ToString();
                            if (keysmtp == "rgmailsmtp" && keysmtp != null)
                            {
                                panelgmail.Visible = true;
                                rmysmtp.Checked = false;
                                rgmailsmtp.Checked = true;
                                ramazonsmtp.Checked = false;
                                panel2.Visible = false;
                                pamazon.Visible = false;
                                panelgmail.Visible = true;
                            }
                        }
                        //else 
                            if (key_regedit12.GetValue("aaddressemail") != null)
                        {
                            if (keysmtp == "ramazonsmtp" && keysmtp != null)
                            {
                                rmysmtp.Checked = false;
                                rgmailsmtp.Checked = false;
                                ramazonsmtp.Checked = true;

                                panel2.Visible = false;
                                panelgmail.Visible = false;
                                pamazon.Visible = true;
                            }
                            txtamailaddressamazon.Text = key_regedit12.GetValue("aaddressemail").ToString();
                            txt_amazon_name_send.Text = key_regedit12.GetValue("amazon_name_send").ToString();
                               
                        }
                        //else
                            if (key_regedit12.GetValue("lbl_server_smtp_send") != null)
                        {
                            txt_popup_server_smtp.Text = key_regedit12.GetValue("lbl_server_smtp").ToString();
                            txt_popup_port_smtp.Text = key_regedit12.GetValue("lbl_port_smtp_user").ToString();
                            txt_popup_acc_smtp.Text = key_regedit12.GetValue("lbl_server_smtp_send").ToString();
                            txt_popup_pass_smtp.Text = key_regedit12.GetValue("lbl_pass_smtp").ToString();
                            addressemail.Text = key_regedit12.GetValue("addressemail").ToString();
                            cbBoxSSL.SelectedIndex = cbBoxSSL.Items.IndexOf(key_regedit12.GetValue("cbBoxSSL"));
                            txt_my_name_send.Text = key_regedit12.GetValue("my_name_send").ToString();

                            if (keysmtp == "rmysmtp" && keysmtp != null)
                            {
                                rmysmtp.Checked = true;
                                rgmailsmtp.Checked = false;
                                ramazonsmtp.Checked = false;

                                pamazon.Visible = false;
                                panelgmail.Visible = false;
                                panel2.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        
                        rmysmtp.Checked = true;
                        rgmailsmtp.Checked = false;
                        ramazonsmtp.Checked = false;


                        pamazon.Visible = false;
                        panelgmail.Visible = false;
                        panel2.Visible = true;
                    }

              checkMail();     
                
            }
        }

        private void checkMail()
        {
            checkMailBackground.DoWork += new DoWorkEventHandler(checkMailBackground_DoWork);
            checkMailBackground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(checkMailBackground_RunWorkerCompleted);
            checkMailBackground.ProgressChanged += new ProgressChangedEventHandler(checkMailBackground_ProgressChanged);
            checkMailBackground.WorkerReportsProgress = true; checkMailBackground.WorkerSupportsCancellation = true;
        }

        protected void checkMailBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event


                btn_smtp_check.Invoke(new MethodInvoker(delegate { btn_smtp_check.Text = "Đang kiểm tra..."; }));
                string _smtpsmtp = "";
                string _portsmtp = "";
                string _usernamesmtp = "";
                string _passwordsmtp = "";
                string _addressmailsmtp = "";
                string _typemethodsmtp = "";
                try
                {
                    if (rmysmtp.Checked == true)
                    {
                        Object selectedItem = cbBoxSSL.SelectedItem;
                        if (addressemail.Text == "Default@email.com" || addressemail.Text == "email@cuaban.com" || this.txt_popup_server_smtp.Text == "smtp.congtycuaban.com" || this.txt_popup_acc_smtp.Text == "email@congtycuaban.com" || this.txt_popup_pass_smtp.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }

                        else if (!IsValid(addressemail.Text))
                        {
                            MessageBox.Show("Address Email sai định dạng .");
                        }
                        else if (selectedItem.ToString() != "HTTP" && selectedItem.ToString() != "HTTPS")
                        {
                            MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                        }
                        else if (txt_my_name_send.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào tên người gửi.");
                        }
                        else if (this.txt_popup_server_smtp.Text != string.Empty)
                        {
                            _smtpsmtp = this.txt_popup_server_smtp.Text;
                            _portsmtp = this.txt_popup_port_smtp.Text;
                            _usernamesmtp = this.txt_popup_acc_smtp.Text;
                            _passwordsmtp = this.txt_popup_pass_smtp.Text;
                            _addressmailsmtp = addressemail.Text;
                            _typemethodsmtp = selectedItem.ToString();
                        }
                    }
                    else if (rgmailsmtp.Checked == true)
                    {
                        Object selectedItem = cbBoxSSL.SelectedItem;
                        if (txtaccountsmtpgmail.Text == "Default@email.com" || txtaccountsmtpgmail.Text == "email@gmail.com" || this.txtsmtpgmail.Text == "" || this.txtsmtpmatkhaugmail.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }
                        else if (!IsValid(addressemail.Text))
                        {
                            MessageBox.Show("Address Email sai định dạng .");
                        }
                        else if (txt_gmail_name_send.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào tên người gửi.");
                        }
                        else if (this.txtsmtpgmail.Text != string.Empty)
                        {
                            _smtpsmtp = this.txtsmtpgmail.Text;
                            _portsmtp = this.txtportsmtpgmail.Text;
                            _usernamesmtp = this.txtaccountsmtpgmail.Text;
                            _passwordsmtp = this.txtsmtpmatkhaugmail.Text;
                            _addressmailsmtp = txtaccountsmtpgmail.Text;
                            _typemethodsmtp = "HTTPS";
                        }
                    }
                    else if (ramazonsmtp.Checked == true)
                    {
                        if (txtamailaddressamazon.Text == "Default@email.com")
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }
                        else if (!IsValid(txtamailaddressamazon.Text))
                        {
                            MessageBox.Show("Address Email sai định dạng .");
                        }
                        else if (txt_amazon_name_send.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào tên người gửi.");
                        }
                        else
                        {
                            RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
                            _smtpsmtp = key_regedit12.GetValue("albl_server_smtp").ToString();
                            _portsmtp = key_regedit12.GetValue("albl_port_smtp_user").ToString();
                            _usernamesmtp = key_regedit12.GetValue("albl_server_smtp_send").ToString();
                            _passwordsmtp = key_regedit12.GetValue("albl_pass_smtp").ToString();
                            _addressmailsmtp = txtamailaddressamazon.Text;
                            _typemethodsmtp = "HTTPS";
                            key_regedit12.Close();
                        }
                    }
                    MailMessage mail = new MailMessage();
                    string smtpclient = _smtpsmtp;
                    SmtpClient SmtpServer = new SmtpClient(smtpclient);
                    string accsmtp = _usernamesmtp;
                    string htmlEmail = "";
                    mail.From = new MailAddress(_addressmailsmtp);

                    mail.To.Add("xappvn.com@gmail.com");
                    mail.Subject = "testmail";
                    mail.IsBodyHtml = true;

                    htmlEmail = "testmai";

                    mail.Body = htmlEmail;

                    SmtpServer.Port = Convert.ToInt32(_portsmtp);
                    string pas_smtp = _passwordsmtp;

                    if (_typemethodsmtp == "HTTPS")
                    {
                        SmtpServer.EnableSsl = true;
                    }
                    else
                    {
                        SmtpServer.EnableSsl = false;
                    }

                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(accsmtp, pas_smtp);


                    SmtpServer.Send(mail);
                    SmtpServer.Dispose();
                    MessageBox.Show("Tài khoản SMTP hợp lệ, bạn có thể bắt đầu gửi email");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tài khoản SMTP không hợp lệ vui lòng kiểm tra lại");
                }

                btn_smtp_check.Invoke(new MethodInvoker(delegate { btn_smtp_check.Text = "Kiểm tra SMTP"; }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void checkMailBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void checkMailBackground_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void btnAddServerSmtpPopup_Click(object sender, EventArgs e)
        {
            try
            {
                Object selectedItem = cbBoxSSL.SelectedItem;
                if (addressemail.Text == "Default@email.com" || addressemail.Text == "email@cuaban.com" || this.txt_popup_server_smtp.Text == "smtp.congtycuaban.com" || this.txt_popup_acc_smtp.Text == "email@congtycuaban.com" || this.txt_popup_pass_smtp.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                }
                else if (!IsValid(addressemail.Text))
                {
                    MessageBox.Show("Address Email sai định dạng .");
                }
                else if (selectedItem.ToString() != "HTTP" && selectedItem.ToString() != "HTTPS")
                {
                    MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                }
                else if (this.txt_popup_server_smtp.Text != string.Empty)
                {
                    if (selectedItem.ToString() == "HTTP")
                    {
                        mainForm.lbl_ssl.Text = "0";
                    }
                    else
                    {
                        mainForm.lbl_ssl.Text = "1";
                    }
                    mainForm.lbl_server_smtp.Text = this.txt_popup_server_smtp.Text;


                    RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    //storing the values  
                    keyt123.SetValue("lbl_server_smtp", this.txt_popup_server_smtp.Text);
                    keyt123.SetValue("lbl_port_smtp_user", this.txt_popup_port_smtp.Text);
                    keyt123.SetValue("lbl_server_smtp_send", this.txt_popup_acc_smtp.Text);
                    keyt123.SetValue("lbl_pass_smtp", this.txt_popup_pass_smtp.Text);
                    keyt123.SetValue("cbBoxSSL", cbBoxSSL.SelectedItem);
                    keyt123.SetValue("typesmtp", "rmysmtp");
                    keyt123.SetValue("addressemail", addressemail.Text);

                    mainForm.lbl_port_smtp_user.Text = this.txt_popup_port_smtp.Text;
                    mainForm.lbl_server_smtp_send.Text = addressemail.Text;
                    mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                    mainForm.lbl_pass_smtp.Text = this.txt_popup_pass_smtp.Text;
                    key_regedit12.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                string _SError = ex.Message;
                if (_SError == "Object reference not set to an instance of an object.")
                {
                    MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                }else{
                    MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                }
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

        private void addServerSMTP_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(addServerSMTP_KeyDown);
        }

        void addServerSMTP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                MessageBox.Show("Để gửi được email, bạn vui lòng nhập địa chỉ SMTP Server.\nBạn có thể nhập địa chỉ SMTP miễn phí của google, Yahoo hoặc Server mail của bạn. \nServer SMTP:  là máy chủ hỗ trợ bạn gửi mail tới các địa chỉ e-mail khác trên Internet. Ví dụ: smtp.congtycuaban.com \nPort SMTP: mặc định là port 25, port này có thể được thay đổi theo cấu hình server của bạn.\nMọi thắc mắc xin gửi về email: support@smovietnam.com.");
            }
        }

        private void btn_close_addserversmtp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_addgmail_Click(object sender, EventArgs e)
        {
            try
            {
                Object selectedItem = cbBoxSSL.SelectedItem;
                if (txtaccountsmtpgmail.Text == "Default@email.com" || txtaccountsmtpgmail.Text == "email@gmail.com" || this.txtsmtpgmail.Text == "" || this.txtsmtpmatkhaugmail.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                }
                else if (!IsValid(addressemail.Text))
                {
                    MessageBox.Show("Address Email sai định dạng .");
                }
                else if (this.txtsmtpgmail.Text != string.Empty)
                {
                    mainForm.lbl_ssl.Text = "1";

                    mainForm.lbl_server_smtp.Text = this.txtsmtpgmail.Text;


                    RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    //storing the values  
                    keyt123.SetValue("glbl_server_smtp", this.txtsmtpgmail.Text);
                    keyt123.SetValue("glbl_port_smtp_user", this.txtportsmtpgmail.Text);
                    keyt123.SetValue("glbl_server_smtp_send", this.txtaccountsmtpgmail.Text);
                    keyt123.SetValue("glbl_pass_smtp", this.txtsmtpmatkhaugmail.Text);
                    keyt123.SetValue("typesmtp", "rgmailsmtp");
                    keyt123.SetValue("gcbBoxSSL", "HTTPS");
                    keyt123.SetValue("gaddressemail", this.txtaccountsmtpgmail.Text);

                    mainForm.lbl_port_smtp_user.Text = this.txtportsmtpgmail.Text;
                    mainForm.lbl_server_smtp_send.Text = this.txtaccountsmtpgmail.Text;
                    mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                    mainForm.lbl_pass_smtp.Text = this.txtsmtpmatkhaugmail.Text;
                    key_regedit12.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                string _SError = ex.Message;
                if (_SError == "Object reference not set to an instance of an object.")
                {
                    MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                }
            }
        }

        private void rmysmtp_CheckedChanged(object sender, EventArgs e)
        {
            if (rmysmtp.Checked)
            {
                pamazon.Visible = false;
                panelgmail.Visible = false;
                panel2.Visible = true;
            }
        }

        private void rgmailsmtp_CheckedChanged(object sender, EventArgs e)
        {
            if (rgmailsmtp.Checked)
            {
                
                panel2.Visible = false;

                pamazon.Visible = false; 
                panelgmail.Visible = true;
            }
        }

        private void btnaddamazon_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtamailaddressamazon.Text == "Default@email.com")
                {
                    MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                }
                else if (!IsValid(txtamailaddressamazon.Text))
                {
                    MessageBox.Show("Address Email sai định dạng .");
                }
                else
                {
                    mainForm.lbl_server_smtp.Text = this.txtsmtpgmail.Text;

                    RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
                    keyt123.SetValue("aaddressemail", this.txtamailaddressamazon.Text);
                    keyt123.SetValue("typesmtp", "ramazonsmtp");
                    mainForm.lbl_port_smtp_user.Text = key_regedit12.GetValue("albl_server_smtp").ToString();
                    mainForm.lbl_server_smtp_send.Text = this.txtamailaddressamazon.Text;
                    mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                    mainForm.lbl_pass_smtp.Text = key_regedit12.GetValue("albl_pass_smtp").ToString();
                    key_regedit12.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ramazonsmtp_CheckedChanged(object sender, EventArgs e)
        {
            if (ramazonsmtp.Checked)
            {
                
                panel2.Visible = false;
                panelgmail.Visible = false; 
                pamazon.Visible = true;

                try
                {
                    Uri uri = new Uri(Library.apiUrl + "/amazon-info");
                    HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
                    requestFile.ContentType = "application/json";
                    HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
                    if (requestFile.HaveResponse)
                    {
                        //notofication_login.Visible = false;
                        if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                        {
                            hideIsProcessAmazonApi.Visible = false;
                            lblsetupamazon.Text = "";
                            StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                            dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                            //storing the values  
                            keyt123.SetValue("albl_server_smtp", dynObj.smtp);
                            keyt123.SetValue("albl_port_smtp_user", dynObj.port);
                            keyt123.SetValue("albl_server_smtp_send", dynObj.username);
                            keyt123.SetValue("albl_pass_smtp", dynObj.password);
                            
                            keyt123.SetValue("acbBoxSSL", "HTTPS");
                            /*
                             keyt123.SetValue("aaddressemail", this.txtamailaddressamazon.Text);

                            mainForm.lbl_port_smtp_user.Text = this.txtamailaddressamazon.Text;
                            mainForm.lbl_server_smtp_send.Text = dynObj.smtp;
                            mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                            mainForm.lbl_pass_smtp.Text = this.txtsmtpmatkhaugmail.Text;
                             */
                            key_regedit12.Close();
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng kiểm tra lại internet.");
                        }
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/settings/security/lesssecureapps");
        }

        private void btn_save_smtp_Click(object sender, EventArgs e)
        {
            try
            {
                if (rmysmtp.Checked == true)
                {
                    try
                    {
                        Object selectedItem = cbBoxSSL.SelectedItem;
                        if (addressemail.Text == "Default@email.com" || addressemail.Text == "email@cuaban.com" || this.txt_popup_server_smtp.Text == "smtp.congtycuaban.com" || this.txt_popup_acc_smtp.Text == "email@congtycuaban.com" || this.txt_popup_pass_smtp.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }

                        else if (!IsValid(addressemail.Text))
                        {
                            MessageBox.Show("Address Email sai định dạng .");
                        }
                        else if (selectedItem.ToString() != "HTTP" && selectedItem.ToString() != "HTTPS")
                        {
                            MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                        }
                        else if (txt_my_name_send.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào tên người gửi.");
                        }
                        else if (this.txt_popup_server_smtp.Text != string.Empty)
                        {
                            if (selectedItem.ToString() == "HTTP")
                            {
                                mainForm.lbl_ssl.Text = "0";
                            }
                            else
                            {
                                mainForm.lbl_ssl.Text = "1";
                            }
                            mainForm.lbl_server_smtp.Text = this.txt_popup_server_smtp.Text;


                            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                            //storing the values  
                            keyt123.SetValue("lbl_server_smtp", this.txt_popup_server_smtp.Text);
                            keyt123.SetValue("lbl_port_smtp_user", this.txt_popup_port_smtp.Text);
                            keyt123.SetValue("lbl_server_smtp_send", this.txt_popup_acc_smtp.Text);
                            keyt123.SetValue("lbl_pass_smtp", this.txt_popup_pass_smtp.Text);
                            keyt123.SetValue("cbBoxSSL", cbBoxSSL.SelectedItem);
                            keyt123.SetValue("typesmtp", "rmysmtp");
                            keyt123.SetValue("addressemail", addressemail.Text);
                            keyt123.SetValue("my_name_send", txt_my_name_send.Text);
                            

                            mainForm.lbl_port_smtp_user.Text = this.txt_popup_port_smtp.Text;
                            mainForm.lbl_server_smtp_send.Text = addressemail.Text;
                            mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                            mainForm.lbl_pass_smtp.Text = this.txt_popup_pass_smtp.Text;
                            key_regedit12.Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string _SError = ex.Message;
                        if (_SError == "Object reference not set to an instance of an object.")
                        {
                            MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }
                    }
                }
                else if (rgmailsmtp.Checked == true)
                {
                    try
                    {
                        Object selectedItem = cbBoxSSL.SelectedItem;
                        if (txtaccountsmtpgmail.Text == "Default@email.com" || txtaccountsmtpgmail.Text == "email@gmail.com" || this.txtsmtpgmail.Text == "" || this.txtsmtpmatkhaugmail.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }
                        else if (!IsValid(addressemail.Text))
                        {
                            MessageBox.Show("Address Email sai định dạng .");
                        }
                        else if (txt_gmail_name_send.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào tên người gửi.");
                        }
                        else if (this.txtsmtpgmail.Text != string.Empty)
                        {
                            mainForm.lbl_ssl.Text = "1";

                            mainForm.lbl_server_smtp.Text = this.txtsmtpgmail.Text;


                            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                            //storing the values  
                            keyt123.SetValue("glbl_server_smtp", this.txtsmtpgmail.Text);
                            keyt123.SetValue("glbl_port_smtp_user", this.txtportsmtpgmail.Text);
                            keyt123.SetValue("glbl_server_smtp_send", this.txtaccountsmtpgmail.Text);
                            keyt123.SetValue("glbl_pass_smtp", this.txtsmtpmatkhaugmail.Text);
                            keyt123.SetValue("typesmtp", "rgmailsmtp");
                            keyt123.SetValue("gcbBoxSSL", "HTTPS");
                            keyt123.SetValue("gaddressemail", this.txtaccountsmtpgmail.Text);
                            keyt123.SetValue("gmail_name_send", txt_gmail_name_send.Text);

                            mainForm.lbl_port_smtp_user.Text = this.txtportsmtpgmail.Text;
                            mainForm.lbl_server_smtp_send.Text = this.txtaccountsmtpgmail.Text;
                            mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                            mainForm.lbl_pass_smtp.Text = this.txtsmtpmatkhaugmail.Text;
                            key_regedit12.Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string _SError = ex.Message;
                        if (_SError == "Object reference not set to an instance of an object.")
                        {
                            MessageBox.Show("Vui lòng chọn giao thức của SMTP.");
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }
                    }
                }
                else if (ramazonsmtp.Checked == true)
                {
                    try
                    {
                        if (txtamailaddressamazon.Text == "Default@email.com")
                        {
                            MessageBox.Show("Vui lòng nhập vào thông tin của các ô nhập.");
                        }
                        else if (!IsValid(txtamailaddressamazon.Text))
                        {
                            MessageBox.Show("Address Email sai định dạng .");
                        }
                        else if (txt_amazon_name_send.Text == "")
                        {
                            MessageBox.Show("Vui lòng nhập vào tên người gửi.");
                        }
                        else
                        {
                            mainForm.lbl_server_smtp.Text = this.txtsmtpgmail.Text;

                            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                            RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
                            keyt123.SetValue("aaddressemail", this.txtamailaddressamazon.Text);
                            keyt123.SetValue("typesmtp", "ramazonsmtp");
                            keyt123.SetValue("amazon_name_send", txt_amazon_name_send.Text);
                            mainForm.lbl_port_smtp_user.Text = key_regedit12.GetValue("albl_server_smtp").ToString();
                            mainForm.lbl_server_smtp_send.Text = this.txtamailaddressamazon.Text;
                            mainForm.lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                            mainForm.lbl_pass_smtp.Text = key_regedit12.GetValue("albl_pass_smtp").ToString();
                            key_regedit12.Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        // set status chk

        int status_chk = 0;
        private void chkenablereport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkenablereport.Checked)
            {
                try
                {
                    RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    keyt123.SetValue("chkenablereport", "1");
                    panelntenablereport.BackColor = Color.FromArgb(232, 223, 44);
                    lblenableemail1.ForeColor = Color.FromArgb(0, 15, 0);
                    lblenableemail2.ForeColor = Color.FromArgb(0, 15, 0);
                    lblenableemail3.ForeColor = Color.FromArgb(0, 15, 0);
                    status_chk++;
                }
                catch { }
            }
            else
            {
                status_chk--;
                RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                keyt123.SetValue("chkenablereport", "");
                if (status_chk == 0)
                {
                    panelntenablereport.BackColor = System.Drawing.Color.LightGray;
                    lblenableemail1.ForeColor = System.Drawing.Color.DarkGray;
                    lblenableemail2.ForeColor = System.Drawing.Color.DarkGray;
                    lblenableemail3.ForeColor = System.Drawing.Color.DarkGray;
                }
            }
        }

        private void chkdennymail_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdennymail.Checked)
            {
                try
                {
                    status_chk++;
                    RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    keyt123.SetValue("chkdennymail", "1");
                    panelntenablereport.BackColor = Color.FromArgb(232, 223, 44);
                    lblenableemail1.ForeColor = Color.FromArgb(0, 15, 0);
                    lblenableemail2.ForeColor = Color.FromArgb(0, 15, 0);
                    lblenableemail3.ForeColor = Color.FromArgb(0, 15, 0);
                }
                catch { }
            }
            else
            {
                status_chk--;
                RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                keyt123.SetValue("chkdennymail", "");
                if (status_chk == 0)
                {
                    panelntenablereport.BackColor = System.Drawing.Color.LightGray;
                    lblenableemail1.ForeColor = System.Drawing.Color.DarkGray;
                    lblenableemail2.ForeColor = System.Drawing.Color.DarkGray;
                    lblenableemail3.ForeColor = System.Drawing.Color.DarkGray;
                }
            }
        }

        private void btn_smtp_check_Click(object sender, EventArgs e)
        {
            if (!checkMailBackground.IsBusy)
            {
                checkMailBackground.RunWorkerAsync();
            }
        }



    }
}
