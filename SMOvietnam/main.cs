using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.IO.Ports;
using System.IO;
using System.Data.OleDb;
using System.Net.Mail;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using Microsoft.Win32;
using System.Threading;
using System.Drawing.Text;
using System.Collections;
using System.Net;
using Newtonsoft.Json.Linq;
using Finisar.SQLite;
using System.Net;

namespace SMOvietnam
{
    public partial class main : Form
    {
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
        public static IHTMLDocument2 doc;

        // modify proccess background
        private BackgroundWorker runScanCOM = new BackgroundWorker();
        private BackgroundWorker runSendSmsBakground = new BackgroundWorker();
        private BackgroundWorker runSendEmailBakground = new BackgroundWorker();
        private BackgroundWorker runEmailVerifyBakground = new BackgroundWorker();
        private BackgroundWorker getListEmailReportBackground = new BackgroundWorker();

        // SMS variable
        private bool radndomsms = false;
        public int mobifone = 0;
        public int vinaphone = 0;
        public int viettel = 0;
        public int beeline = 0;
        public int vietnamobile = 0;
        public bool startsmssend = false;
        public bool _showtabfb = false;
        public bool _showtabcontact = false;
        public bool _showtabftk = false;
        public bool Windowmax = false;
        public int stt_send = 0;
        private int i_checkmail = 0;
        private int i_mail = 0;
        private int i_mobile = 0;
        public string Comname { get; private set; }
        public string networkName { get; private set; }
        public int delaydevice { get; private set; }
        public int maxdevice { get; private set; }
        public string deviceRowCurrent { get; private set; }
        string namePorts = "";
        int NumberDevice = 0;
        int sum_sms_send_curent = 0;
        bool selectCOM = false;
        bool senddingsms = true;
        bool isShowDeviceNotSelected = true;
        int rowSMSList = 0;
        int sendSMSSuccess = 0;
        private bool sendFailer = false;
        int sum_emailverifys_send_curent = 0;
        public int rowEMAILList = 0;
        bool senddingEmail = true;
        bool isStopSendMail = false;
        bool checkvaldationmail = false;
        int rowEmailListVerify = 0;
        bool senddingEmailVerify = true;
        bool isStopVerifyMail = false;
        int numberVerify = 0;
        string _smtpsmtp = "";
        string _portsmtp = "";
        string _usernamesmtp = "";
        string _passwordsmtp = "";
        string _addressmailsmtp = "";
        string _typemethodsmtp = "";
        private int sum_emai_send_curent = 0;
        private bool _isStopSendMail = false;

        private int sendFail = 0;

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

        public main()
        {
            InitializeComponent();

            // set multi selection for datagird hahaha
            dgvEmailListSend.MultiSelect = true;
            dgvEmailListSend.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvMobile.MultiSelect = true;
            dgvMobile.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridListMailCheck.MultiSelect = true;
            dataGridListMailCheck.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // Set lại không cho tự gửi sms nữa

            RegistryKey setIntervalKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
            setIntervalKey.SetValue("isRunBookActionSendSMS", 1);

            // Hỏi lại xem, có muốn gửi nữa ko 
            checkSMSBookOld();

            int count_txt_content_sms = txt_box_content_sms.TextLength;
            lbl_char_sms.Text = "Ký tự: " + count_txt_content_sms + " / " + ((count_txt_content_sms / 161) + 1);
            //lbl_sms_system.Text = Library.checkMsg();
            modifyRunScanCom();
            modifySendSmsBakground();
            getListEmailReport();

            RegistryKey key_regedit1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
            if (key_regedit1 != null)
            {
                // Set lại file đính kèm
                RegistryKey setKeyFileAttackMail = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");

                if (key_regedit1.GetValue("gmail_name_send") == null)
                {
                    setKeyFileAttackMail.SetValue("gmail_name_send", "");
                }

                if (key_regedit1.GetValue("amazon_name_send") == null)
                {
                    setKeyFileAttackMail.SetValue("amazon_name_send", "");
                }

                if (key_regedit1.GetValue("my_name_send") == null)
                {
                    setKeyFileAttackMail.SetValue("my_name_send", "");
                }

                if (key_regedit1.GetValue("chkenablereport") == null)
                {
                    setKeyFileAttackMail.SetValue("chkenablereport", "");
                }

                if (key_regedit1.GetValue("chkdennymail") == null)
                {
                    setKeyFileAttackMail.SetValue("chkdennymail", "");
                }

                if (key_regedit1.GetValue("typesmtp") != null)
                {
                    string keysmtp = key_regedit1.GetValue("typesmtp").ToString();
                    if (keysmtp == "rgmailsmtp")
                    {
                        if (key_regedit1.GetValue("gaddressemail") != null)
                        {
                            lbl_server_smtp_send.Text = key_regedit1.GetValue("gaddressemail").ToString();
                            lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                        }
                       
                    }
                    else if (keysmtp == "ramazonsmtp")
                    {
                        if (key_regedit1.GetValue("aaddressemail") != null)
                        {
                            lbl_server_smtp_send.Text = key_regedit1.GetValue("aaddressemail").ToString();
                            lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                    else
                    {
                        if (key_regedit1.GetValue("addressemail") != null)
                        {
                            lbl_server_smtp_send.Text = key_regedit1.GetValue("addressemail").ToString();
                            lbl_server_smtp_send.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                }

                if (key_regedit1.GetValue("emailcheck") != null)
                {
                    txt_account_smtp_verify.Text = key_regedit1.GetValue("emailcheck").ToString();
                }

                if (key_regedit1.GetValue("addressemail") == null)
                {
                    RegistryKey keysetemail = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    keysetemail.SetValue("addressemail", "Default@email.com");
                }
                key_regedit1.Close();
            }
            else
            {
                RegistryKey keyt1 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                keyt1.SetValue("lbl_server_smtp", "smtp.congtycuaban.com");
                keyt1.SetValue("lbl_port_smtp_user", "25");
                keyt1.SetValue("lbl_server_smtp_send", "email@congtycuaban.com");
                keyt1.SetValue("lbl_pass_smtp", "123455");
                keyt1.SetValue("emailcheck", "Nhập địa chỉ email của bạn");
                keyt1.SetValue("chkenablereport", "");
                keyt1.SetValue("chkdennymail", "");
                keyt1.SetValue("gmail_name_send", "");
                keyt1.SetValue("amazon_name_send", "");
                keyt1.SetValue("my_name_send", "");
            }
            modifySendEmailBakground();
            modifyEmailVerifyBakground();
        }
        public bool _sendFailer 
        {
           get 
           {
               return sendFailer; 
           }
           set 
           {
              sendFailer = value; 
           }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void checkSMSBookOld()
        {
            DateTime now = DateTime.Now;
            DateTime dt = DateTime.Parse(now.ToString());
            int timeBook = 0;
            int DateCurrent = 0;
            RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
            if (key_regedit12.GetValue("gio") != null)
            {
                int nam = Convert.ToInt32(key_regedit12.GetValue("nam"));
                int thang = Convert.ToInt32(key_regedit12.GetValue("thang"));
                int ngay = Convert.ToInt32(key_regedit12.GetValue("ngay"));
                int gio = Convert.ToInt32(key_regedit12.GetValue("gio"));
                int phut = Convert.ToInt32(key_regedit12.GetValue("phut"));

                timeBook = nam + thang + ngay + gio + phut;

            }

            int _nam = Convert.ToInt32(dt.Year.ToString());
            int _thang = Convert.ToInt32(dt.Month.ToString());
            int _ngay = Convert.ToInt32(dt.Day.ToString());
            int _gio = Convert.ToInt32(dt.Hour.ToString());
            int _phut = Convert.ToInt32(dt.Minute.ToString());

            DateCurrent = _nam + _thang + _ngay + _gio + _phut;

            if (DateCurrent >= timeBook && Convert.ToInt32(key_regedit12.GetValue("isRunBookActionSendSMS")) == 0)
            {
                lblbook.Text = "Bạn có 1 lịch gửi SMS được chạy vào lúc: " + key_regedit12.GetValue("gio") + ":" + key_regedit12.GetValue("phut") + "   " + key_regedit12.GetValue("ngay") + "/" + key_regedit12.GetValue("thang") + "/" + key_regedit12.GetValue("nam");
                if (MessageBox.Show("Bạn có 1 lịch gửi SMS đã được cài đặt từ trước, bạn có muốn chạy lịch này không ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RegistryKey setContinusIntervalKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                    setContinusIntervalKey.SetValue("isRunBookActionSendSMS", 0);
                }
                else {

                    lblbook.Text = "Lịch gửi SMS của bạn đã bị hủy";
                }
            }
            else {
                if (Convert.ToInt32(key_regedit12.GetValue("isRunBookActionSendSMS")) == 0)
                {
                    lblbook.Text = "Bạn có 1 lịch gửi SMS được chạy vào lúc: " + key_regedit12.GetValue("gio") + ":" + key_regedit12.GetValue("phut") + "   " + key_regedit12.GetValue("ngay") + "/" + key_regedit12.GetValue("thang") + "/" + key_regedit12.GetValue("nam");
                }
            }
        }

        private void btn_logout_Click(object sender, EventArgs e){
            RegistryKey keylogin = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
            keylogin.SetValue("keyUsername", "");
            keylogin.SetValue("keyPassword", "");
            Application.Exit();
        }
        private void main_Load(object sender, EventArgs e)
        {
            loadHtmlForm();
        }

        # region background service

        private void getListEmailReport()
        {
            getListEmailReportBackground.DoWork += new DoWorkEventHandler(getListEmailReportBackground_DoWork);
            getListEmailReportBackground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(getListEmailReportBackground_RunWorkerCompleted);
            getListEmailReportBackground.ProgressChanged += new ProgressChangedEventHandler(getListEmailReportBackground_ProgressChanged);
            getListEmailReportBackground.WorkerReportsProgress = true; getListEmailReportBackground.WorkerSupportsCancellation = true;
        }

        private void modifyRunScanCom()
        {
            runScanCOM.DoWork += new DoWorkEventHandler(runScanCOM_DoWork);
            runScanCOM.RunWorkerCompleted += new RunWorkerCompletedEventHandler(runScanCOM_RunWorkerCompleted);
            runScanCOM.ProgressChanged += new ProgressChangedEventHandler(runScanCOM_ProgressChanged);
            runScanCOM.WorkerReportsProgress = true; runScanCOM.WorkerSupportsCancellation = true;
        }

        private void modifySendSmsBakground()
        {
            runSendSmsBakground.DoWork += new DoWorkEventHandler(runSendSmsBakground_DoWork);
            runSendSmsBakground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(runSendSmsBakground_RunWorkerCompleted);
            runSendSmsBakground.ProgressChanged += new ProgressChangedEventHandler(runSendSmsBakground_ProgressChanged);
            runSendSmsBakground.WorkerReportsProgress = true; runSendSmsBakground.WorkerSupportsCancellation = true;
        }

        private void modifySendEmailBakground()
        {
            runSendEmailBakground.DoWork += new DoWorkEventHandler(runSendEmail_DoWork);
            runSendEmailBakground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(runSendEmail_RunWorkerCompleted);
            runSendEmailBakground.ProgressChanged += new ProgressChangedEventHandler(runSendEmail_ProgressChanged);
            runSendEmailBakground.WorkerReportsProgress = true; runSendEmailBakground.WorkerSupportsCancellation = true;
        }

        private void modifyEmailVerifyBakground()
        {
            runEmailVerifyBakground.DoWork += new DoWorkEventHandler(runEmailVerify_DoWork);
            runEmailVerifyBakground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(runEmailVerify_RunWorkerCompleted);
            runEmailVerifyBakground.ProgressChanged += new ProgressChangedEventHandler(runEmailVerify_ProgressChanged);
            runEmailVerifyBakground.WorkerReportsProgress = true; runEmailVerifyBakground.WorkerSupportsCancellation = true;
        }

        # endregion background service
        # region tab
        private void btn_sms_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabpage1;
            restartBackColor();

            btn_sms.BackColor = System.Drawing.Color.WhiteSmoke;
        }
        private void btn_email_check_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabpage3;
            restartBackColor();
        }


        private static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        private static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }

        private void lbl_sms_system_Click(object sender, EventArgs e)
        {
            
        }
        private void restartBackColor()
        {
            btn_sms.BackColor = System.Drawing.Color.Gainsboro;
            btn_contact.BackColor = System.Drawing.Color.Gainsboro;
            panel_sms.BackColor = System.Drawing.Color.Transparent;
        }

        # endregion tab
        # region Group sms
        private void btn_addNumber_Click(object sender, EventArgs e)
        {
            try
            {
                addNumberMobi fmOpenAddNumberMobile = new addNumberMobi(this);
               
                fmOpenAddNumberMobile.Show();

            }
            catch { }
        }
        private void btn_import_mobile_Click(object sender, EventArgs e)
        {
            openFileDialogMobile.ShowDialog();
        }
        private void openFileDialogMobile_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string filePath = openFileDialogMobile.FileName;
                string extension = Path.GetExtension(filePath);
                string header = "YES";
                string conStr, sheetName;

                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath, header);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath, header);
                        break;
                }

