using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SMOvietnam
{
    public partial class setupSendSms : Form
    {
        main mainForm;
        public setupSendSms(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();

            rendergio();
            renderphut();
            renderngay();
            renderthang();
            rendernam();


            RegistryKey key_regedit12 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\beecom");
            if (key_regedit12.GetValue("gio") != null)
            {
                cbogio.SelectedIndex = cbogio.Items.IndexOf(key_regedit12.GetValue("gio"));
                cbophut.SelectedIndex = cbophut.Items.IndexOf(key_regedit12.GetValue("phut"));
                cbongay.SelectedIndex = cbongay.Items.IndexOf(key_regedit12.GetValue("ngay"));
                cbothang.SelectedIndex = cbothang.Items.IndexOf(key_regedit12.GetValue("thang"));
                cbonam.SelectedIndex = cbonam.Items.IndexOf(key_regedit12.GetValue("nam"));
            }

        }

        private void rendernam()
        {
            string gio = "";
            for (int ix = 2015; ix < 2020; ix++)
            {
                if (ix < 10)
                {
                    gio = "0" + ix;
                }
                else
                {
                    gio = ix.ToString();
                }
                cbonam.Items.Add(gio);
            }
        }

        private void renderthang()
        {
            string gio = "";
            for (int ix = 1; ix < 13; ix++)
            {
                if (ix < 10)
                {
                    gio = "0" + ix;
                }
                else
                {
                    gio = ix.ToString();
                }
                cbothang.Items.Add(gio);
            }
        }

        private void renderngay()
        {
            string gio = "";
            for (int ix = 1; ix < 32; ix++)
            {
                if (ix < 10)
                {
                    gio = "0" + ix;
                }
                else
                {
                    gio = ix.ToString();
                }
                cbongay.Items.Add(gio);
            }
        }

        private void rendergio()
        {
            string gio = "";
            for (int ix = 0; ix < 24; ix++)
            {
                if (ix < 10)
                {
                    gio = "0" + ix;
                }
                else
                {
                    gio = ix.ToString();
                }
                cbogio.Items.Add(gio);
            }
        }

        private void renderphut()
        {
            string gio = "";
            for (int ix = 0; ix < 60; ix++)
            {
                if (ix < 10)
                {
                    gio = "0" + ix;
                }
                else
                {
                    gio = ix.ToString();
                }
                cbophut.Items.Add(gio);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
            //storing the values  
            keyt123.SetValue("gio", cbogio.SelectedItem);
            keyt123.SetValue("phut", cbophut.SelectedItem);
            keyt123.SetValue("ngay", cbongay.SelectedItem);
            keyt123.SetValue("thang", cbothang.SelectedItem);
            keyt123.SetValue("nam", cbonam.SelectedItem);
            keyt123.SetValue("isRunBookActionSendSMS", 0);
            mainForm.lblbook.Text = "Thêm đặt lịch thành công";
            MessageBox.Show("Đặt lịch thành công");
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistryKey keyt123 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\beecom");
            //storing the values  
            keyt123.SetValue("isRunBookActionSendSMS", 1);
            mainForm.lblbook.Text = "Hủy đặt lịch thành công";
            MessageBox.Show("Hủy lịch thành công");
            this.Hide();
        }

    }
}
