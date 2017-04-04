using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using mshtml;

namespace SMOvietnam
{
    public partial class templateEmail : Form
    {

        public int nodes = 0;
        public String url_template = "";
        public string downloadString = "";
        public static IHTMLDocument2 doc;
        private BackgroundWorker selectMailBackground = new BackgroundWorker();
        main mainForm;
        TreeNode tNode;
        public templateEmail(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();

            Uri uri = new Uri(Library.apiUrl + "/list-template");
            int iss = 0;
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
                        nodes = dynObj.node;
                        foreach (var obj in dynObj.data)
                        {
                            tNode = treeView1.Nodes.Add("", obj.name_template.ToString());
                            foreach (var objs in obj.temp)
                            {
                                if (iss == 0)
                                {
                                    treeView1.Nodes[0].Toggle();
                                    WebClient client = new WebClient();
                                    downloadString = client.DownloadString(objs.file_path.ToString());
                                    webBrowser1.Navigate(new Uri(objs.file_path.ToString()));
                                }
                                treeView1.Nodes[iss].Nodes.Add(objs.file_path.ToString(), objs.name_template.ToString());

                            }
                            iss++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại internet.");
                }
            }
            
            selectMail();

            

        }

        private void selectMail()
        {
            selectMailBackground.DoWork += new DoWorkEventHandler(selectMailBackground_DoWork);
            selectMailBackground.RunWorkerCompleted += new RunWorkerCompletedEventHandler(selectMailBackground_RunWorkerCompleted);
            selectMailBackground.ProgressChanged += new ProgressChangedEventHandler(selectMailBackground_ProgressChanged);
            selectMailBackground.WorkerReportsProgress = true; selectMailBackground.WorkerSupportsCancellation = true;
        }

        protected void selectMailBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event

                WebClient client = new WebClient();
                downloadString = client.DownloadString(url_template);
                webBrowser1.Invoke(new MethodInvoker(delegate { webBrowser1.Navigate(new Uri(url_template)); }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void selectMailBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }
        protected void selectMailBackground_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // set true
        bool expand = false;
        bool expand_first = true;
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = nodes - 1; i >= 0; i--)
            {
                if (expand_first)
                {
                    treeView1.Nodes[0].Toggle();
                    expand_first = false;
                }
                treeView1.Nodes[i].Toggle();
            }
            if (!expand)
            {
                button3.Text = "Thu gọn";
                expand = true;
            }
            else {
                expand = false;
                button3.Text = "Mở rộng";
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            url_template = e.Node.Name;
            if (url_template != null && url_template != "")
            {
                panel1.Visible = true;
                if (!selectMailBackground.IsBusy)
                {
                    selectMailBackground.RunWorkerAsync();
                }
            }
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Xin chờ";
            doc = mainForm.HTMLEditors.Document.DomDocument as IHTMLDocument2;
            doc.body.innerHTML = downloadString;
            doc.designMode = "On";
            this.Close();
            
        }


    }
}