                //Get the name of the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                //Read Data from the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();
                            int countRows = dt.Rows.Count;
                            int fv = dgvMobile.Rows.Count;
                            dgvMobile.Rows.Add(countRows);
                            i_mobile = fv;
                            foreach (DataRow drow in dt.Rows)
                            {
                                string mobile = drow["MOBILE"].ToString();
                                string fullname = drow["HOTEN"].ToString();
                                if (!string.IsNullOrEmpty(mobile))
                                {
                                    dgvMobile.Rows[i_mobile].Cells[0].Value = true;
                                    dgvMobile.Rows[i_mobile].Cells[1].Value = mobile;
                                    dgvMobile.Rows[i_mobile].Cells[2].Value = fullname;
                                    dgvMobile.Rows[i_mobile].Cells[3].Value = "Chưa gửi";
                                    i_mobile++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string _errors = ex.Message;
                if (_errors == "The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine.")
                {
                    MessageBox.Show("Bạn phải cài đặt office 2007 trở lên trên máy của bạn, mới có thể sử dụng file xlsx.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định. \nVui lòng kiểm tra lại file của bạn.", "Thông báo");
                }
            }
        }
        private void btn_del_mobile_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = (from row in dgvMobile.Rows.Cast<DataGridViewRow>()
                                                  where Convert.ToBoolean(row.Cells["select"].Value) == true
                                                  select row).ToList();
            int countListSMSDelete = selectedRows.Count;
            
