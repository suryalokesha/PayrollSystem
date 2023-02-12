using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Logic
{
    public interface IPayFrequency
    {
        decimal CalculateSalary(DateTime weekStart, int hours, int minutes, decimal sickDays, decimal baseSalary);
    }
}
