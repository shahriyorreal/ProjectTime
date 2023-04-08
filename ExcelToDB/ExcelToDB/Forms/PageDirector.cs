using ExcelToDB.UserControls;
using ExcelToDB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelToDB.Repositories;

namespace ExcelToDB
{
    public partial class PageDirector : Form
    {
        DataRepository conn = new DataRepository();
        Calculation calc = new Calculation();
        public User User = new User();
        public PageDirector()
        {
            InitializeComponent();
        }
        private void AddUserControl(UserControl usercontrol)
        {
            usercontrol.Dock = DockStyle.Fill;
            PanelContainer.Controls.Clear();
            PanelContainer.Controls.Add(usercontrol);
            usercontrol.BringToFront();
        }
        private void GetData()
        {
            conn.Open();
            string qwery = "SELECT SEC_TO_TIME( SUM(TIME_TO_SEC(TIME)))AS TIME, staffs.`salary`, staffs.`Name`, '' As S FROM user_works_time, staffs WHERE user_works_time.`id_user` = staffs.`id` AND DATE BETWEEN DATE_SUB(NOW(), INTERVAL 1 MONTH) AND NOW() GROUP BY staffs.`id`";
            var get = conn.Get(qwery);
            foreach (DataRow item in get.Rows)
            {
                var time = item["time"].ToString();
                var salary = double.Parse(item["salary"].ToString());
                var res = calc.Calc_Salary(time, salary);
                item["s"] =  res;
            }
            DataGridView1.DataSource = get;
            conn.Close();
        }

        private void btn_Staffs_Click(object sender, EventArgs e)
        {
            UC_Staffs us_staffs = new UC_Staffs();
            AddUserControl(us_staffs);
        }
        private void btn_product_Click(object sender, EventArgs e)
        {
            UC_Tovars uc_tovars = new UC_Tovars();
            AddUserControl(uc_tovars);
           
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

        private void btnHide_MouseHover_1(object sender, EventArgs e)
        {
            btnHide.FillColor = Color.Yellow;
        }

        private void btnHide_MouseLeave_1(object sender, EventArgs e)
        {
            btnHide.FillColor = Color.Transparent;
        }

        private void btnMax_MouseHover_1(object sender, EventArgs e)
        {
            btnMax.FillColor = Color.Blue;
        }
        private void btnMax_MouseLeave_1(object sender, EventArgs e)
        {
            btnMax.FillColor = Color.Transparent;
        }
        private void btnExit_MouseHover_1(object sender, EventArgs e)
        {
            btnExit.FillColor = Color.Red;
        }
        private void btnExit_MouseLeave_1(object sender, EventArgs e)
        {
            btnExit.FillColor = Color.Transparent;
        }
        private void PageDirector_Load(object sender, EventArgs e)
        {
            label2.Text = User.Name;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            GetData();
        } 
    }
}
