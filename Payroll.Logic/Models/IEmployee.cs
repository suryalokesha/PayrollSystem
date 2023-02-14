using System;

namespace Payroll.Logic
{
    public interface IEmployee
    {
        string GetFullName();
        void AddDeduction(Deduction type);
        decimal CalculateLabourCost(DateTime weekStart, int hours, int minutes, int sickDays);
    
    }
}
