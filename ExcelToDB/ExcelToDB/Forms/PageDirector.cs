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

namespace ExcelToDB
{
    public partial class PageDirector : Form
    {
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
    }
}
