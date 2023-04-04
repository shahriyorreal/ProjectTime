using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ExcelToDB.Repositories;

namespace ExcelToDB.UserControls
{
    public partial class UC_Staffs : UserControl
    {
        MySqlConnection Mysqlcon = new MySqlConnection();
        DataRepository data = new DataRepository();          
        public UC_Staffs()
        {
            InitializeComponent();
            FillDGV();
            comboJobTitle.Items.Add("Staff");
            comboJobTitle.Items.Add("Admin");
            comboJobTitle.SelectedIndex = 0;
        }
        DateTime time = new DateTime();
        private void buttonAddStaff_Click(object sender, EventArgs e)
        {
            int id_profession;
            if (comboJobTitle.SelectedItem == "Staff")
            {
                id_profession = 2;
            }
            else
            {
                id_profession = 1;
            }
            data.Open();
            string sql = "INSERT INTO staffs(Name, id_profession, username, password, salary, hour) VALUES('" + txtNameStaff.Text + "','" + id_profession + "', '" + txtUsernameStaff.Text + "', '" + txtPasswordStaff.Text + "', '" + txtSalary.Text + "', '" + time.AddHours(Convert.ToDouble(txtTime.Text))+ "')";
            data.InsertUpdate(sql);
            FillDGV();
            data.Close();
            txtNameStaff.Clear();
            txtPasswordStaff.Clear();
            txtUsernameStaff.Clear();
        }
        private void FillDGV()
        {
            data.Open();
            string query = "SELECT s.id,s.`Name`,s.`username`,s.`password` ,p.`name` AS p_name FROM staffs AS s,profession AS p WHERE isActive = 1 AND s.`id_profession` = p.`id`";
            var get = data.Get(query);
            DataTable dt = new DataTable();
            DataGridView1.DataSource = get;
            DataGridView1.Columns["id"].Visible = false;
            data.Close();
        }
        int id = 0;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id_profession;
            if (comboJobTitle.SelectedItem == "Staff")
            {
                id_profession = 2;
            }
            else
            {
                id_profession = 1;
            }
            data.Open();
            string update = "UPDATE staffs SET Name = '" + txtNameStaff.Text + "', id_profession = '" + id_profession + "', username = '" + txtUsernameStaff.Text + "', password = '" + txtPasswordStaff.Text + "' WHERE staffs.id ="+id+" ";
            data.InsertUpdate(update);
            data.Close();
            txtNameStaff.Clear();
            txtPasswordStaff.Clear();
            txtUsernameStaff.Clear();
            FillDGV();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            data.Open();
            string qwery = "UPDATE staffs SET isActive = false WHERE staffs.id = '"+id+"'";
            data.InsertUpdate(qwery);
            data.Close();
            FillDGV();
        }
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = int.Parse(DataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());
            txtNameStaff.Text = DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtUsernameStaff.Text = DataGridView1.SelectedRows[0].Cells["username"].Value.ToString();
            txtPasswordStaff.Text = DataGridView1.SelectedRows[0].Cells["password"].Value.ToString();
            comboJobTitle.Text = DataGridView1.Rows[e.RowIndex].Cells["p_name"].Value.ToString();
        }
    }
}
