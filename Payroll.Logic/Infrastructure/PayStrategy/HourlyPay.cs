using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Logic
{
    public class HourlyPay : IPayFrequency
    {
        /// <summary>
        /// Hourly pay
        /// </summary>
        /// <param name="weekStart"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="sickDays"></param>
        /// <returns>CalculateSalary</returns>
        public decimal CalculateSalary(DateTime weekStart, int hours, int minutes, decimal sickDays, decimal baseSalary)
        {
            return (baseSalary * hours) + ((baseSalary * minutes) / 60);
        }
    }
}
