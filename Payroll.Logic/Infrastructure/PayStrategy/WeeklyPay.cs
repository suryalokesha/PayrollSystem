using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Logic
{
    public class WeeklyPay : IPayFrequency
    {
        public decimal CalculateSalary(DateTime weekStart, int hours, int minutes, decimal sickDays, decimal baseSalary)
        {
            return sickDays <= 0 ? baseSalary : (sickDays > 0 && sickDays < 5) ? (baseSalary - (baseSalary / 5) * sickDays) : 0;
        }
    }
}
