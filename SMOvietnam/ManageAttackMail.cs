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
using Finisar.SQLite;
using System.Threading;
using System.Net.Sockets;

namespace SMOvietnam
{
    public partial class ManageAttackMail : Form
    {
        main mainForm;
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;
        public ManageAttackMail(main mainForm)
        {
            this.mainForm = mainForm;
            this.Owner = mainForm;
            InitializeComponent();

            sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");

            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM file_attack WHERE id_mail_add = " + Library._mail_add + " ORDER BY id DESC";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            int _i_mail = 0;
            ListfileAttack.Rows.Clear();
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                ListfileAttack.Rows.Add();
                ListfileAttack.Rows[_i_mail].Cells[0].Value = (_i_mail + 1);
                ListfileAttack.Rows[_i_mail].Cells[1].Value = sqlite_datareader["path_file"].ToString();
                ListfileAttack.Rows[_i_mail].Cells[3].Value = sqlite_datareader["id"].ToString();
                _i_mail++;
            }
            sqlite_conn.Close();

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

        private int _getNextIDMailAdd(string table)
        {
            SQLiteConnection __sqlite_conn;
            SQLiteCommand __sqlite_cmd;
            
            __sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");
            __sqlite_conn.Open();
            __sqlite_cmd = __sqlite_conn.CreateCommand();
            __sqlite_cmd.CommandText = "SELECT max(id) AS idss FROM " + table;
            sqlite_datareader = __sqlite_cmd.ExecuteReader();

            int _id_next = 0;
            while (sqlite_datareader.Read())
            {
                _id_next = Convert.ToInt32(sqlite_datareader["idss"]) + 1;
            }
            __sqlite_conn.Close();
            return _id_next;

        }

        private void btnSelectFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFd = new OpenFileDialog();

            DialogResult dr = OpenFd.ShowDialog();
            SQLiteDataReader sqlite_datareader;

            if (Path.GetExtension(OpenFd.FileName) != ".exe")
            {
                string __txtSQLQuery = "INSERT INTO file_attack (path_file, id_mail_add, id) VALUES ('" + OpenFd.FileName + "', " + Library._mail_add + ", " + _getNextIDMailAdd("file_attack") + ");";
                ExecuteQuery(__txtSQLQuery);

                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = "SELECT * FROM file_attack WHERE id_mail_add = " + Library._mail_add + " ORDER BY id DESC";
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                int _i_mail = 0;
                ListfileAttack.Rows.Clear();
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    ListfileAttack.Rows.Add();
                    ListfileAttack.Rows[_i_mail].Cells[0].Value = (_i_mail + 1);
                    ListfileAttack.Rows[_i_mail].Cells[1].Value = sqlite_datareader["path_file"].ToString();
                    ListfileAttack.Rows[_i_mail].Cells[3].Value = sqlite_datareader["id"].ToString();
                    _i_mail++;
                }
                sqlite_conn.Close();
            }else{
                MessageBox.Show("Bạn không được phép đính kèm 1 file .exe");
            }
        }

        private void ListfileAttack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SQLiteDataReader sqlite_datareader;
            int _id = 0;
            _id = Convert.ToInt32(ListfileAttack.Rows[e.RowIndex].Cells[3].Value);
            if (e.ColumnIndex == 2)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng đã chọn", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtSQLQuery = "DELETE FROM file_attack WHERE id = '" + _id + "'";
                    ExecuteQuery(txtSQLQuery);
                        
                    sqlite_conn = new SQLiteConnection("Data Source=database3.db;Version=3;New=False;Compress=True;");

                    sqlite_conn.Open();
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = "SELECT * FROM file_attack WHERE id_mail_add = " + Library._mail_add + " ORDER BY id DESC";
                    sqlite_datareader = sqlite_cmd.ExecuteReader();

                    int _i_mail = 0;
                    ListfileAttack.Rows.Clear();
                    while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                    {
                        ListfileAttack.Rows.Add();
                        ListfileAttack.Rows[_i_mail].Cells[0].Value = (_i_mail + 1);
                        ListfileAttack.Rows[_i_mail].Cells[1].Value = sqlite_datareader["path_file"].ToString();
                        ListfileAttack.Rows[_i_mail].Cells[3].Value = sqlite_datareader["id"].ToString();
                        _i_mail++;
                    }
                    sqlite_conn.Close();
                }
            }
        }
    }
}
