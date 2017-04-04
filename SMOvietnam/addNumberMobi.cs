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
    public partial class addNumberMobi : Form
    {
        main mainForm;
        public addNumberMobi(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();
        }

        private void btn_popup_add_mobile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_add_mobile.Text != string.Empty)
                {
                    int fv = mainForm.dgvMobile.Rows.Count;
                    string numberMobile = "";
                    string content = txt_add_mobile.Text;
                    string[] words = content.Split(',');
                    
                    foreach (string word in words)
                    {
                        string _word = word.Substring(0, 1);
                        if (_word == "0")
                        {
                            numberMobile = word.Remove(0, 1).ToString();
                        }
                        else {
                            numberMobile = word.ToString();
                        }
                        
                        mainForm.dgvMobile.Rows.Add();
                        mainForm.dgvMobile.Rows[fv].Cells[0].Value = true;
                        mainForm.dgvMobile.Rows[fv].Cells[1].Value = numberMobile;
                        mainForm.dgvMobile.Rows[fv].Cells[3].Value = "Chưa gửi";
                        fv++;
                    }
                    txt_add_mobile.Text = "";
                    MessageBox.Show("Thêm thành công");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btn_close_addmobile_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_close_add_numbermobile_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
