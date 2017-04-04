using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Net;
using System.IO;
using System.IO.Ports;

namespace SMOvietnam
{
    public partial class addImg : Form
    {
        main mainForm;
        public static IHTMLDocument2 doc;
        public addImg(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
        }

        private void btnAddImageContentMail_Click(object sender, EventArgs e)
        {
            string linkimg = txt_link_img.Text;
            doc = mainForm.HTMLEditors.Document.DomDocument as IHTMLDocument2;
            doc.execCommand("insertImage", false, linkimg);
            this.Close();
        }

        private void lbl_link_upload_img_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://imgur.com/");
        }

        private void btn_close_addimg_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_selectimg_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFd = new OpenFileDialog();

            DialogResult dr = OpenFd.ShowDialog();

            locateimg.Text = OpenFd.FileName;

        }

        private void btn_upload_img_Click(object sender, EventArgs e)
        {
            System.Net.WebClient Client = new System.Net.WebClient();

            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile(Library.apiUrl + "/uploadimg", "POST",
                                              @locateimg.Text);

            string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
            txt_link_img.Text = s;
            MessageBox.Show("Upload success");
        }


        

       



    }
}
