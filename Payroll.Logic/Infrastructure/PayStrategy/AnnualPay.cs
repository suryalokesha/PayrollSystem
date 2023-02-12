using Payroll.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Logic
{
    public class AnnualPay : IPayFrequency
    {
        public decimal CalculateSalary(DateTime weekStart, int hours, int minutes, decimal sickDays, decimal baseSalary)
        {
            decimal calculatedPay = 0m;
            // basic weekly pay rate p
            decimal weeklyPayRate = baseSalary / 52;
            calculatedPay = sickDays <= 0 ? weeklyPayRate : (sickDays > 0 && sickDays < 5) ? (weeklyPayRate - (weeklyPayRate / 5) * sickDays) : 0;
            return calculatedPay;
        }
    }
}
