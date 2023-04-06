using ExcelToDB.Classes;
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
        int salary = 0;
        DataRepository conn = new DataRepository();

        public User User = new User();
        System.Timers.Timer t;
        int h, m, s;
        public FormStaffs()
        {
            InitializeComponent();
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
        }
        public int MyProperty { get; set; }
        public void Salary()
        {
            conn.Open();
            String qwery = "SELECT salary FROM staffs WHERE staffs.id = '" + User.id + "'";
            var get = conn.Get(qwery);
            DataTable dt = new DataTable();
            salary = int.Parse(get.Rows[0]["salary"].ToString());
            conn.Close();
        }

        private void GetData()
        {
            conn.Open();
            string query = "SELECT user_works_time.`time`, user_works_time.`date` FROM user_works_time WHERE user_works_time.`id_user` = '"+User.id+"' group by day(date)";
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
            Salary();
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            t.Stop();
        }

        private void btn_ViewResult_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            conn.Open();
            string qwery = "SELECT SEC_TO_TIME( SUM(TIME_TO_SEC(TIME)))AS TIME, user_works_time.`date` FROM user_works_time WHERE id_user = '" + User.id + "'  AND DATE BETWEEN DATE_SUB(NOW(), INTERVAL 1 MONTH) AND NOW()";
            var get = conn.Get(qwery);
            DataTable dt = new DataTable();
            var time_salary = get.Rows[0]["TIME"].ToString();
            DataGridView1.DataSource = get;
            conn.Close();

            //int nnn = int.Parse(time_salary) * salary;
            label2.Text = time_salary.ToString();

         }

    }
}
