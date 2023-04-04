using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToDB.Classes
{
    public class User
    {
        public int id { get; set; }
        public Roles Roless { get; set; }

        public string Name { get; set; }

        public bool Exist { get; set; }
    }
}
