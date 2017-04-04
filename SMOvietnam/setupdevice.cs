using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMOvietnam
{
    public partial class setupdevice : Form
    {
         main mainForm;
         public setupdevice(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
            lbl_networl.Text = mainForm.Comname;
            cbnetwork.SelectedIndex = cbnetwork.Items.IndexOf(mainForm.networkName);
            addMaxDevice(mainForm.maxdevice);
            cbamount.SelectedIndex = cbamount.Items.IndexOf(mainForm.maxdevice);
            addToDelay(mainForm.delaydevice);
            cbdeelay.SelectedIndex = cbdeelay.Items.IndexOf(mainForm.delaydevice);

            // delay { get; private set; }
            //public string maxdevice { get; private set; }
             
        }

         public void addToDelay(int ds)
         {
             int big = ds + 100;

             for (int min = (ds - 85) < 0 ? 0 : (ds - 50); min < ds; min--)
             {
                 cbdeelay.Items.Add(min);
                 min = min + 4;
             }

             for (int dsc = ds; dsc < big; dsc++)
             {
                 cbdeelay.Items.Add(dsc);
                 dsc = dsc + 4;
             }
         }

         public void addMaxDevice(int dss)
         {
             int bigs = dss + 1000;

             for (int dscs = dss; dscs < bigs; dscs++)
             {
                 cbamount.Items.Add(dscs);
                 dscs = dscs + 49;
             }
         }

        private void btn_close_setupdevice_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_setup_Click(object sender, EventArgs e)
        {
            try
            {

                int _row = Convert.ToInt32(mainForm.deviceRowCurrent);
                if (cbdeelay.SelectedIndex > 0)
                { 
                    mainForm.listCOM.Rows[_row].Cells[3].Value = cbdeelay.SelectedItem;
                }
                if (cbamount.SelectedIndex > 0)
                {
                    mainForm.listCOM.Rows[_row].Cells[4].Value = cbamount.SelectedItem;
                }
                mainForm.listCOM.Rows[_row].Cells[2].Value = cbnetwork.SelectedItem;
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
