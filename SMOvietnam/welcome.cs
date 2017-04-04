using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;
using Microsoft.Win32;

namespace SMOvietnam
{
    public partial class welcome : Form
    {

        private bool success = false;
        public welcome()
        {
            InitializeComponent();
            //Doc lai Username vaf Password
            RegistryKey keIsLogin = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");

            if (keIsLogin != null)
            {
                var Username = keIsLogin.GetValue("keyUsername");
                var Password = keIsLogin.GetValue("keyPassword");
                if (Username != "" && Password != "")
                {
                    login(Username.ToString(), Library.Base64Decode(Password.ToString()));
                }
            }
        }

        private void SpLashScreen()
        {
            Application.Run(new SplashScreen());
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_acc.Text = "";
            txt_pass.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string username = "";
            string password = "";
            login(username, password);
        }


        private void login(string username, string password)
        {
            notofication_login.Visible = true;
            try
            {
                if (txt_acc.Text == "")
                {
                    notofication_login.Visible = false;
                    MessageBox.Show("Tài khoản không được để trống.");
                }
                else if (txt_pass.Text == "")
                {
                    notofication_login.Visible = false;
                    MessageBox.Show("Mật khẩu không được để trống.");
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    Library.username = username != "" ? username : txt_acc.Text;
                    string _password = password != "" ? password : txt_pass.Text;
                    //MessageBox.Show("" + Library.uuiduser + "");
                    Uri uri = new Uri(Library.apiUrl + "/login?username=" + Library.username + "&password=" + _password + "&bios=" + Library.uuiduser);
                    HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
                    requestFile.ContentType = "application/json";
                    HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
                    //Console.WriteLine(Library.apiUrl + "/login?username=" + Library.username + "&password=" + _password + "&bios=" + Library.uuiduser);
                    if (requestFile.HaveResponse)
                    {
                        //notofication_login.Visible = false;
                        if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                        {
                            StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                            dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                            int status = dynObj.status;
                            if (status == 200)
                            {
                                success = true;
                                if (saveaccount.Checked)
                                {
                                    RegistryKey keylogin = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                                    keylogin.SetValue("keyUsername", Library.username);
                                    keylogin.SetValue("keyPassword", Library.Base64Encode(_password));
                                }
                                else {
                                    RegistryKey keylogin = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                                    keylogin.SetValue("keyUsername", "");
                                    keylogin.SetValue("keyPassword", "");
                                }
                                

                                main m = new main();
                                m.Show();
                                this.Hide();
                                // Do ko hidden duoc form khi goi auto nen phai su dung cach nay 
                                this.WindowState = FormWindowState.Minimized; // ddafu tien gat no xuong
                                this.ShowInTaskbar = false; // Khong cho no show o taskbar
                                this.Visible = false; // hieern thi cung ko cho
                                // done
                                
                            }
                            else if (status == 202)
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Sai tài khoản hoặc mật khẩu.");
                            }
                            else if (status == 207)
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Sai tài khoản hoặc mật khẩu.");
                            }
                            else if (status == 203)
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Vui lòng chọn mua gói tài khoản sử dụng cho tài khoản này.");
                            }
                            else if (status == 204)
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Quá số hạn máy cho phép ở tài khoản này.");
                            }
                            else if (status == 205)
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Tài khoản hết hạn.");
                            }
                            else if (status == 206)
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Tài khoản đang bị khóa, vui lòng liên hệ hỗ trợ để được trợ giúp.");
                            }
                            else
                            {
                                notofication_login.Visible = false;
                                MessageBox.Show("Sai tài khoản hoặc mật khẩu.");
                            }
                        }
                        else
                        {
                            notofication_login.Visible = false;
                            MessageBox.Show("Sự cố kết nối máy chủ, vui lòng kiểm tra lại internet của bạn.\nHoặc liên hệ hỗ trợ: support@beecom.vn.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (success)
                {
                    main m = new main();
                    m.Show();
                    this.Hide();
                    // Do ko hidden duoc form khi goi auto nen phai su dung cach nay 
                    this.WindowState = FormWindowState.Minimized; // ddafu tien gat no xuong
                    this.ShowInTaskbar = false; // Khong cho no show o taskbar
                    this.Visible = false; // hieern thi cung ko cho
                }
            }
        }


        private void welcome_Load(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(Library.apiUrl + "/checkstatus");
                HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
                requestFile.ContentType = "application/json";

                HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
                if (requestFile.HaveResponse)
                {
                    if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                    {
                        StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                        dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                        
                        int status = dynObj.status;
                        if (status != 0 && status != 1)
                        {
                            MessageBox.Show("Sự cố kết nối máy chủ, vui lòng kiểm tra lại internet của bạn.\nHoặc liên hệ hỗ trợ: support@beecom.vnm", "Thông báo");
                            Application.Exit();
                        }
                        else if (status == 0)
                        {
                            string msg = dynObj.msg;
                            int free = dynObj.free;
                            int update = dynObj.update;
                            string caption = dynObj.caption;
                            if (update == 1)
                            {
                                MessageBox.Show(msg, caption);
                            }
                            else if (free == 0)
                            {
                                MessageBox.Show(msg, caption);
                            }
                            Application.Exit();
                        }
                        else
                        {
                            btn_login.Enabled = true;
                            btn_cancel.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sự cố kết nối máy chủ, vui lòng kiểm tra lại internet của bạn.\nHoặc liên hệ hỗ trợ: support@beecom.vn", "Thông báo");
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message + "\nSự cố kết nối máy chủ, vui lòng kiểm tra lại internet của bạn.\nHoặc liên hệ hỗ trợ: support@beecom.vn", "Thông báo");
                Application.Exit();
            }

        }

        Boolean flag; int x, y;
        private void welcome_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
            x = e.X;
            y = e.Y;
        }

        private void welcome_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void welcome_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                this.SetDesktopLocation(Cursor.Position.X - x, Cursor.Position.Y - y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txt_acc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login(txt_acc.Text, txt_pass.Text);
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login(txt_acc.Text, txt_pass.Text);
            }
        }
    }
}
