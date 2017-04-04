using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.IO.Ports;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.PduConverter;
using GsmComm.Server;

namespace SMOvietnam
{
    public class Library
    {

        private static string _username = "";
        private static string _checkCOM = "";

        private static int __mail_add = 0;

        public static int _mail_add
        {
            get { return __mail_add; }
            set { __mail_add = value; }
        }

        private static string __name_send_mail = "";
        public static string _name_send_mail
        {
            get { return __name_send_mail; }
            set { __name_send_mail = value; }
        }

        public static string checkCOM
        {
            get { return _checkCOM; }
            set { _checkCOM = value; }
        }


        public static string username
        {
            get { return _username; }
            set { _username = value; }
        }

        private static string _password = "";

        public static string password
        {
            get { return _password; }
            set { _password = value; }
        }

        private static string _uuiduser = getUUIDCustom();

        public static string uuiduser
        {
            get { return _uuiduser; }
        }

        private static string _apiurl = "http://api.beecom.vn/";

        public static string apiUrl
        {
            get { return _apiurl; }
            set { _apiurl = value; }
        }

        private static string _domain = "http://test.beecom.vn/";

        public static string domain
        {
            get { return _domain; }
        }

        private static string _facebook = "http://facebook.beecom.vn";

        public static string facebook
        {
            get { return _facebook; }
        }

        public static string Str2Hex(string strMessage)
        {
            byte[] ba = Encoding.BigEndianUnicode.GetBytes(strMessage);
            string strHex = BitConverter.ToString(ba);
            strHex = strHex.Replace("-", "");
            return strHex;
        }

        public static bool IsUnicode(string input)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
            return asciiBytesCount != unicodBytesCount;
        }

