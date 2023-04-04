using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ExcelToDB.Repositories
{
   public class DataRepository
    {
        MySqlConnection conn = new MySqlConnection();
        private string constr = "server =localhost; username =root; password =;database=humans;";

        public void InsertUpdate(string query)
        {
            MySqlCommand command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
        }
        public DataTable Get(string query)
        {
            MySqlDataAdapter mydata = new MySqlDataAdapter(query, conn);
            DataTable dtable = new DataTable();
            mydata.Fill(dtable);
            return dtable;
        }

        public bool Open()
        {
            try
            {
                conn.ConnectionString = constr;
                conn.Open();
                return true;
            }
            catch (Exception)
            {

                return false;
            } 
        }
        public void Close()
        {
            conn.Close();
        }
        
    }
}
