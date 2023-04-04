using ExcelToDB.Classes;
using ExcelToDB.Repositories;
using MySql.Data.MySqlClient;
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
    public partial class FormSignIn : Form
    {
        public FormSignIn()
        {
            InitializeComponent();
            label1.Parent = guna2PictureBox1;
            label1.BackColor = Color.Transparent;
            Close_button.Parent = guna2PictureBox1;
            Close_button.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PageDirector frm3 = new PageDirector();
            frm3.Show();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Auth auth = new Auth();//Hello World
                var user = auth.IsExist(UserTextBox.Text, PasswordTextBox.Text);
                if (user.Exist)
                {
                    if (user.Roless == Roles.admin)
                    {
                        var FN = new PageDirector();
                        FN.User = user;
                        FN.Show();
                    }
                    else
                    {
                        var FN = new FormStaffs();
                        FN.User = user;
                        FN.Show();  
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception) {}
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