        private SmsSubmitPdu[] CreateConcatMessage(string message, string number, bool unicode, bool showParts)
        {
            SmsSubmitPdu[] pdus = null;
            try
            {
                if (!unicode)
                {

                    pdus = GsmComm.PduConverter.SmartMessaging.SmartMessageFactory.CreateConcatTextMessage(message, number);
                }
                else
                {

                    pdus = GsmComm.PduConverter.SmartMessaging.SmartMessageFactory.CreateConcatTextMessage(message, true, number);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return pdus;
        }


 

        public static string sendATSMSCommand(string PortName, string cellNo, string messages, object _sendFailer)
        {
            int baudRate = 9600;
            int timeout = 300;
            GsmCommMain comm;

            comm = new GsmCommMain(PortName, baudRate, timeout);

            try
            {
                comm.Open();
            }
            catch (Exception)
            {
                //return false;
            }

            try
            {
                SmsSubmitPdu[] pdus;

                bool unicode = Library.IsUnicode(messages);
                try
                {
                    if (!unicode)
                    {

                        pdus = GsmComm.PduConverter.SmartMessaging.SmartMessageFactory.CreateConcatTextMessage(messages, cellNo);
                    }
                    else
                    {

                        pdus = GsmComm.PduConverter.SmartMessaging.SmartMessageFactory.CreateConcatTextMessage(messages, true, cellNo);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

                foreach (SmsSubmitPdu pdu in pdus)
                {
                    comm.SendMessage(pdu);
                }

                
                return "1";

            }
            catch (Exception ex)
            {
                return ex.ToString();
                //return false;
            }
            finally
            {
                if (comm != null)
                {
                    comm.Close();
                }
            }
        }


        public static string getUUIDCustom()
        {
            try
            {
                string drive = "C";
                if (drive == string.Empty)
                {
                    //Find first drive
                    foreach (DriveInfo compDrive in DriveInfo.GetDrives())
                    {
                        if (compDrive.IsReady)
                        {
                            drive = compDrive.RootDirectory.ToString();
                            break;
                        }
                    }
                }

                if (drive.EndsWith(":\\"))
                {
                    //C:\ -> C
                    drive = drive.Substring(0, drive.Length - 2);
                }

                ManagementObject disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                disk.Get();

                string volumeSerial = disk["VolumeSerialNumber"].ToString();
                disk.Dispose();


                string cpuInfo = "";
                ManagementClass managClass = new ManagementClass("win32_processor");
                ManagementObjectCollection managCollec = managClass.GetInstances();

                foreach (ManagementObject managObj in managCollec)
                {
                    if (cpuInfo == "")
                    {
                        //Get only the first CPU's ID
                        cpuInfo = managObj.Properties["processorID"].Value.ToString();
                        break;
                    }
                }

                //Mix them up and remove some useless 0's
                return cpuInfo.Substring(13) + cpuInfo.Substring(1, 4) + volumeSerial + cpuInfo.Substring(4, 4);
            }catch
            {
                int _errorProcess = 201;
                return "" + _errorProcess + "";
            }
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string getMd5(String txt)
        {
            String str = "";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("x2");
            }
            return str;
        }

        public static bool callAPI(string link)
        {
            Uri uri = new Uri(link);
            HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
            requestFile.ContentType = "application/json";
            
            HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
          
            if (requestFile.HaveResponse)
            {
                if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                    dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                    if (dynObj == true)
                    {
                        return true;
                    }
                }
                else
                { return false; }
            }
            return false;
        }

        public static string checkMsg()
        {
            Uri uri = new Uri(Library.apiUrl + "/check-msg-sytem");
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
                    if (status == 1)
                    {
                        return dynObj.content;
                    }
                    else {
                        return "[ Chưa có thông báo ]";
                    }
                }
                else
                { return "[ Chưa có thông báo ]"; }
            }
            return "[ Chưa có thông báo ]";
        }

        public static string verifyEmail(string email, string sender)
        {
            Uri uri = new Uri(Library.apiUrl + "/verify-email?email=" + email + "&sender=" + sender + "");
           
            HttpWebRequest requestFile = (HttpWebRequest)WebRequest.Create(uri);
            requestFile.ContentType = "application/json";

            HttpWebResponse webResp = requestFile.GetResponse() as HttpWebResponse;
            if (requestFile.HaveResponse)
            {
                if (webResp.StatusCode == HttpStatusCode.OK || webResp.StatusCode == HttpStatusCode.Accepted)
                {
                    StreamReader respReader = new StreamReader(webResp.GetResponseStream());
                    //dynamic dynObj = Newtonsoft.Json.JsonConvert.DeserializeObject(respReader.ReadToEnd());
                    string data = respReader.ReadToEnd();
                    return data;
                }
                else
                { return "=201"; }
            }
            return "=201";
        }

        public static Bitmap RotateImage(Image image, float angle)
        {
            return RotateImage(image, new PointF((float)image.Width / 2, (float)image.Height / 2), angle);
        }

        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

        public static string nameISP(string nId)
        {
            switch (nId)
            {
                case "45201":
                    return "Mobifone";
                case "45202":
                    return "Vinaphone";
                case "45203":
                    return "Beeline";
                case "45204":
                    return "Viettel";
                case "45205":
                    return "Vietnamobile";
                case "VIETTEL":
                    return "Viettel";                    
                case "VINAPHONE":
                    return "Vinaphone";
                case "MOBIFONE":
                    return "Mobifone";
                case "VNM":
                    return "Mobifone";
                case "VIETNAMOBILE":
                    return "Vietnamobile";
                case "BEELINE":
                    return "Beeline";
                default:
                    return "Không xác định";   
            }
        }

        public static string getNumberDelay(string nId)
        {
            switch (nId)
            {
                case "Mobifone":
                    return "36|300";
                case "Vinaphone":
                    return "26|300";
                case "Beeline":
                    return "26|300";
                case "Viettel":
                    return "71|150";
                case "Vietnamobile":
                    return "26|300";
                default:
                    return "0|0";
            }
        }
        public static string getNameISP(string PortName)
        {
            SerialPort port = null;
            try
            {
                port = new SerialPort(PortName, 9600);
                port.ReadTimeout = 500;
                port.Open();
                port.Write("AT+COPS?\r\n");
                System.Threading.Thread.Sleep(1000);
                string vfc = port.ReadExisting();

                string[] split = vfc.Split(new Char[] { '"' });
                string ddc = split[1];
                port.Close();
                return Library.nameISP(ddc);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                if (port != null)
                {
                    port.Close();
                }
            }
        }

        public static bool IsModem(string PortName)
        {
            SerialPort port = null;
            try
            {
                port = new SerialPort(PortName, 9600);
                port.ReadTimeout = 200;
                port.Open();
                port.Write("AT\r\n");
                for (int i = 0; i < 4; i++)
                {
                    string line = port.ReadLine();
                    if (line.IndexOf("OK") != -1)
                    {
                        port.Close();
                        if (checkInList(PortName))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (port != null)
                {
                    port.Close();
                }
            }
        }

        private static bool checkInList(string PortName)
        {
            SerialPort port = null;
            try
            {
                //return true;
                port = new SerialPort(PortName, 9600);
                port.ReadTimeout = 200;
                port.Open();
                port.Write("AT+GSN\r\n");
                System.Threading.Thread.Sleep(500);
                string vfc = port.ReadExisting();
                port.Close();
                if (vfc != Library.checkCOM)
                {
                    Library.checkCOM = vfc;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (port != null)
                {
                    port.Close();
                }
            }

            return false;
        }

        public static string checkNetworkNumber(string nId)
        {
            if (nId == "90" || nId == "93" || nId == "120" || nId == "121" || nId == "122" || nId == "126" || nId == "128")
            {
                return "45201";
            }

            if (nId == "91" || nId == "94" || nId == "123" || nId == "124" || nId == "125" || nId == "127" || nId == "129")
            {
                return "45202";
            }

            if (nId == "99")
            {
                return "45203";
            }

            if (nId == "96" || nId == "97" || nId == "98" || nId == "163" || nId == "164" || nId == "165" || nId == "166" || nId == "167" || nId == "168" || nId == "169")
            {
                return "45204";
            }

            if (nId == "92" || nId == "188")
            {
                return "45205";
            }
            return "201";
        }


        public static string selectNetworkNumber(string nId)
        {
            string number = nId.Substring(0, 2);
            if (number == "90" || number == "93")
            {
                return "Mobifone";
            }

            if (number == "12")
            {
                string _number = nId.Substring(0, 3);
                if (_number == "123" || _number == "124" || _number == "125" || _number == "127" || _number == "129")
                {
                    return "Vinaphone";
                }
                if (_number == "120" || _number == "121" || _number == "122" || _number == "126" || _number == "128")
                {
                    return "Mobifone";
                }
            }

            if (number == "91" || number == "94")
            {
                return "Vinaphone";
            }

            if (number == "99")
            {
                return "Beeline";
            }

            if (number == "96" || number == "97" || number == "98" || number == "16")
            {
                 return "Viettel";
            }

            if (number == "92" ||number == "188")
            {
                return "Vietnamobile";
            }
            
            return "201";
        }


    }
}
