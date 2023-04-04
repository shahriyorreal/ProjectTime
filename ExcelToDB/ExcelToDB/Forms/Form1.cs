using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ExcelToDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.Show();
            InitializeComponent(); 
        }
        DataTableCollection tablecollection;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = " Excel WorkBook |*.xlsx" })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            tablecollection = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in tablecollection)
                                comboBox1.Items.Add(table.TableName);
                        }
                    }
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tablecollection[comboBox1.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection sqlcon = new MySqlConnection();
            sqlcon.ConnectionString = ("server =localhost; username =root; password =;database=humans;");
            MySqlCommand com = new MySqlCommand();
            sqlcon.Open();
            foreach (DataGridViewRow r in dataGridView1.Rows)
			{
                string StrQuery = @"INSERT INTO staffs SET Name ='" + r.Cells["Name"].Value + "',Profession ='"+r.Cells["Profession"].Value +"' ";
                com = new MySqlCommand(StrQuery, sqlcon);
                com.ExecuteNonQuery();
			}
            sqlcon.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = Clipboard.GetText();

            string[] lines = s.Replace("\n", "").Split('\r');
            
            dataGridView1.Rows.Add(lines.Length - 1);
            string[] fields;
            int row = 0;
            int col = 0;
            //dataGridView1[col, row].Value = f;
            foreach (string item in lines)
            {
                fields = item.Split('\t');
                foreach (string f in fields)
                {
                    Console.WriteLine(f);
                    col++;
                }
                row++;
                col = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();
                
            string[] lines = s.Replace("\n", "").Split('\r');
            dataGridView1.Columns.Add("Name", "Column Name in Text");
            dataGridView1.Rows.Add(lines.Length - 0);
            string[] fields;
            int row = 0;
            int col = 0;

            foreach (string item in lines)
            {
                dataGridView1.Columns.Add("Name", "Column Name in Text");
                fields = item.Split('\t');
                foreach (string f in fields)
                {
                    if (f != null)
                    {
                        dataGridView1[col, row].Value = f;
                        col++;
                    }

                }
                row++;
                col = 0;
                
            }
        }
    }
}
//this.deletedCols.Add(cs.MappingName);	