                if (countListSMSDelete == 0)
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa những dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow rs in selectedRows)
                        {
                            dgvMobile.Rows.RemoveAt(rs.Index);
                        }
                        i_mobile = dgvMobile.RowCount;
                    }
                }
        }
        private void cbsort_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng này bị khóa ở phiên bản miễn phí.", "Thông báo");
        }
        private void listCOM_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void txt_box_content_sms_TextChanged(object sender, EventArgs e)
        {
            int count_txt_content_sms = txt_box_content_sms.TextLength;
            lbl_char_sms.Text = "Ký tự: " + count_txt_content_sms + " / " + ((count_txt_content_sms / 161) + 1);
        }
        private void btn_pause_sms_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn dừng gửi sms ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btn_pause_sms.Enabled = false;
                btn_start_sms.Enabled = true;
                btn_start_sms.Text = "Bắt đầu gửi";
                senddingsms = false;
                pgbsendsms.Value = 100;
                runSendSmsBakground.CancelAsync();
                MessageBox.Show("Đã dừng gửi tin nhắn.");
            }
        }
        private void clickReloadDevice_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_status_check_device.Visible = false;lbl_status_search_com.Visible = true;lbl_status_search_com.Text = "Đang tìm kiếm...";
                clickReloadDevice.Visible = false; lbl_search_device_sms.Visible = false;
                if (!runScanCOM.IsBusy)
                {
                    runScanCOM.RunWorkerAsync();
                }
            }catch{}
        }
        private void sumNetwork(string namenk)
        { 
            switch(namenk)
            {
                case "Viettel":
                    viettel = viettel + 1;
                    break;
                case "Mobifone":
                    mobifone = mobifone + 1;
                    break;
                case "Vinaphone":
                    vinaphone = vinaphone + 1;
                    break;
                case "Beeline":
                    beeline = beeline + 1;
                    break;
                case "Vietnamobile":
                    vietnamobile = vietnamobile + 1;
                    break;
            }
        }
        protected void runScanCOM_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
                bool statusCOM = false;
                /**
                * @void tìm các thiết bị cắm
                */
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                int CountDevice = allDrives.Length; // đếm tổng các thiết bị cắm hiện thời
                if (CountDevice < NumberDevice)
                {
                    listCOM.Rows.Clear(); listCOM.Refresh(); NumberDevice = CountDevice; lbl_status_check_device.Text = "Đang chờ thiết bị...";
                }
                /**
                * @void tìm các cổng COM
                */
                string[] Ports = SerialPort.GetPortNames(); int LenghtPorts = Ports.Length; int numberCOM = 0; selectCOM = false; NumberDevice = CountDevice; int countListCom = listCOM.Rows.Count;
                int _LenghtPorts = LenghtPorts - 1;
                for (int j = 0; j < LenghtPorts; j++)
                {
                    //Application.DoEvents();
                    int notAvai = 1; string nameCom = Ports[j];

                    if (Library.IsModem(nameCom))
                    {
                        for (int bep = 0; bep < countListCom; bep++)
                        {
                            if (@listCOM.Rows[bep].Cells[1].Value.ToString() == @nameCom)
                            { notAvai = 0; break; }
                        }

                        if (notAvai == 1 && countListCom > 0)
                        {
                            string nameNetwork = Library.getNameISP(nameCom);
                            // cộng tổng số nhà mạng
                            sumNetwork(nameNetwork);
                            string _delaymax = Library.getNumberDelay(nameCom);
                            string[] delaymax = _delaymax.Split(new string[] { "|" }, StringSplitOptions.None);// _delaymax.Split("|");
                            string _delay = delaymax[0];
                            string _max = delaymax[1];
                            if (nameCom != null && nameCom != "" && nameNetwork != "Không xác định")
                            {
                                if (listCOM.InvokeRequired)
                                {
                                    listCOM.Invoke(new MethodInvoker(delegate
                                    {
                                        listCOM.Rows.Add();
                                        listCOM.Rows[numberCOM].Cells[0].Value = true;
                                        listCOM.Rows[numberCOM].Cells[1].Value = nameCom;
                                        listCOM.Rows[numberCOM].Cells[2].Value = nameNetwork;
                                        listCOM.Rows[numberCOM].Cells[3].Value = _delay;
                                        listCOM.Rows[numberCOM].Cells[4].Value = _max;
                                        listCOM.Rows[numberCOM].Cells[6].Value = 0;
                                        listCOM.Rows[numberCOM].Cells[7].Value = 0;
                                    }));
                                }

                            }
                        }
                        else if (countListCom == 0)
                        {
                            string nameNetwork = Library.getNameISP(nameCom);
                            sumNetwork(nameNetwork);
                            string _delaymax = Library.getNumberDelay(nameNetwork);
                            string[] delaymax = _delaymax.Split(new string[] { "|" }, StringSplitOptions.None);// _delaymax.Split("|");
                            string _delay = delaymax[0];
                            string _max = delaymax[1];
                            if (nameCom != null && nameCom != "" && nameNetwork != "Không xác định")
                            {
                                if (listCOM.InvokeRequired)
                                {
                                    listCOM.Invoke(new MethodInvoker(delegate
                                    {
                                        listCOM.Rows.Add();
                                        listCOM.Rows[numberCOM].Cells[0].Value = true;
                                        listCOM.Rows[numberCOM].Cells[1].Value = nameCom;
                                        listCOM.Rows[numberCOM].Cells[2].Value = nameNetwork;
                                        listCOM.Rows[numberCOM].Cells[3].Value = _delay;
                                        listCOM.Rows[numberCOM].Cells[4].Value = _max;
                                        listCOM.Rows[numberCOM].Cells[6].Value = 0;
                                        listCOM.Rows[numberCOM].Cells[7].Value = 0;
                                    }));
                                }

                            }
                        }
                        senddingsms = true; selectCOM = true; numberCOM++;
                        statusCOM = true;
                    }
                    
                }
                lbl_status_search_com.Invoke(new MethodInvoker(delegate { lbl_status_search_com.Visible = false; }));
                clickReloadDevice.Invoke(new MethodInvoker(delegate { clickReloadDevice.Visible = true; }));
                lbl_search_device_sms.Invoke(new MethodInvoker(delegate { lbl_search_device_sms.Visible = true; }));
                selectCOM = true;
            }
            catch { }
        }
        protected void runScanCOM_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e){}
        protected void runScanCOM_ProgressChanged(object sender, ProgressChangedEventArgs e){}

        private void btn_start_sms_Click(object sender, EventArgs e)
        {
           try
           {
                // set câu thông báo nếu mà ku cho nó chưa được chọn
                isShowDeviceNotSelected = true;
                senddingsms = true;
                if (selectCOM)
                {
                    lbl_send_sms.Text = "Số tin nhắn đã gửi đến khách hàng: 0";
                    List<DataGridViewRow> checklistMobileSendSelect = (from rowssmschecklist in dgvMobile.Rows.Cast<DataGridViewRow>()
                                                                       where Convert.ToBoolean(rowssmschecklist.Cells["select"].Value) == true
                                                                       select rowssmschecklist).ToList();
                    int mobile = checklistMobileSendSelect.Count;
                    sum_sms_send_curent = 100 / mobile;
                    pgbsendsms.Value = 1;
                    startsmssend = false;
                    if (mobile == 0)
                    {
                        MessageBox.Show("Vui lòng thêm vào danh sách số điện thoại");
                    }
                    else {

                        btn_start_sms.Text = "Đang check...";
                        btn_start_sms.Enabled = false;
                        btn_pause_sms.Enabled = true;

                        rowSMSList = 0;
                        sendSMSSuccess = 0;
                        int countDgvMobile = dgvMobile.Rows.Count;

                        foreach (DataGridViewRow rowlistcom in listCOM.Rows)
                        {
                            rowlistcom.Cells["status"].Value = 0;
                            rowlistcom.Cells["numbersends"].Value = 0;
                        }

                        foreach (DataGridViewRow rowlistSms in checklistMobileSendSelect)
                        {
                            rowlistSms.Cells["status_sms"].Value = "Chưa gửi...";
                            rowlistSms.Cells["status_sms"].Style.BackColor = System.Drawing.Color.White;
                            rowlistSms.Cells["status_sms"].Style.ForeColor = System.Drawing.Color.Black;
                        }
                        if (!runSendSmsBakground.IsBusy)
                        {
                            runSendSmsBakground.RunWorkerAsync();
                        }
                    }
                    
                }
                else
                {
                    MessageBox.Show("Vui lòng bấm nút tìm kiếm thiết bị.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                string _error_sms = ex.Message;
                if (_error_sms == "The requested resource is in use.")
                {
                    MessageBox.Show("Vui lòng tắt phần mềm kết nối của nhà mạng.", "Thông báo");
                }
                else if (_error_sms == "Object reference not set to an instance of an object.")
                {
                    btn_start_sms.Enabled = true;
                    btn_start_sms.Text = "Bắt đầu gửi";
                    btn_pause_sms.Enabled = false;
                    MessageBox.Show("Vui lòng thêm vào danh sách gửi.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định.", "Thông báo");
                }
            }
             
        }
        private string getComNameDeviceSMS(string numberFone)
        {
            try
            {
                // chọn ra danh sách thiết bị được phép gửi sms
                List<DataGridViewRow> selectedlistCOM = (from rows_ in listCOM.Rows.Cast<DataGridViewRow>()
                                                         where Convert.ToBoolean(rows_.Cells["selected_sms"].Value) == true
                                                         select rows_).ToList();
                int countselecteddevice = selectedlistCOM.Count;
                if (countselecteddevice == 0)
                {
                    return "201";
                }
                #region foreach device selected
                foreach (DataGridViewRow row in selectedlistCOM)
                {
                    // lấy tên nhfa mạng trong dòng này
                    string _nameISP = row.Cells["network"].Value.ToString();
                    // nếu tên nhà mạng và nhà mangh của số điện thoại này giống nhau thì sẽ nhảy vào trong cho vui
                    if (toonetwork.Checked)
                    {
                        if (Library.selectNetworkNumber(numberFone) == _nameISP)
                        {
                            // convert lại int
                            int sendted = Convert.ToInt32(row.Cells["status"].Value.ToString());
                            if (sendted != 1)
                            {
                                // check xem có bao nhiêu thiết bị của nhà mạng này, nếu lớn hơn 1 thì mới đánh dấu là đã gửi để ko chọn vào ku này cho lần gửi sau
                                if (_nameISP == "Viettel")
                                {
                                    row.Cells["status"].Value = 0;
                                    if (viettel > 1)
                                    {
                                        row.Cells["status"].Value = 1;
                                    }
                                }
                                else if (_nameISP == "Mobifone")
                                {
                                    row.Cells["status"].Value = 0;
                                    if (mobifone > 1)
                                    {
                                        row.Cells["status"].Value = 1;
                                    }
                                }
                                else if (_nameISP == "Vinaphone")
                                {
                                    row.Cells["status"].Value = 0;
                                    if (vinaphone > 1)
                                    {
                                        row.Cells["status"].Value = 1;
                                    }
                                }
                                else if (_nameISP == "Beeline")
                                {
                                    row.Cells["status"].Value = 0;
                                    if (beeline > 1)
                                    {
                                        row.Cells["status"].Value = 1;
                                    }
                                }
                                else if (_nameISP == "Vietnamobile")
                                {
                                    row.Cells["status"].Value = 0;
                                    if (vietnamobile > 1)
                                    {
                                        row.Cells["status"].Value = 1;
                                    }
                                }

                                // check xem thiết bị này còn thừa số sms được phép gửi đi hay ko ?
                                int current_sms = Convert.ToInt32(row.Cells["numbersends"].Value.ToString());
                                int numbersend = current_sms + 1;
                                if (numbersend <= Convert.ToInt32(row.Cells["line"].Value.ToString()))
                                {
                                    row.Cells["numbersends"].Value = numbersend;
                                    if (startsmssend)
                                    {
                                        System.Threading.Thread.Sleep((Convert.ToInt32(row.Cells["delay"].Value.ToString())) * 1000);
                                    }
                                    startsmssend = true;
                                    // trả lại cổng com được phép gửi
                                    return row.Cells["device_sms"].Value.ToString();
                                }

                            }
                            else
                            {
                                row.Cells["status"].Value = 0;
                            }
                        }
                    }
                    else {
                        // convert lại int
                        if(countselecteddevice == 1)
                        {
                            row.Cells["status"].Value = 0;
                            int current_sms = Convert.ToInt32(row.Cells["numbersends"].Value.ToString());
                            int numbersend = current_sms + 1;
                            if (numbersend <= Convert.ToInt32(row.Cells["line"].Value.ToString()))
                            {
                                row.Cells["numbersends"].Value = numbersend;
                                if (startsmssend)
                                {
                                    System.Threading.Thread.Sleep((Convert.ToInt32(row.Cells["delay"].Value.ToString())) * 1000);
                                }
                                startsmssend = true;
                                // trả lại cổng com được phép gửi
                                return row.Cells["device_sms"].Value.ToString();
                            }
                        }
                        int sendted = Convert.ToInt32(row.Cells["status"].Value.ToString());
                        if (sendted != 1)
                        {
                            row.Cells["status"].Value = 1;

                            // check xem thiết bị này còn thừa số sms được phép gửi đi hay ko ?
                            int current_sms = Convert.ToInt32(row.Cells["numbersends"].Value.ToString());
                            int numbersend = current_sms + 1;
                            if (numbersend <= Convert.ToInt32(row.Cells["line"].Value.ToString()))
                            {
                                row.Cells["numbersends"].Value = numbersend;
                                if (startsmssend)
                                {
                                    System.Threading.Thread.Sleep((Convert.ToInt32(row.Cells["delay"].Value.ToString())) * 1000);
                                }
                                startsmssend = true;
                                // trả lại cổng com được phép gửi
                                return row.Cells["device_sms"].Value.ToString();
                            }

                        }
                        else
                        {
                            row.Cells["status"].Value = 0;
                        }
                    }
                }
                #endregion
            }
            catch { }
            return "2012";
        }

        private int random(int str)
        {
            Random rd = new Random();
            return rd.Next(0, (str - 1));
        }

        protected void runSendSmsBakground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
                bool notselectedCom = false;
                    //Random random = new Random();
                    //int randomNumber = random.Next(10, 500);

                    string MsgGET = txt_box_content_sms.Text;

                    int randomContent = 0;
                    int mobile = dgvMobile.Rows.Count;

                    // chọn ra danh sách thiết bị được phép gửi sms
                    List<DataGridViewRow> listMobileSendSelect = (from rows_ in dgvMobile.Rows.Cast<DataGridViewRow>()
                                                                    where Convert.ToBoolean(rows_.Cells["select"].Value) == true
                                                                    select rows_).ToList();
                    int countListMobileSendSelect = listMobileSendSelect.Count;
                    if (countListMobileSendSelect == 0)
                    {
                        MessageBox.Show("Vui lòng thêm hoặc chọn vào danh sách số điện thoại");
                    }
                    else
                    {
                        foreach (DataGridViewRow rowmobile in listMobileSendSelect)
                        {
                            try {
                                if (senddingsms)
                                {
                                    if (rowSMSList < countListMobileSendSelect)
                                    {
                                        // Lấy ra được số điện thoại cần phang
                                        string numberMobile = rowmobile.Cells["numberPhone"].Value.ToString();

                                        // chọn thiết bị để gửi sms
                                        string comPort = getComNameDeviceSMS(numberMobile);
                                        //System.Threading.Thread.Sleep(10000);
                                        // nếu không chọn được nhà mạng nào thì như thế này đây

                                        if (comPort == "201")
                                        {
                                            // Voãi beep gửi thất bại con mụa nó rồi
                                            /*notselectedCom = true;
                                            rowmobile.Cells["status_sms"].Value = "Không gửi được";
                                            rowmobile.Cells["status_sms"].Style.BackColor = System.Drawing.Color.Red;
                                            rowmobile.Cells["status_sms"].Style.ForeColor = System.Drawing.Color.White;*/
                                            MessageBox.Show("Vui lòng kiểm tra lại thiết bị gửi sms", "Thông báo");
                                        }
                                        else
                                        {
                                            // Nếu chọn được nhà mạng thì ngon ngay
                                            // Check xem fullname có bị rỗng không? nếu rỗng thì chết bà nó ở đây, cần phải fix
                                            string namefullName = "";
                                            if (rowmobile.Cells["fullname"].Value != null && rowmobile.Cells["fullname"].Value.ToString() != String.Empty)
                                            {
                                                namefullName = rowmobile.Cells["fullname"].Value.ToString();
                                            }
                                            // đã fix xong

                                            string ContentSMS = "";
                                            if (random_content_sms.Checked)
                                            {
                                                string[] split = MsgGET.Split(new Char[] { '{' });

                                                string _string1 = split[0];
                                                string _strings = "";
                                                string bee = "";
                                                int count = split.Count();

                                                for (int i = 1; i <= (count - 1); i++)
                                                {
                                                    string[] _split = split[i].Split(new Char[] { '}' });
                                                    string _content = _split[0].ToString();
                                                    string[] content = _content.Split(new Char[] { '|' });
                                                    int _scount = content.Count();
                                                    int max = random(_scount);
                                                    for (int z = max; z < (max + 1); z++)
                                                    {
                                                        bee = content[z].ToString();
                                                    }

                                                    if (i == 1)
                                                    {
                                                        _strings = _string1 + " " + bee + " " + _split[1];
                                                    }
                                                    else
                                                    {
                                                        _strings = _strings + " " + bee + " " + _split[1];
                                                    }
                                                }
                                                ContentSMS = _strings;
                                            }
                                            else
                                            {
                                                ContentSMS = MsgGET;
                                            }


                                            // Replace cái chữ name kia thành tên của con nhà người ta
                                            string messages = ContentSMS.Replace("</NAME/>!", namefullName);
                                            txtshow_sms.Invoke(new MethodInvoker(delegate { txtshow_sms.Text = messages; }));

                                            string cellNo = "+84" + numberMobile;
                                            // Bắt đầu gửi sms cho bọn nó.
                                            // Nếu là gửi thành công thì nhảy vào trong này
                                            btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Text = "Đang gửi..."; }));
                                            rowmobile.Cells["status_sms"].Value = "Đang gửi";
                                            rowmobile.Cells["status_sms"].Style.BackColor = System.Drawing.Color.YellowGreen;
                                            rowmobile.Cells["status_sms"].Style.ForeColor = System.Drawing.Color.White;
                                            MessageBox.Show(Library.sendATSMSCommand(comPort, cellNo, messages, _sendFailer));
                                        }
                                    }
                                    else
                                    {
                                        // Thông báo cho bọn nó là vừa gửi xong được bao nhiêu 
                                        lbl_send_sms.Invoke(new MethodInvoker(delegate { lbl_send_sms.Text = "Số tin nhắn đã gửi đến khách hàng: " + sendSMSSuccess; }));
                                        // Chuyển trạng thái nút bắt đầu gửi tin nhắn.
                                        btn_export_send_sms.Invoke(new MethodInvoker(delegate { btn_export_send_sms.Enabled = true; }));
                                        btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Enabled = true; }));
                                        btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Text = "Bắt đầu gửi"; }));

                                        btn_pause_sms.Invoke(new MethodInvoker(delegate { btn_pause_sms.Enabled = false; }));
                                        senddingsms = false;
                                    }
                                }
                                else
                                {
                                    btn_export_send_sms.Invoke(new MethodInvoker(delegate { btn_export_send_sms.Enabled = true; }));
                                    btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Enabled = true; }));
                                    btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Text = "Bắt đầu gửi"; }));

                                    btn_pause_sms.Invoke(new MethodInvoker(delegate { btn_pause_sms.Enabled = false; }));
                                }
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                            if (rowSMSList == (mobile - 1))
                            {
                                btn_export_send_sms.Invoke(new MethodInvoker(delegate { btn_export_send_sms.Enabled = true; }));
                                btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Enabled = true; }));
                                btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Text = "Bắt đầu gửi"; }));

                                btn_pause_sms.Invoke(new MethodInvoker(delegate { btn_pause_sms.Enabled = false; }));
                                if (_sendFailer)
                                {
                                    _sendFailer = false;
                                    MessageBox.Show("Vui lòng kiểm tra lại thẻ sim hoặc tài khoản trong sim.", "Thông báo");
                                }
                            }
                            if (notselectedCom)
                            {
                                btn_export_send_sms.Invoke(new MethodInvoker(delegate { btn_export_send_sms.Enabled = true; }));
                                btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Enabled = true; }));
                                btn_start_sms.Invoke(new MethodInvoker(delegate { btn_start_sms.Text = "Bắt đầu gửi"; }));

                                btn_pause_sms.Invoke(new MethodInvoker(delegate { btn_pause_sms.Enabled = false; }));
                                if (isShowDeviceNotSelected)
                                {
                                    isShowDeviceNotSelected = false;
                                    senddingsms = true;
                                    MessageBox.Show("Không chọn được thiết bị, vui lòng kiểm tra lại thiết bị hoặc đã đạt giới hạn gửi của các thiết bị.", "Thông báo");
                                }
                            }
                            rowSMSList++;
                        }
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void runSendSmsBakground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void runSendSmsBakground_ProgressChanged(object sender, ProgressChangedEventArgs e) { }
        private void listCOM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    try
                    {

                        Comname = listCOM.Rows[e.RowIndex].Cells[1].Value.ToString();
                        networkName = listCOM.Rows[e.RowIndex].Cells[2].Value.ToString();
                        delaydevice = Convert.ToInt32(listCOM.Rows[e.RowIndex].Cells[3].Value);
                        maxdevice = Convert.ToInt32(listCOM.Rows[e.RowIndex].Cells[4].Value);
                        deviceRowCurrent = e.RowIndex.ToString();

                        setupdevice fmOpensetupdevice = new setupdevice(this);
                        fmOpensetupdevice.Show();

                    }
                    catch { }
                }
                /*if (e.ColumnIndex == 0)
                {}*/
            }
            catch { }
        }
        private void btn_booking_Click(object sender, EventArgs e)
        {
            try
            {
                setupSendSms fmsetupSendSms = new setupSendSms(this);
                fmsetupSendSms.Show();

            }
            catch { }
        }
        private void canhan_CheckedChanged(object sender, EventArgs e)
        {
            if (canhan.Checked)
            {
                try
                {
                    string content = txt_box_content_sms.Text + " </NAME/>!";
                    txt_box_content_sms.Text = content;
                }
                catch { }
            }
            else
            {
                string content = txt_box_content_sms.Text;
                txt_box_content_sms.Text = content.Replace("</NAME/>!", "");
            }
        }
        private void selectedsms_CheckedChanged(object sender, EventArgs e)
        {

            if (selectedsms.Checked)
            {
                foreach (DataGridViewRow row in dgvMobile.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvMobile.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
            }
        }
        private void random_content_sms_CheckedChanged(object sender, EventArgs e)
        {
            string content = txt_box_content_sms.Text;
            if (random_content_sms.Checked)
            {
                if (!radndomsms)
                {
                    txt_box_content_sms.Text = content + " {noi_dung_1|noi_dung_2}";
                    radndomsms = true;
                }
            }
        }
        private void btn_down_file_sms_Click(object sender, EventArgs e)
        {
            try {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "xlsx";

                saveFileDialog.InitialDirectory = @"Desktop";
                saveFileDialog.FileName = "sample" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";

                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx |All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    const string MyFileName = "sample.xlsx";
                    string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                    var filePath = Path.Combine(execPath, MyFileName);
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook book = app.Workbooks.Open(filePath);

                    book.SaveAs(saveFileDialog.FileName); //Save
                    book.Close();
                    MessageBox.Show("Tải file thành công");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Để tải được file mẫu, bạn vui lòng cài đặt Office");
            }
        }
        private void lbl_search_device_sms_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_status_check_device.Visible = false; lbl_status_search_com.Visible = true; lbl_status_search_com.Text = "Đang tìm kiếm...";
                clickReloadDevice.Visible = false; lbl_search_device_sms.Visible = false;
                if (!runScanCOM.IsBusy)
                {
                    runScanCOM.RunWorkerAsync();
                }
            }
            catch { }
        }
        private void btn_export_send_sms_Click(object sender, EventArgs e)
        {
            exportReportSendSms();
        }
        private void exportReportSendSms()
        {
            try
            {
                if (MessageBox.Show("Bạn có muốn xuất báo cáo ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sfd_report_send_sms.InitialDirectory = "C:";
                    sfd_report_send_sms.Title = "Savefile";
                    sfd_report_send_sms.FileName = "report-send-sms-with-beecom" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";
                    //Excel Files(2003)|*.xls|
                    sfd_report_send_sms.Filter = "Excel Files|*.xlsx";
                    if (sfd_report_send_sms.ShowDialog() != DialogResult.Cancel)
                    {
                        Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                        Excel.Application.Workbooks.Add(Type.Missing);
                        Excel.Columns.ColumnWidth = 20;

                        int countColumn = dgvMobile.Columns.Count - 1;
                        int countRows = dgvMobile.Rows.Count;

                        for (int i = 2; i <= dgvMobile.Columns.Count; i++)
                        {
                            Excel.Cells[1, i - 1] = dgvMobile.Columns[i - 1].HeaderText;
                        }
                        for (int ix = 0; ix < countRows; ix++)
                        {
                            for (int j = 1; j <= countColumn; j++)
                            {
                                if (dgvMobile.Rows[ix].Cells[j].Value != null)
                                {
                                    Excel.Cells[ix + 2, j] = dgvMobile.Rows[ix].Cells[j].Value.ToString();
                                }
                                else
                                {
                                    Excel.Cells[ix + 2, j] = " ";
                                }

                            }
                        }
                        Excel.ActiveWorkbook.SaveCopyAs(sfd_report_send_sms.FileName.ToString());
                        Excel.ActiveWorkbook.Saved = true;
                        MessageBox.Show("Xuất báo cáo thành công");
                        Excel.Quit();
                    }

                }
            }
            catch {
                MessageBox.Show("Vui lòng cài đặt office trước khi xuất báo cáo.", "Thông báo");
            }
        }
        # endregion
        # region Email verify
        private void openFileDialogFileEmailCheck_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string filePath = openFileDialogFileEmailCheck.FileName;
                //txt_file_email_check.Text = filePath;

                string extension = Path.GetExtension(filePath);
                string header = "YES";
                string conStr, sheetName;

                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath, header);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath, header);
                        break;
                }

                //Get the name of the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }

                //Read Data from the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();
                            int countRows = dt.Rows.Count;
                            int fv = dataGridListMailCheck.Rows.Count;
                            i_checkmail = fv;
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!string.IsNullOrEmpty(drow["EMAIL"].ToString()))
                                {
                                    dataGridListMailCheck.Rows.Add();
                                    dataGridListMailCheck.Rows[i_checkmail].Cells[0].Value = true;
                                    dataGridListMailCheck.Rows[i_checkmail].Cells[1].Value = drow["EMAIL"];
                                    dataGridListMailCheck.Rows[i_checkmail].Cells[2].Value = drow["HOTEN"];
                                    dataGridListMailCheck.Rows[i_checkmail].Cells[3].Value = "Chưa verify";
                                    i_checkmail++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string _errors = ex.Message;
                if (_errors == "The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine.")
                {
                    MessageBox.Show("Bạn phải cài đặt office 2007 trở lên trên máy của bạn, mới có thể sử dụng file xlsx.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định. \nVui lòng kiểm tra lại file của bạn.", "Thông báo");
                }
            }
        }
        private void btn_start_check_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_account_smtp_verify.Text == "Nhập địa chỉ email của bạn")
                {
                    MessageBox.Show("Vui lòng thiết lập địa chỉ email check.", "Thông báo");
                }
                else { 
                    List<DataGridViewRow> checkListEmailVerify = (from rowEmailVerify in dataGridListMailCheck.Rows.Cast<DataGridViewRow>()
                                                              where Convert.ToBoolean(rowEmailVerify.Cells["selected_email_verify"].Value) == true
                                                              select rowEmailVerify).ToList();
                    int emailverifys = checkListEmailVerify.Count;
                    sum_emailverifys_send_curent = 100 / emailverifys;
                    pgbsendsms.Value = 1;

                    //MessageBox.Show("" + lISTEmailVerify + "");
                    senddingEmailVerify = true;
                    btn_start_check.Text = "Đang check...";
                    btn_start_check.Enabled = false;
                    btn_stop_check.Enabled = true;
                    btn_stop_check.Enabled = true;
                    rowEmailListVerify = 0;
                    btn_export_file.Enabled = false;

                    foreach (DataGridViewRow _rowemailverify in checkListEmailVerify)
                    {
                        _rowemailverify.Cells["status_check"].Value = "Chưa verify...";
                        _rowemailverify.Cells["status_check"].Style.BackColor = System.Drawing.Color.White;
                        _rowemailverify.Cells["status_check"].Style.ForeColor = System.Drawing.Color.Black;
                    }
                    if (!runEmailVerifyBakground.IsBusy)
                    {
                        runEmailVerifyBakground.RunWorkerAsync();
                    }
                }
            }
            catch /*(Exception ex)*/
            {
                /*MessageBox.Show(ex.Message);*/
            }
        }
        protected void runEmailVerify_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
                List<DataGridViewRow> listEmailVerify = (from rows_ in dataGridListMailCheck.Rows.Cast<DataGridViewRow>()
                                                         where Convert.ToBoolean(rows_.Cells["selected_email_verify"].Value) == true
                                                         select rows_).ToList();
                int countListEmailVerify = listEmailVerify.Count;

                if (countListEmailVerify == 0)
                {
                    MessageBox.Show("Vui lòng thêm hoặc chọn vào danh sách email");
                }
                else
                {
                    btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Text = "Đang verify..."; }));
                    foreach (DataGridViewRow rowemailverify in listEmailVerify)
                    {
                        # region process email verify
                        if (senddingEmailVerify)
                        {
                            
                            if (rowEmailListVerify < countListEmailVerify)
                            {
                                
                                string comPort = namePorts;
                                string emailaddress = rowemailverify.Cells["email_check"].Value.ToString();

                                //
                                if (emailaddress != null)
                                {
                                    rowemailverify.Cells["status_check"].Value = "Đang verify";
                                    rowemailverify.Cells["status_check"].Style.BackColor = System.Drawing.Color.YellowGreen;
                                    rowemailverify.Cells["status_check"].Style.ForeColor = System.Drawing.Color.White;

                                    string __data = Library.verifyEmail(emailaddress, txt_account_smtp_verify.Text);
                                   
                                    string[] split = __data.Split(new Char[] { '=' });
                                    string contentdebug = split[0];
                                    string rsdebug = split[1];
                                    //

                                    txtbox_debug_check.Invoke(new MethodInvoker(delegate { txtbox_debug_check.Text = contentdebug + "\r" + txtbox_debug_check.Text; }));

                                    rowemailverify.Cells["status_check"].Value = "Đã verify";
                                    rowemailverify.Cells["status_check"].Style.BackColor = System.Drawing.Color.Green;
                                    rowemailverify.Cells["status_check"].Style.ForeColor = System.Drawing.Color.White;

                                    if (rsdebug == "200")
                                    {
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows.Add(); }));
                                        int _rowEmailListVerify = numberVerify + 1;
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows[numberVerify].Cells[0].Value = _rowEmailListVerify; }));
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows[numberVerify].Cells[1].Value = emailaddress; }));
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows[numberVerify].Cells[2].Value = dataGridListMailCheck.Rows[rowEmailListVerify].Cells[2].Value.ToString(); }));
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows[numberVerify].Cells[3].Value = "Tồn tại"; }));
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows[numberVerify].Cells[3].Style.BackColor = System.Drawing.Color.Green; }));
                                        dgvverifyok.Invoke(new MethodInvoker(delegate { dgvverifyok.Rows[numberVerify].Cells[3].Style.ForeColor = System.Drawing.Color.White; }));
                                        numberVerify++;
                                    }
                                    else
                                    {
                                        Console.Beep();
                                        rowemailverify.Cells["status_check"].Value = "Không tồn tại";
                                        rowemailverify.Cells["status_check"].Style.BackColor = System.Drawing.Color.Red;
                                        rowemailverify.Cells["status_check"].Style.ForeColor = System.Drawing.Color.White;
                                    }

                                }
                                else
                                {
                                    senddingEmailVerify = false;
                                    btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Text = "Bắt đầu verify"; }));
                                    btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Enabled = true; }));
                                    btn_stop_check.Invoke(new MethodInvoker(delegate { btn_stop_check.Enabled = false; }));
                                }
                            }
                            else
                            {
                                //lbl_send_sms.Text = "Số mail đã gửi đến khách hàng: " + sendEMAILSuccess;
                                senddingEmailVerify = false;
                                btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Text = "Bắt đầu verify"; }));
                                btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Enabled = true; }));
                                btn_stop_check.Invoke(new MethodInvoker(delegate { btn_stop_check.Enabled = false; }));
                            }

                            // hehe, ngồi kéo cho ns tăng cái progressbar lên thoi ma
                            // nhân với tổng số hay cái gi ấy
                            int intsc = (rowEmailListVerify == 0 ? 1 : (rowEmailListVerify + 1)) * sum_emailverifys_send_curent;
                            // check neu ma ku lon hon 100 thi phai xu ly ngay
                            int pgbvalueemailverify = intsc > 100 ? 100 : intsc;
                            // gán lại vào cái p nào, nhưng mà phải chơi cái invoke cho nó máu.
                            pgbsendsms.Invoke(new MethodInvoker(delegate { pgb_check_email.Value = pgbvalueemailverify; }));
                            rowEmailListVerify++;

                            if (rowEmailListVerify == countListEmailVerify)
                            {
                                senddingEmailVerify = false;
                                btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Text = "Bắt đầu verify"; }));
                                btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Enabled = true; }));
                                btn_stop_check.Invoke(new MethodInvoker(delegate { btn_stop_check.Enabled = false; }));
                                pgb_check_email.Invoke(new MethodInvoker(delegate { pgb_check_email.Value = 100; }));
                                btn_export_file.Invoke(new MethodInvoker(delegate { btn_export_file.Enabled = true; }));
                            }

                        }
                        else
                        {
                            btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Enabled = true; }));
                            btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Text = "Bắt đầu verify"; }));
                        }

                        #endregion
                    }
                }


            }
            catch (Exception ex)
            {
                string VLError = ex.Message;
                if (VLError == "Failure sending mail." && !isStopVerifyMail)
                {
                    isStopVerifyMail = true;
                    senddingEmailVerify = false;
                    btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Text = "Bắt đầu verify"; }));
                    btn_start_check.Invoke(new MethodInvoker(delegate { btn_start_check.Enabled = true; }));
                    btn_stop_check.Invoke(new MethodInvoker(delegate { btn_stop_check.Enabled = false; }));
                    MessageBox.Show("Verify Email thất bại, Vui lòng thử lại.", "Thông báo");
                }

            }

        }
        protected void runEmailVerify_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void runEmailVerify_ProgressChanged(object sender, ProgressChangedEventArgs e) { }
        private void btn_delete_email_verify_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedrowDeleteEmailVerify = (from rowDeleteEmailVerify in dataGridListMailCheck.Rows.Cast<DataGridViewRow>()
                                                                  where Convert.ToBoolean(rowDeleteEmailVerify.Cells["selected_email_verify"].Value) == true
                                                                  select rowDeleteEmailVerify).ToList();
            int countselectedrowDeleteEmailVerify = selectedrowDeleteEmailVerify.Count;

            if (countselectedrowDeleteEmailVerify == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa những dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow rs in selectedrowDeleteEmailVerify)
                    {
                        dataGridListMailCheck.Rows.RemoveAt(rs.Index);
                    }
                    i_checkmail = dataGridListMailCheck.RowCount;
                }
            }
        }
        private void btn_stop_check_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn dừng Verify Email ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btn_stop_check.Enabled = false;
                btn_start_check.Enabled = true;
                btn_start_sms.Text = "Bắt đầu check";
                senddingEmailVerify = false;
                pgb_check_email.Value = 100;
                runEmailVerifyBakground.CancelAsync();
                MessageBox.Show("Đã dừng Verify Email.");
            }
        }
        # endregion verify Email
        # region sendmail

        private void btn_file_upload_Click(object sender, EventArgs e)
        {
            openFileDialogFileEmailCheck.ShowDialog();
        }

        private int _getNextIDMailAdd()
        {
            SQLiteConnection __sqlite_conn;
            SQLiteCommand __sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            __sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");
            __sqlite_conn.Open();
            __sqlite_cmd = __sqlite_conn.CreateCommand();
            __sqlite_cmd.CommandText = "SELECT max(id) AS idss FROM mail_add";
            sqlite_datareader = __sqlite_cmd.ExecuteReader();

            int _id_next = 0;
            while (sqlite_datareader.Read())
            {
                _id_next = Convert.ToInt32(sqlite_datareader["idss"]) + 1;
            }
            __sqlite_conn.Close();
            return _id_next;

        }

        bool click_email = false;
        private void btn_email_Click(object sender, EventArgs e)
        {
            if (!click_email)
            {
                Library._mail_add = _getNextIDMailAdd();
                string txtSQLQuery = "INSERT INTO mail_add (id) VALUES (" + Library._mail_add + ");";
                ExecuteQuery(txtSQLQuery);
                click_email = true;
            }
            

            loadToolboxEditor();
            loadFontList();
            tabControl1.SelectedTab = tabpage2;
            restartBackColor();
        }
        private void loadToolboxEditor()
        {
            cbFontSize.Items.Clear();
            cbFontSize.Items.Add(8);
            cbFontSize.Items.Add(10);
            cbFontSize.Items.Add(12);
            cbFontSize.Items.Add(14);
            cbFontSize.Items.Add(18);
            cbFontSize.Items.Add(24);
            cbFontSize.Items.Add(36);
            cbFontSize.Items.Add(72);
        }

        private void loadFontList()
        {
            InstalledFontCollection fonts = new InstalledFontCollection();
            for (int i = 0; i < fonts.Families.Length; i++)
            {
                cbFont.Items.Add(fonts.Families[i].Name);
            }
        }

        private void loadHtmlForm()
        {
            HTMLEditors.DocumentText = "<html><body></body></html>";
            doc = HTMLEditors.Document.DomDocument as IHTMLDocument2;
            doc.designMode = "On";
        }       
        private void openFileDialogEmailList_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                string filePath = openFileDialogEmailList.FileName;
                string extension = Path.GetExtension(filePath);
                string header = "YES";
                string conStr, sheetName;

                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath, header);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath, header);
                        break;
                }

                //Get the name of the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }
                int ok_import = 1;
                //Read Data from the First Sheet.
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();
                            int countRows = dt.Rows.Count;
                            i_mail = dgvEmailListSend.RowCount;
                            foreach (DataRow drow in dt.Rows)
                            {
                                if (!string.IsNullOrEmpty(drow["EMAIL"].ToString()))
                                {
                                    if (IsValidEmailImport(drow["EMAIL"].ToString()))
                                    {
                                        dgvEmailListSend.Rows.Add();
                                        dgvEmailListSend.Rows[i_mail].Cells[1].Value = i_mail;
                                        dgvEmailListSend.Rows[i_mail].Cells[0].Value = true;
                                        dgvEmailListSend.Rows[i_mail].Cells[2].Value = drow["EMAIL"];
                                        dgvEmailListSend.Rows[i_mail].Cells[3].Value = drow["HOTEN"];
                                        dgvEmailListSend.Rows[i_mail].Cells[4].Value = "Chưa gửi";
                                        i_mail++;
                                        string txtSQLQuery = "INSERT INTO emails (email_name, type_list, fullname) VALUES ('" + drow["EMAIL"] + "', 1, '" + drow["HOTEN"] + "');";
                                        ExecuteQuery(txtSQLQuery);
                                    }
                                }
                            }
                            MessageBox.Show("Thêm thành công");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string _errors = ex.Message;
                if (_errors == "The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine.")
                {
                    MessageBox.Show("Bạn phải cài đặt office 2007 trở lên trên máy của bạn, mới có thể sử dụng file xlsx.", "Thông báo");
                }
                else
                {
                    MessageBox.Show(_errors, "Thông báo");
                   // MessageBox.Show("Lỗi không xác định. \nVui lòng kiểm tra lại file của bạn.", "Thông báo");
                }
            }
        }

        private bool IsValidEmailImport(string emailaddress)
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

        private void checkValidationSendMail()
        {
            int lengDocMail = HTMLEditors.DocumentText.Length;
            checkvaldationmail = false;

            RegistryKey key_regedit2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
            string addressemail = key_regedit2.GetValue("addressemail").ToString(); // "112.213.92.6";// 

            if (addressemail == "default@email.com")
            {
                checkvaldationmail = true;
                MessageBox.Show("Vui lòng cài đặt Address Email trong menu 'Cài đặt Mail server'.", "Thông báo");
            }
            else if (txt_subject_email.Text == "")
            {
                checkvaldationmail = true;
                MessageBox.Show("Vui lòng nhập vào tiêu đề của nội dung gửi mail.", "Thông báo");
            }
            else if (lbl_server_smtp_send.Text == "Email@smovietnam.com ( Không sẵn sàng )")
            {
                checkvaldationmail = true;
                MessageBox.Show("Vui lòng cài đặt Mail Server.", "Thông báo");
            }
            else if (lengDocMail == 233 || lengDocMail < 5)
            {
                checkvaldationmail = true;
                MessageBox.Show("Vui lòng nhập vào nội dung gửi mail.", "Thông báo");
            }
            else if (dgvEmailListSend.Rows.Count < 1)
            {
                checkvaldationmail = true;
                MessageBox.Show("Vui lòng thêm vào danh sách email cần gửi.", "Thông báo");
            }
        }
        protected void runSendEmail_DoWork(object sender, DoWorkEventArgs e)
        {
            int id_next = _getNextID();
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
                List<DataGridViewRow> listEmailSend = (from rows_ in dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                       where Convert.ToBoolean(rows_.Cells["stt_sendmail"].Value) == true
                                                         select rows_).ToList();
                int countListEmailSend = listEmailSend.Count;

                string MsgGET = "";

                if (countListEmailSend < 1)
                {
                    MessageBox.Show("Vui lòng thêm vào danh sách email cần gửi.", "Thông báo");
                }
                else {
                    HTMLEditors.Invoke(new MethodInvoker(delegate { MsgGET = HTMLEditors.DocumentText; }));

                    string txtSQLQuery = "INSERT INTO email_campaign (id, email_name, type_save) VALUES (" + id_next + ", '" + txt_subject_email.Text + "', 2);";
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

                    string _txtSQLQuery = "INSERT INTO content_mail_campaign (mail_object, mail_fullname, typesmtp, content, id_mail_campaign, open_mail, denny_mail, id_mail_add) VALUES ('" + txt_subject_email.Text + "', '" + Library._name_send_mail + "','" + keysmtp + "','" + Library.Base64Encode(MsgGET) + "', " + id_next + ", " + Convert.ToInt32(chkenablereport) + ", " + Convert.ToInt32(chkdennymail) + ", " + Library._mail_add + ");";
                    ExecuteQuery(_txtSQLQuery);

                    foreach (DataGridViewRow rowemailEmailSendy in listEmailSend)
                    {
                        if (!_isStopSendMail)
                        {
                            sendEmailFnc(rowemailEmailSendy, countListEmailSend, MsgGET, rowEMAILList, sum_emai_send_curent, id_next);
                            stt_send++; rowEMAILList++;
                        }
                    }
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.ToString());
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
                List<DataGridViewRow> listEmailSend = (from rows_ in dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                       where Convert.ToBoolean(rows_.Cells["stt_sendmail"].Value) == true
                                                       select rows_).ToList();
                int countListEmailSend = listEmailSend.Count;

                string MsgGET = "";

                if (countListEmailSend < 1)
                {
                    MessageBox.Show("Vui lòng thêm vào danh sách email cần gửi.", "Thông báo");
                }
                else
                {
                    HTMLEditors.Invoke(new MethodInvoker(delegate { MsgGET = HTMLEditors.DocumentText; }));

                    foreach (DataGridViewRow rowemailEmailSendy in listEmailSend)
                    {
                        if (Convert.ToInt64(rowemailEmailSendy.Cells["stt"].Value) >= stt_send && !_isStopSendMail)
                        {
                            sendEmailFnc(rowemailEmailSendy, countListEmailSend, MsgGET, rowEMAILList, sum_emai_send_curent, id_next);
                            stt_send++;
                            rowEMAILList++;
                        }
                    }
                }
            }
        }

        private int _getNextID()
        {
            SQLiteConnection __sqlite_conn;
            SQLiteCommand __sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            __sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");
            __sqlite_conn.Open();
            __sqlite_cmd =__sqlite_conn.CreateCommand();
            __sqlite_cmd.CommandText = "SELECT max(id) AS ids FROM email_campaign";
            sqlite_datareader = __sqlite_cmd.ExecuteReader();

            int _id_next = 0;
            while (sqlite_datareader.Read())
            {
                _id_next = Convert.ToInt32(sqlite_datareader["ids"]) + 1;
            }
            __sqlite_conn.Close();
            return _id_next;
        }

        private void sendEmailFnc(DataGridViewRow rowemailEmailSendy, int countListEmailSend, String MsgGET, int rowEMAILList, int sum_emai_send_curent, int id_next)
        {
            try {
            if (senddingEmail)
            {
                RegistryKey key_regedit2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
                string keysmtp = key_regedit2.GetValue("typesmtp").ToString();
                if ((rowEMAILList >= 470) && (keysmtp == "rgmailsmtp"))
                {
                    MessageBox.Show("Bạn đã chạm giới hạn gửi bằng smtp Gmail, vui lòng sử dụng vào ngày hôm sau");
                    senddingEmail = false;
                    btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Text = "Bắt đầu gửi"; }));
                    btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Enabled = true; }));
                    btn_stop_send_email.Invoke(new MethodInvoker(delegate { btn_stop_send_email.Enabled = false; }));
                    btn_export_report_send_email.Invoke(new MethodInvoker(delegate { btn_export_report_send_email.Enabled = true; }));
                    pgb_send_email.Invoke(new MethodInvoker(delegate { pgb_send_email.Value = 100; }));
                    _isStopSendMail = true;
                }else
                {
                    if (rowEMAILList <= countListEmailSend - 1)
                    {
                        string emailaddress = rowemailEmailSendy.Cells["email"].Value.ToString();
                        string namefullName = rowemailEmailSendy.Cells["fullname_list_email"].Value.ToString();
                        string messages = MsgGET.Replace("</NAME>", namefullName);

                        MailMessage mail = new MailMessage();
                        string smtpclient = _smtpsmtp; // "112.213.92.6";// 
                        SmtpClient SmtpServer = new SmtpClient(smtpclient);
                        string accsmtp = _usernamesmtp;
                        string htmlEmail = "";
                        mail.From = new MailAddress(_addressmailsmtp, Library._name_send_mail);

                        mail.To.Add(emailaddress);
                        mail.Subject = txt_subject_email.Text;
                        mail.IsBodyHtml = true;

                        if (key_regedit2.GetValue("chkdennymail").ToString() == "1")
                        {
                            messages = "<h4><a target='_blank' href='" + Library.apiUrl + "/update-email-denny?id=" + Library.username + "&o=" + txt_subject_email.Text + "&e=" + emailaddress + "'>Bấm vào đây để từ chối nhận thư này.</a></h4></br>" + messages;
                        }

                        htmlEmail = messages;

                        if (key_regedit2.GetValue("chkenablereport").ToString() == "1")
                        {
                            htmlEmail = messages + "<img style='width: 1px;height: 1px;display: none;opacity: 0' src='" + Library.apiUrl + "/update-email-open?id=" + Library.username + "&o=" + txt_subject_email.Text + "&e=" + emailaddress + "'/> <br /> <h3>Gợi ý: Bấm hiển thị hình ảnh hoặc cho phép hiển thị hình ảnh để đọc hết thư này.</h3>";
                        }

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
                        //SmtpServer.Timeout = 99999;

                        if (key_regedit2.GetValue("chkenablereport").ToString() == "1" || key_regedit2.GetValue("chkdennymail").ToString() == "1")
                        {
                            string _uri_save_email = Library.apiUrl + "/save-email?username=" + Library.username + "&email=" + emailaddress + "&fn=" + namefullName + "&object_name=" + txt_subject_email.Text;
                            Library.callAPI(_uri_save_email).ToString();
                        }


                        SQLiteConnection sqlite_conn_attack;
                        SQLiteCommand sqlite_cmd_attack;
                        SQLiteDataReader sqlite_datareader_attack;
                        sqlite_conn_attack = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");

                        sqlite_conn_attack.Open();
                        sqlite_cmd_attack = sqlite_conn_attack.CreateCommand();
                        sqlite_cmd_attack.CommandText = "SELECT * FROM file_attack WHERE id_mail_add = " + Library._mail_add;
                        sqlite_datareader_attack = sqlite_cmd_attack.ExecuteReader();

                        System.Net.Mail.Attachment attachment;

                        while (sqlite_datareader_attack.Read()) // Read() returns true if there is still a result line to read
                        {
                            mail.Attachments.Add(new Attachment(sqlite_datareader_attack["path_file"].ToString()));
                        }

                        sqlite_conn_attack.Close();
                        
                        key_regedit2.Close();


                        SmtpServer.Send(mail);
                        btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Text = "Đang gửi..."; }));
                        rowemailEmailSendy.Cells["status_email_send"].Value = "Đang gửi...";
                        rowemailEmailSendy.Cells["status_email_send"].Style.BackColor = System.Drawing.Color.YellowGreen;
                        rowemailEmailSendy.Cells["status_email_send"].Style.ForeColor = System.Drawing.Color.White;
                        SmtpServer.Dispose();


                        rowemailEmailSendy.Cells["status_email_send"].Value = "Đã gửi...";
                        rowemailEmailSendy.Cells["status_email_send"].Style.BackColor = System.Drawing.Color.Green;
                        rowemailEmailSendy.Cells["status_email_send"].Style.ForeColor = System.Drawing.Color.White;

                        string txtSQLQuery = "UPDATE emails SET type_list = 2 WHERE email_name = '" + emailaddress + "';";
                        ExecuteQuery(txtSQLQuery);

                        string __txtSQLQuery = "INSERT INTO mail_list_campaign (email_name, fullname, type_mail, id_mail_campaign) VALUES ('" + emailaddress + "', '" + namefullName + "', 2, " + id_next + ");";
                        ExecuteQuery(__txtSQLQuery);

                        // hehe, ngồi kéo cho ns tăng cái progressbar lên thoi ma
                        // nhân với tổng số hay cái gi ấy

                        int _amount_sendmail_ok = rowEMAILList + 1;
                        int intsc = _amount_sendmail_ok * sum_emai_send_curent;
                        // check neu ma ku lon hon 100 thi phai xu ly ngay
                        int pgbvalueemailsend = intsc > 100 ? 100 : intsc;
                        // gán lại vào cái p nào, nhưng mà phải chơi cái invoke cho nó máu.

                        pgb_send_email.Invoke(new MethodInvoker(delegate { pgb_send_email.Value = pgbvalueemailsend; }));

                        lbl_email_sended.Invoke(new MethodInvoker(delegate { lbl_email_sended.Text = "Thành công: " + (_amount_sendmail_ok - sendFail) + ", Lỗi: " + sendFail; }));

                        if (rowEMAILList >= countListEmailSend - 1)
                        {
                        
                            senddingEmail = false;
                            btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Text = "Bắt đầu gửi"; }));
                            btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Enabled = true; }));
                            btn_stop_send_email.Invoke(new MethodInvoker(delegate { btn_stop_send_email.Enabled = false; }));
                            btn_export_report_send_email.Invoke(new MethodInvoker(delegate { btn_export_report_send_email.Enabled = true; }));
                            pgb_send_email.Invoke(new MethodInvoker(delegate { pgb_send_email.Value = 100; }));
                        }
                    }
                    else
                    {
                        btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Text = "Bắt đầu gửi"; }));
                        btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Enabled = true; }));
                        btn_stop_send_email.Invoke(new MethodInvoker(delegate { btn_stop_send_email.Enabled = false; }));
                        btn_export_report_send_email.Invoke(new MethodInvoker(delegate { btn_export_report_send_email.Enabled = true; }));
                        senddingEmail = false;
                        pgb_send_email.Invoke(new MethodInvoker(delegate { pgb_send_email.Value = 100; }));
                    }
                }
            }
            else
            {
                btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Enabled = true; }));
                btn_send_email.Invoke(new MethodInvoker(delegate { btn_send_email.Text = "Bắt đầu gửi"; }));
                btn_stop_send_email.Invoke(new MethodInvoker(delegate { btn_stop_send_email.Enabled = false; }));
                btn_export_report_send_email.Invoke(new MethodInvoker(delegate { btn_export_report_send_email.Enabled = true; }));
                senddingEmail = false;
                pgb_send_email.Invoke(new MethodInvoker(delegate { pgb_send_email.Value = 100; }));
            }
            }catch(Exception exa){
                sendFail = sendFail + 1;
                rowemailEmailSendy.Cells["status_email_send"].Value = "Lỗi...";
                rowemailEmailSendy.Cells["status_email_send"].Style.BackColor = System.Drawing.Color.Red;
                rowemailEmailSendy.Cells["status_email_send"].Style.ForeColor = System.Drawing.Color.White;
            }
        }


        protected void runSendEmail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void runSendEmail_ProgressChanged(object sender, ProgressChangedEventArgs e) { }       
        private void btn_add_smtp_Click(object sender, EventArgs e)
        {
            try
            {
                addServerSMTP fmOpenServerSMTP = new addServerSMTP(this);
                fmOpenServerSMTP.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_stop_send_email_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn dừng gửi Email ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                senddingEmail = false;
                pgb_send_email.Value = 100;
                runSendEmailBakground.CancelAsync();
                btn_send_email.Text = "Bắt đầu gửi";
                btn_send_email.Enabled = true;
                btn_stop_send_email.Enabled = false;
                btn_export_report_send_email.Enabled = true;
                MessageBox.Show("Đã dừng gửi Email.");
            }
        }
        private void btn_send_email_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
                if (key_regedit12 != null)
                {
                    if (key_regedit12.GetValue("typesmtp") != null)
                    {
                        string keysmtp = key_regedit12.GetValue("typesmtp").ToString();
                        if (keysmtp == "rgmailsmtp")
                        {
                            _smtpsmtp = key_regedit12.GetValue("glbl_server_smtp").ToString();
                            _portsmtp = key_regedit12.GetValue("glbl_port_smtp_user").ToString();
                            _usernamesmtp = key_regedit12.GetValue("glbl_server_smtp_send").ToString();
                            _passwordsmtp =key_regedit12.GetValue("glbl_pass_smtp").ToString();
                            _addressmailsmtp = key_regedit12.GetValue("gaddressemail").ToString();
                            Library._name_send_mail = key_regedit12.GetValue("gmail_name_send").ToString();
                            _typemethodsmtp = key_regedit12.GetValue("gcbBoxSSL").ToString();
                        }
                        else if (keysmtp == "ramazonsmtp")
                        {
                            _smtpsmtp = key_regedit12.GetValue("albl_server_smtp").ToString();
                            _portsmtp = key_regedit12.GetValue("albl_port_smtp_user").ToString();
                            _usernamesmtp = key_regedit12.GetValue("albl_server_smtp_send").ToString();
                            _passwordsmtp = key_regedit12.GetValue("albl_pass_smtp").ToString();
                            _addressmailsmtp = key_regedit12.GetValue("aaddressemail").ToString();
                            Library._name_send_mail = key_regedit12.GetValue("amazon_name_send").ToString();
                            _typemethodsmtp = key_regedit12.GetValue("acbBoxSSL").ToString();
                        }
                        else
                        {
                            _smtpsmtp = key_regedit12.GetValue("lbl_server_smtp").ToString();
                            _portsmtp = key_regedit12.GetValue("lbl_port_smtp_user").ToString();
                            _usernamesmtp = key_regedit12.GetValue("lbl_server_smtp_send").ToString();
                            _passwordsmtp = key_regedit12.GetValue("lbl_pass_smtp").ToString();
                            _addressmailsmtp = key_regedit12.GetValue("addressemail").ToString();
                            Library._name_send_mail = key_regedit12.GetValue("my_name_send").ToString();
                            _typemethodsmtp = key_regedit12.GetValue("cbBoxSSL").ToString();
                        }

                        checkValidationSendMail();
                        if (!checkvaldationmail)
                        {

                            List<DataGridViewRow> checkListEmailSend = (from row_pending_send_mail in dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                                        where Convert.ToBoolean(row_pending_send_mail.Cells["stt_sendmail"].Value) == true
                                                                        select row_pending_send_mail).ToList();
                            int emailsend = checkListEmailSend.Count;

                            if (emailsend < 1)
                            {
                                MessageBox.Show("Vui lòng thêm vào danh sách email cần gửi.", "Thông báo");
                            }
                            else
                            {
                                sum_emai_send_curent = 100 / emailsend;
                                pgb_send_email.Value = 1;

                                senddingEmail = true;
                                btn_send_email.Text = "  Đang check";
                                btn_send_email.Enabled = false;
                                btn_stop_send_email.Enabled = true;
                                rowEMAILList = 0;
                                lbl_email_sended.Text = "Số mail đã gửi đến khách hàng: 0";
                                foreach (DataGridViewRow _rowemailsend in checkListEmailSend)
                                {
                                    _rowemailsend.Cells["status_email_send"].Value = "Chưa gửi";
                                    _rowemailsend.Cells["status_email_send"].Style.BackColor = System.Drawing.Color.White;
                                    _rowemailsend.Cells["status_email_send"].Style.ForeColor = System.Drawing.Color.Black;
                                }
                                if (!runSendEmailBakground.IsBusy)
                                {
                                    runSendEmailBakground.RunWorkerAsync();
                                }
                            }
                        }
                    }
                }
                else { 
                    MessageBox.Show("Vui lòng cài đặt Mail Server", "Thông báo");
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddEmailListSend_Click(object sender, EventArgs e)
        {
            openFileDialogEmailList.ShowDialog();
        }
        private void btnAddEmailPopup_Click(object sender, EventArgs e)
        {
            try
            {
                addEmailToListSend fmOpenAddEmailtoList = new addEmailToListSend(this);
                fmOpenAddEmailtoList.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            doc.execCommand("FontName", false, cbFont.Text);
        }
        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _fontsize = 0;
            if (cbFontSize.Text == "8")
            {
                _fontsize = 1;
            }
            else if (cbFontSize.Text == "10")
            {
                _fontsize = 2;
            }
            else if (cbFontSize.Text == "12")
            {
                _fontsize = 3;
            }
            else if (cbFontSize.Text == "14")
            {
                _fontsize = 4;
            }
            else if (cbFontSize.Text == "18")
            {
                _fontsize = 5;
            }
            else if (cbFontSize.Text == "24")
            {
                _fontsize = 6;
            }
            else if (cbFontSize.Text == "36")
            {
                _fontsize = 7;
            }
            else if (cbFontSize.Text == "72")
            {
                _fontsize = 8;
            }
            doc.execCommand("fontSize", true, _fontsize);
        }
        private void btn_bold_text_Click(object sender, EventArgs e)
        {
            doc.execCommand("Bold", false, null);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            doc.execCommand("Italic", false, null);
        }
        private void btn_editor_u_Click(object sender, EventArgs e)
        {
            doc.execCommand("UnderLine", false, null);
        }
        private void btn_justifyLeft_Click(object sender, EventArgs e)
        {
            doc.execCommand("justifyLeft", false, null);
        }
        private void btn_justifyCenter_Click(object sender, EventArgs e)
        {
            doc.execCommand("justifyCenter", false, null);
        }
        private void btn_justifyRight_Click(object sender, EventArgs e)
        {
            doc.execCommand("justifyRight", false, null);
        }
        private void btn_justifyFull_Click(object sender, EventArgs e)
        {
            doc.execCommand("justifyFull", false, null);
        }
        private void btn_createLink_Click(object sender, EventArgs e)
        {
            doc.execCommand("createLink", false, null);
        }
        private void btn_Editor_Images_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dlgOpenfile = new OpenFileDialog();
            //Object hasclick = dlgOpenfile.ShowDialog();


            //doc.execCommand("insertImage", false, dlgOpenfile.FileName);
            try
            {
                addImg fmOpenAddImg = new addImg(this);
                fmOpenAddImg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btn_color_text_Click(object sender, EventArgs e)
        {
            ColorDialog dlgColor = new ColorDialog();
            Object hasclick = dlgColor.ShowDialog();
            doc.execCommand("ForeColor", false, "rgb(" + dlgColor.Color.R + ", " + dlgColor.Color.G + ", " + dlgColor.Color.B + ")");
        }
        private void btn_bg_editor_Click(object sender, EventArgs e)
        {
            ColorDialog dlgsColor = new ColorDialog();
            Object hasclick = dlgsColor.ShowDialog();
            doc.execCommand("backColor", false, "rgb(" + dlgsColor.Color.R + ", " + dlgsColor.Color.G + ", " + dlgsColor.Color.B + ")");
        }
        private void btn_redo_Click(object sender, EventArgs e)
        {
            doc.execCommand("undo", false, null);
        }
        private void btn_undo_Click(object sender, EventArgs e)
        {
            doc.execCommand("redo", false, null);
        }
        private void selected_email_send_CheckedChanged(object sender, EventArgs e)
        {
            if (selected_email_send.Checked)
            {
                foreach (DataGridViewRow row in dgvEmailListSend.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvEmailListSend.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
            }
        }
        private void btn_delete_list_mail_send_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> _selectedRows = (from row in dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                       where Convert.ToBoolean(row.Cells["stt_sendmail"].Value) == true
                                                       select row).ToList();
                int countListEMAISEND = _selectedRows.Count;


                if (countListEMAISEND == 0)
                {
                    MessageBox.Show("Vui lòng chọn dòng cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn xóa những dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow rs in _selectedRows)
                        {
                            String _email_delete = rs.Cells["email"].Value.ToString();
                            string txtSQLQuery = "DELETE FROM emails WHERE email_name = '" + _email_delete + "'";
                            ExecuteQuery(txtSQLQuery);
                            dgvEmailListSend.Rows.RemoveAt(rs.Index);
                        }

                        List<DataGridViewRow> Add_selectedRows = (from row in dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                               select row).ToList();

                        int _i_mail = 0;
                        int __i_mail = 0;
                        foreach (DataGridViewRow _rs in Add_selectedRows)
                        {
                            __i_mail = _i_mail + 1;
                            dgvEmailListSend.Rows[_i_mail].Cells[1].Value = __i_mail;
                            dgvEmailListSend.Rows[_i_mail].Cells[0].Value = true;
                            dgvEmailListSend.Rows[_i_mail].Cells[2].Value = _rs.Cells["email"].Value.ToString();
                            dgvEmailListSend.Rows[_i_mail].Cells[3].Value = _rs.Cells["fullname_list_email"].Value.ToString();

                            _i_mail++;
                        }
                    }
                }
            }
            catch { }
        }
        private void btn_export_report_send_email_Click(object sender, EventArgs e)
        {
            sfd_mailcheck.InitialDirectory = "C:";
            sfd_mailcheck.Title = "Savefile";
            sfd_mailcheck.FileName = "report-send-email-with-beecom" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";
            sfd_mailcheck.Filter = "Excel Files|*.xlsx";
            if (sfd_mailcheck.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Application.Workbooks.Add(Type.Missing);
                    Excel.Columns.ColumnWidth = 20;

                    int countColumn = dgvEmailListSend.Columns.Count - 1;
                    int countRows = dgvEmailListSend.Rows.Count;

                    for (int i = 1; i < dgvEmailListSend.Columns.Count - 1; i++)
                    {
                        Excel.Cells[1, i] = dgvEmailListSend.Columns[i].HeaderText;
                    }
                    for (int i = 0; i < countRows; i++)
                    {
                        for (int j = 0; j < countColumn - 1; j++)
                        {
                            if (dgvEmailListSend.Rows[i].Cells[j + 1].Value != null)
                            {
                                Excel.Cells[i + 2, j + 1] = dgvEmailListSend.Rows[i].Cells[j + 1].Value.ToString();
                            }
                        }
                    }
                    Excel.ActiveWorkbook.SaveCopyAs(sfd_mailcheck.FileName.ToString());
                    Excel.ActiveWorkbook.Saved = true;
                    MessageBox.Show("Xuất file thành công");
                    Excel.Quit();
                }
                catch
                {
                    MessageBox.Show("Vui lòng cài đặt office trước khi xuất file báo cáo", "Thông báo");
                }
            }
        }

        # endregion send mail        
        # region drag form
        Boolean flag; int x, y;
        private void main_MouseDown(object sender, MouseEventArgs e){flag = true;x = e.X;y = e.Y;}

        private void main_MouseUp(object sender, MouseEventArgs e){flag = false;}

        private void main_MouseMove(object sender, MouseEventArgs e){if (flag == true){this.SetDesktopLocation(Cursor.Position.X - x, Cursor.Position.Y - y);}}
        # endregion dragfrom
        # region other
        private void RotateImage(PictureBox pb, Image img, float angle)
        {
            if (img == null || pb.Image == null)
                return;

            Image oldImage = pb.Image;
            pb.Image = Library.RotateImage(img, angle);
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }


        private void btn_close_main_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_minisize_main_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        # endregion other
        # region export report check email
        private void button9_Click(object sender, EventArgs e)
        {
            sfd_mailcheck.InitialDirectory = "C:";
            sfd_mailcheck.Title = "Savefile";
            sfd_mailcheck.FileName = "report-verify-with-beecom" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";
            sfd_mailcheck.Filter = "Excel Files|*.xlsx";
            if (sfd_mailcheck.ShowDialog() != DialogResult.Cancel)
            {
                try {
                    Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Application.Workbooks.Add(Type.Missing);
                    Excel.Columns.ColumnWidth = 20;

                    int countColumn = dgvverifyok.Columns.Count - 1;
                    int countRows = dgvverifyok.Rows.Count;

                    for (int i = 1; i < dgvverifyok.Columns.Count; i++)
                    {
                        Excel.Cells[1, i] = dgvverifyok.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < countRows; i++)
                    {
                        for (int j = 0; j < countColumn; j++)
                        {
                            if (dgvverifyok.Rows[i].Cells[j].Value != null)
                            {
                                Excel.Cells[i + 2, j + 1] = dgvverifyok.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                    Excel.ActiveWorkbook.SaveCopyAs(sfd_mailcheck.FileName.ToString());
                    Excel.ActiveWorkbook.Saved = true;
                    MessageBox.Show("Xuất file thành công");
                    Excel.Quit();
                }catch{
                    MessageBox.Show("Vui lòng cài đặt office trước khi xuất file báo cáo", "Thông báo");
                }
            }
        }

        #endregion

        private void panel_sms_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkallemailverify_CheckedChanged(object sender, EventArgs e)
        {
            if (checkallemailverify.Checked)
            {
                foreach (DataGridViewRow row in dataGridListMailCheck.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridListMailCheck.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RegistryKey keyt13 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
            keyt13.SetValue("emailcheck", txt_account_smtp_verify.Text);
            MessageBox.Show("Lưu địa chỉ check thành công");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "xlsx";

                saveFileDialog.InitialDirectory = @"Desktop";
                saveFileDialog.FileName = "sample" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";

                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx |All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    const string MyFileName = "sample.xlsx";
                    string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                    var filePath = Path.Combine(execPath, MyFileName);
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook book = app.Workbooks.Open(filePath);

                    book.SaveAs(saveFileDialog.FileName); //Save
                    book.Close();
                    MessageBox.Show("Tải file thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Để tải được file mẫu, bạn vui lòng cài đặt Office");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "xlsx";

                saveFileDialog.InitialDirectory = @"Desktop";
                saveFileDialog.FileName = "sample" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";

                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx |All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    const string MyFileName = "sample.xlsx";
                    string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                    var filePath = Path.Combine(execPath, MyFileName);
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook book = app.Workbooks.Open(filePath);

                    book.SaveAs(saveFileDialog.FileName); //Save
                    book.Close();
                    MessageBox.Show("Tải file thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Để tải được file mẫu, bạn vui lòng cài đặt Office");
            }
        }

        private void checkSendSms_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime dt = DateTime.Parse(now.ToString());
            int timeBook = 0;
            int DateCurrent = 0;
            RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
            if (key_regedit12.GetValue("gio") != null)
            {
                int nam = Convert.ToInt32(key_regedit12.GetValue("nam"));
                int thang = Convert.ToInt32(key_regedit12.GetValue("thang"));
                int ngay = Convert.ToInt32(key_regedit12.GetValue("ngay"));
                int gio = Convert.ToInt32(key_regedit12.GetValue("gio"));
                int phut = Convert.ToInt32(key_regedit12.GetValue("phut"));

                timeBook = nam +thang +ngay +gio +phut;

            }

            int _nam = Convert.ToInt32(dt.Year.ToString());
            int _thang = Convert.ToInt32(dt.Month.ToString());
            int _ngay = Convert.ToInt32(dt.Day.ToString());
            int _gio = Convert.ToInt32(dt.Hour.ToString());
            int _phut = Convert.ToInt32(dt.Minute.ToString());

            DateCurrent = _nam + _thang + _ngay + _gio + _phut;

            if (DateCurrent >= timeBook && Convert.ToInt32(key_regedit12.GetValue("isRunBookActionSendSMS")) == 0)
            {
                bookActionSendSMS();
            }
            else {
                if (Convert.ToInt32(key_regedit12.GetValue("isRunBookActionSendSMS")) == 0)
                {
                    lblbook.Text = "Bạn có 1 lịch gửi SMS được chạy vào lúc: " + key_regedit12.GetValue("gio") + ":" + key_regedit12.GetValue("phut") + "   " + key_regedit12.GetValue("ngay") + "/" + key_regedit12.GetValue("thang") + "/" + key_regedit12.GetValue("nam");
                }
            }

        }

        private void bookActionSendSMS()
        {
            try
            {
                // set câu thông báo nếu mà ku cho nó chưa được chọn
                isShowDeviceNotSelected = true;
                senddingsms = true;
                if (selectCOM)
                {
                    lbl_send_sms.Text = "Số tin nhắn đã gửi đến khách hàng: 0";
                    List<DataGridViewRow> checklistMobileSendSelect = (from rowssmschecklist in dgvMobile.Rows.Cast<DataGridViewRow>()
                                                                       where Convert.ToBoolean(rowssmschecklist.Cells["select"].Value) == true
                                                                       select rowssmschecklist).ToList();
                    int mobile = checklistMobileSendSelect.Count;
                    sum_sms_send_curent = 100 / mobile;
                    pgbsendsms.Value = 1;
                    startsmssend = false;
                    if (mobile == 0)
                    {
                        lblbook.Text = "Hệ thống không thể gửi được SMS từ lịch đặt này do chưa có danh sách số điện thoại nhận tin nhắn.";
                    }
                    else
                    {
                        btn_start_sms.Text = "Đang gửi...";
                        btn_start_sms.Enabled = false;
                        btn_pause_sms.Enabled = true;

                        rowSMSList = 0;
                        sendSMSSuccess = 0;
                        int countDgvMobile = dgvMobile.Rows.Count;

                        foreach (DataGridViewRow rowlistcom in listCOM.Rows)
                        {
                            rowlistcom.Cells["status"].Value = 0;
                            rowlistcom.Cells["numbersends"].Value = 0;
                        }

                        foreach (DataGridViewRow rowlistSms in checklistMobileSendSelect)
                        {
                            rowlistSms.Cells["status_sms"].Value = "Đang gửi...";
                            rowlistSms.Cells["status_sms"].Style.BackColor = System.Drawing.Color.Green;
                            rowlistSms.Cells["status_sms"].Style.ForeColor = System.Drawing.Color.White;
                        }
                        if (!runSendSmsBakground.IsBusy)
                        {
                            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
                            //storing the values  
                            keyt123.SetValue("isRunBookActionSendSMS", 1);
                            runSendSmsBakground.RunWorkerAsync();
                        }
                    }
                }
                else
                {
                    lblbook.Text = "Hệ thống không thể gửi được SMS từ lịch đặt này do không tìm thấy được thiết bị gửi, vui lòng bấm nút tìm kiếm thiết bị.";
                }
            }
            catch (Exception ex)
            {
                string _error_sms = ex.Message;
                if (_error_sms == "The requested resource is in use.")
                {
                    lblbook.Text = "Hệ thống không thể gửi được SMS từ lịch đặt này bạn vui lòng tắt phần mềm của nhà mạng";
                }
                else if (_error_sms == "Object reference not set to an instance of an object.")
                {
                    btn_start_sms.Enabled = true;
                    btn_start_sms.Text = "Bắt đầu gửi";
                    btn_pause_sms.Enabled = false;
                    lblbook.Text = "Hệ thống không thể gửi được SMS từ lịch đặt này do chưa có danh sách số điện thoại nhận tin nhắn";
                }
                else
                {
                    lblbook.Text = "Hệ thống không thể gửi được SMS từ lịch đặt này do chưa có danh sách số điện thoại nhận tin nhắn";
                }
            }
        }

     

        private int width = 0;
        private int height = 0;
        private int xposition = 0;
        private int yposition = 0;

        private void btnZoomWindow_Click(object sender, EventArgs e)
        {
            if (Windowmax)
            {
                //this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Windowmax = false;
                //this.WindowState = FormWindowState.Maximized;
                this.Width = width;
                this.Height = height;
                this.Location = new Point(xposition, yposition);
                this.StartPosition = FormStartPosition.Manual;
            }
            else
            {
                width = this.Width;
                height = this.Height;
                xposition = Location.X;
                yposition = Location.Y;
                Windowmax = true;
                //this.WindowState = FormWindowState.Maximized;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height - (Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height);
                this.Location = new Point();
                this.StartPosition = FormStartPosition.Manual;
            }
        }

        private void addEmailCheckPopup_Click(object sender, EventArgs e)
        {
            try
            {
                addEmailToListCheck addEmailToListCheck = new addEmailToListCheck(this);
                addEmailToListCheck.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ManageAttackMail fmManageAttackMail = new ManageAttackMail(this);
                fmManageAttackMail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_facebook_Click(object sender, EventArgs e)
        {
            if (!_showtabfb)
            {
                webBrowser3.Navigate(new Uri(Library.facebook + "/?id=" + Library.getMd5(Library.username)));
                _showtabfb = true;
            }
            tabControl1.SelectedTab = tabpage5;
            restartBackColor();
        }

        bool renderSelectNam = false;
        bool renderSelectThang = false;
        bool renderSelectNgay = false;

        private void rendernam()
        {
            if (!renderSelectNam)
            {
                cbo_from_year.Items.Add("Năm");
                cbo_to_year.Items.Add("Năm");
                string nam = "";
                for (int ix = 2015; ix < 2020; ix++)
                {
                    if (ix < 10)
                    {
                        nam = "0" + ix;
                    }
                    else
                    {
                        nam = ix.ToString();
                    }
                    cbo_from_year.Items.Add(nam);
                    cbo_to_year.Items.Add(nam);
                }
                renderSelectNam = true;
            }
            
        }

        private void renderthang()
        {
            if (!renderSelectThang)
            {
                string thang = "";
                cbo_from_month.Items.Add("Tháng");
                cbo_to_month.Items.Add("Tháng");
                for (int ix = 1; ix < 13; ix++)
                {
                    if (ix < 10)
                    {
                        thang = "0" + ix;
                    }
                    else
                    {
                        thang = ix.ToString();
                    }
                    cbo_from_month.Items.Add(thang);
                    cbo_to_month.Items.Add(thang);
                }
                renderSelectThang = true;
            }
            
        }

        private void renderngay()
        {
            if (!renderSelectNgay)
            {
                string ngay = "";
                cbo_from_day.Items.Add("Ngày");
                cbo_to_day.Items.Add("Ngày");
                for (int ix = 1; ix < 32; ix++)
                {
                    if (ix < 10)
                    {
                        ngay = "0" + ix;
                    }
                    else
                    {
                        ngay = ix.ToString();
                    }
                    cbo_from_day.Items.Add(ngay);
                    cbo_to_day.Items.Add(ngay);
                }
                renderSelectNgay = true;
            }
            
        }

        private void btn_report_email_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage6;
            restartBackColor();

            Uri uri = new Uri(Library.apiUrl+"/get-list-object?username=" + Library.username);

            HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
            requestFile.ContentType = "application/json";
            HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
            if (requestFile.HaveResponse)
            {
                if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                    dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                    int state = dynObj.state;
                    if (state == 200)
                    {
                        cbo_object_email.Text = "Tiêu đề nội dung email gửi đi";
                        cbo_object_email.Items.Clear();
                        cbo_object_email.Items.Add("Tiêu đề nội dung email gửi đi");
                        foreach (var obj in dynObj.data)
                        {
                            cbo_object_email.Items.Add(obj.object_name);
                        }
                    }
                    else
                    {
                        cbo_object_email.Text = "Không tìm thấy danh sách nào";
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại internet.");
                }

                cbotypereportemail.Items.Clear();
                cbotypereportemail.Items.Add("Tất cả");
                cbotypereportemail.Items.Add("Email khách hàng đã mở");
                cbotypereportemail.Items.Add("Email khách hàng chưa mở");
                cbotypereportemail.Items.Add("Email khách hàng từ chối nhận email");

                renderngay();
                renderthang();
                rendernam();
            }

        }

        private void btn_search_list_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbo_object_email.SelectedIndex == 0 || cbo_object_email.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn tiêu đề email gửi đi.", "Thông báo");
                }
                else if (cbotypereportemail.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn kiểu báo cáo.", "Thông báo");
                }
                else if (cbo_from_day.SelectedIndex == 0 || cbo_from_month.SelectedIndex == 0 || cbo_from_year.SelectedIndex == 0 || cbo_to_day.SelectedIndex == 0 || cbo_to_month.SelectedIndex == 0 || cbo_to_year.SelectedIndex == 0 || cbo_from_day.SelectedIndex == -1 || cbo_from_month.SelectedIndex == -1 || cbo_from_year.SelectedIndex == -1 || cbo_to_day.SelectedIndex == -1 || cbo_to_month.SelectedIndex == -1 || cbo_to_year.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn đầy đủ thời gian cần báo cáo.", "Thông báo");
                }
                else
                {

                    var sstart_date = cbo_from_year.SelectedItem.ToString() + cbo_from_month.SelectedItem.ToString() + cbo_from_day.SelectedItem.ToString();
                    var send_date = cbo_to_year.SelectedItem.ToString() + cbo_to_month.SelectedItem.ToString() + cbo_to_day.SelectedItem.ToString();

                    if (Int32.Parse(sstart_date) > Int32.Parse(send_date))
                    {
                        MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc");
                    }
                    else {
                        if (!getListEmailReportBackground.IsBusy)
                        {
                            getListEmailReportBackground.RunWorkerAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        protected void getListEmailReportBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
                    
                btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Text = "Đang thực hiện"; }));
                btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Enabled = false; }));
                btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Enabled = true; }));
                 var ob_name = "";

                 var type_report = 0;
                 var cbo_from_years = "";
                 var cbo_from_months = "";
                 var cbo_from_days = "";
                 var cbo_to_years = "";
                 var cbo_to_months = "";
                 var cbo_to_days = "";

                 cbo_object_email.Invoke(new MethodInvoker(delegate { ob_name = cbo_object_email.SelectedItem.ToString(); }));
                cbo_object_email.Invoke(new MethodInvoker(delegate { type_report = cbotypereportemail.SelectedIndex; }));

                cbo_from_year.Invoke(new MethodInvoker(delegate { cbo_from_years = cbo_from_year.SelectedItem.ToString(); }));
                cbo_from_month.Invoke(new MethodInvoker(delegate { cbo_from_months = cbo_from_month.SelectedItem.ToString(); }));
                cbo_from_day.Invoke(new MethodInvoker(delegate { cbo_from_days = cbo_from_day.SelectedItem.ToString(); }));

                cbo_to_year.Invoke(new MethodInvoker(delegate { cbo_to_years = cbo_to_year.SelectedItem.ToString(); }));
                cbo_to_month.Invoke(new MethodInvoker(delegate { cbo_to_months = cbo_to_month.SelectedItem.ToString(); }));
                cbo_to_day.Invoke(new MethodInvoker(delegate { cbo_to_days = cbo_to_day.SelectedItem.ToString(); }));

                Uri uri = new Uri(Library.apiUrl + "/get-email2?username=" + Library.username + "&object_name=" + Base64Encode(ob_name) + "&type_report=" + type_report + "&start_date=" + cbo_from_years + "-" + cbo_from_months + "-" + cbo_from_days + "&end_date=" + cbo_to_years + "-" + cbo_to_months + "-" + cbo_to_days);
                Console.Write(Library.apiUrl + "/get-email2?username=" + Library.username + "&object_name=" + Base64Encode(ob_name) + "&type_report=" + type_report + "&start_date=" + cbo_from_years + "-" + cbo_from_months + "-" + cbo_from_days + "&end_date=" + cbo_to_years + "-" + cbo_to_months + "-" + cbo_to_days);
                HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
                requestFile.ContentType = "application/json";
                HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
                if (requestFile.HaveResponse)
                {
                    if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                    {
                        StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                        dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                        int state = dynObj.state;
                        if (state == 200)
                        {
                            dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows.Clear(); }));
                            //dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows.Add(); }));
                            var countEmailrp = 0;
                            var countEmailrow = 0;
                            foreach (var obj in dynObj.data)
                            {
                                countEmailrow = countEmailrp + 1;

                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows.Add(); }));
                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[0].Value = countEmailrow.ToString(); }));
                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[1].Value = obj.email.ToString(); }));
                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[2].Value = obj.fullname.ToString(); }));
                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[3].Value = obj.object_name.ToString(); }));

                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[4].Value = obj.created.ToString(); }));
                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[5].Value = obj.open_date.ToString(); }));
                                dgvEmailAReportAPI.Invoke(new MethodInvoker(delegate { dgvEmailAReportAPI.Rows[countEmailrp].Cells[6].Value = (obj.open_state == 0 ? "Chưa mở" : (obj.open_state == 1 ? "Đã mở" : "Đã mở/Từ chối")); }));
                                countEmailrp = countEmailrp + 1;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu cho điều kiện bạn chọn.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng kiểm tra lại internet.");
                    }

                    btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Text = "Đồng ý"; }));
                    btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Enabled = true; }));
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Text = "Đồng ý"; }));
                btn_search_list.Invoke(new MethodInvoker(delegate { btn_search_list.Enabled = true; }));
                getListEmailReportBackground.CancelAsync();
            }
        }
        protected void getListEmailReportBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void getListEmailReportBackground_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void btn_export_report_email_Click(object sender, EventArgs e)
        {
            saveFileReportEmail.InitialDirectory = "C:";
            saveFileReportEmail.Title = "Savefile";
            saveFileReportEmail.FileName = "report-email-open-with-beecom" + DateTime.Now.ToString("HH-mm-ss_d-MM-yyyy") + ".xlsx";
            saveFileReportEmail.Filter = "Excel Files|*.xlsx";
            if (saveFileReportEmail.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Application.Workbooks.Add(Type.Missing);
                    Excel.Columns.ColumnWidth = 20;

                    int countColumn = dgvEmailAReportAPI.Columns.Count;
                    int countRows = dgvEmailAReportAPI.Rows.Count;

                    for (int i = 1; i < dgvEmailAReportAPI.Columns.Count; i++)
                    {
                        Excel.Cells[1, i] = dgvEmailAReportAPI.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < countRows; i++)
                    {
                        for (int j = 0; j < countColumn; j++)
                        {
                            if (dgvEmailAReportAPI.Rows[i].Cells[j].Value != null)
                            {
                                Excel.Cells[i + 2, j + 1] = dgvEmailAReportAPI.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                    Excel.ActiveWorkbook.SaveCopyAs(saveFileReportEmail.FileName.ToString());
                    Excel.ActiveWorkbook.Saved = true;
                    MessageBox.Show("Xuất file thành công");
                    Excel.Quit();
                }
                catch
                {
                    MessageBox.Show("Vui lòng cài đặt office trước khi xuất file báo cáo", "Thông báo");
                }
            }
        }

        private void btn_contact_Click(object sender, EventArgs e)
        {
            if (!_showtabcontact)
            {
                webBrowser1.Navigate(new Uri(Library.domain + "contact-app/?id=" + Library.getMd5(Library.username)));
                _showtabcontact = true;
            }
            tabControl1.SelectedTab = tabPage4;
            restartBackColor();
            btn_contact.BackColor = System.Drawing.Color.WhiteSmoke;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection sqlite_conn;
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");

            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM emails";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            int _i_mail = 0;
            dgvEmailListSend.Rows.Clear();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                dgvEmailListSend.Rows.Add();
                dgvEmailListSend.Rows[_i_mail].Cells[4].Value = _i_mail;
                dgvEmailListSend.Rows[_i_mail].Cells[0].Value = true;
                dgvEmailListSend.Rows[_i_mail].Cells[1].Value = sqlite_datareader["email_name"].ToString();
                dgvEmailListSend.Rows[_i_mail].Cells[2].Value = sqlite_datareader["fullname"].ToString();

                String lbl_status_send = "Chưa gửi";
                
                if (Convert.ToInt32(sqlite_datareader["type_list"]) != 1)
                {
                    lbl_status_send = "Đã gửi";
                    dgvEmailListSend.Rows[_i_mail].Cells[3].Style.BackColor = System.Drawing.Color.Green;
                    dgvEmailListSend.Rows[_i_mail].Cells[3].Style.ForeColor = System.Drawing.Color.White;

                }

                dgvEmailListSend.Rows[_i_mail].Cells[3].Value = lbl_status_send;
                _i_mail++;
            }
            sqlite_conn.Close();
        }

        private void btn_email_is_sended_Click(object sender, EventArgs e)
        {
            try
            {
                mailsend fmmailsend = new mailsend(this);
                fmmailsend.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_email_is_pendding_Click(object sender, EventArgs e)
        {
            try
            {
                mailPending fmmailPending = new mailPending(this);
                fmmailPending.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_select_temp_Click(object sender, EventArgs e)
        {
            try
            {
                templateEmail fmtemplateEmail = new templateEmail(this);
                fmtemplateEmail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_save_email_tmp_Click(object sender, EventArgs e)
        {
            try
            {
                save_email_tmp fmsave_email_tmp = new save_email_tmp(this);
                fmsave_email_tmp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HTMLEditors_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //HTMLEditors.DocumentText = template;
            //doc = HTMLEditors.Document.DomDocument as IHTMLDocument2;
            //doc.designMode = "On";
        }

        private int rowIndex = 0;
        
        private void dgvEmailListSend_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);               
            }
        }

        private void deleteselected_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa những dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                var selectedRows = dgvEmailListSend.SelectedRows
                                        .OfType<DataGridViewRow>()
                                        .Where(row => !row.IsNewRow)
                                        .ToArray();
                foreach (var row in selectedRows)
                {
                    dgvEmailListSend.Rows.Remove(row);
                }

                List<DataGridViewRow> Add_selectedRows = (from row in dgvEmailListSend.Rows.Cast<DataGridViewRow>()
                                                          select row).ToList();

                int _i_mail = 0;
                int __i_mail = 0;
                foreach (DataGridViewRow _rs in Add_selectedRows)
                {
                    __i_mail = _i_mail + 1;
                    dgvEmailListSend.Rows[_i_mail].Cells[1].Value = __i_mail;
                    dgvEmailListSend.Rows[_i_mail].Cells[0].Value = true;
                    dgvEmailListSend.Rows[_i_mail].Cells[2].Value = _rs.Cells["email"].Value.ToString();
                    dgvEmailListSend.Rows[_i_mail].Cells[3].Value = _rs.Cells["fullname_list_email"].Value.ToString();

                    _i_mail++;
                }
            }
        }

        private void deletelistsms_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa những dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedRows = dgvMobile.SelectedRows
                                        .OfType<DataGridViewRow>()
                                        .Where(row => !row.IsNewRow)
                                        .ToArray();
                foreach (var row in selectedRows)
                {
                    dgvMobile.Rows.Remove(row);
                }
            }
        }

        private void dgvMobile_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        private void dataGridListMailCheck_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip3.Show(Cursor.Position);
            }
        }

        private void deleteemail_verify_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa những dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var selectedRows = dataGridListMailCheck.SelectedRows
                                        .OfType<DataGridViewRow>()
                                        .Where(row => !row.IsNewRow)
                                        .ToArray();
                foreach (var row in selectedRows)
                {
                    dataGridListMailCheck.Rows.Remove(row);
                }
            }
        }
       
         
    }

}
    