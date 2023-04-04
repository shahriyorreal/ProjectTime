using ExcelToDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToDB.Classes
{
   public class Auth
    {
       DataRepository conn = new DataRepository();
       public User IsExist( string username,string password) 
       {
           User user = new User();
           conn.Open();
           String querry = "SELECT staffs.id ,staffs.`Name` ,profession.`name` AS profession FROM staffs, Profession WHERE username = '"+username+"' AND PASSWORD = '"+password+"' AND id_profession = Profession.`id`";
           var dtable = conn.Get(querry);

           if (dtable.Rows.Count > 0)
           {
               
               user.Exist = true;
               user.id = int.Parse(dtable.Rows[0]["id"].ToString());
               var a = dtable.Rows[0]["profession"].ToString();
               if (a == "admin")
               {
                   user.Roless = Roles.admin;
                   user.Name = dtable.Rows[0]["Name"].ToString();
               }
               else
               {
                   user.Roless = Roles.staff;
                   user.Name = dtable.Rows[0]["Name"].ToString();
               }
               return user;
           }
           else
           {
               user.Exist = false;
               return user;
           }
           conn.Close();
       }
    }
}
