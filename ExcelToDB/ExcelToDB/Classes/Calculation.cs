using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToDB.Classes
{
    class Calculation
    {
        public double Calc_Salary(string FullHour, double Salary)
        {
            var dateTime = Convert.ToDateTime(FullHour);
            double time = dateTime.Hour + dateTime.Minute / 60;
            var result = Salary * time;
            return result;
        }

        internal object Calc_Salary(System.Data.DataRow dataRow1, System.Data.DataRow dataRow2)
        {
            throw new NotImplementedException();
        }
    }
}
