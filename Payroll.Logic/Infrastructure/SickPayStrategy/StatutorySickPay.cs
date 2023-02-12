
namespace Payroll.Logic
{
    public class StatutorySickPay : ISickPay
    {
        public Decimal CalculateSickPay(DateTime hireDate, int sickDays, decimal totalWeeklyPay)
        {
            return sickDays > 0 ? (92.5m / 7) * sickDays : 0;
        }
    }
}
