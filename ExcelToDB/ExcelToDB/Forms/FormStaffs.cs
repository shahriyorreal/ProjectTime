﻿using ExcelToDB.Classes;
using ExcelToDB.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelToDB
{
    public partial class FormStaffs : Form
    {
        DataRepository conn = new DataRepository();

        public User User = new User();
        System.Timers.Timer t;
        int h, m, s;
        public FormStaffs()
        {
            //User = new Classes.User();
            InitializeComponent();
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;         
        }

        private void GetData()
        {
            conn.Open();
            string query = "SELECT user_works_time.`time`, user_works_time.`date` FROM user_works_time WHERE user_works_time.`id_user` = '"+User.id+"'";
            var get = conn.Get(query);
            DataTable dt = new DataTable();
            DataGridView1.DataSource = get;
            conn.Close();
        }

        private void ClearTime()
        {
            h = 0;
            m = 0;
            s = 0;
        }

        private void OnTimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
                {
                    s += 1;
                    if (s ==60)
	                {
                        s = 0;
                        m = 1;
                    }

                    if (m == 60)
                    {
                        m = 0;
                        h = 1;
                    }
                    txtResult.Text = String.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
                }
                ));
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            t.Start();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            t.Stop();
            conn.Open();
            conn.InsertUpdate("INSERT INTO user_works_time SET id_user = '"+User.id+"', time = '"+txtResult.Text+"', date = '"+DateTime.Now.ToString("yyyy-MM-dd")+"'");
            conn.Close();
            ClearTime();
        }

        private void FormStaffs_Load(object sender, EventArgs e)
        {
            NameStaffsLabel.Text = User.Name; 
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {

            WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            t.Stop();
        }

        private void btn_ViewResult_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
