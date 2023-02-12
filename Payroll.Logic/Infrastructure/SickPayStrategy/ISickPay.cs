
namespace Payroll.Logic
{
    public interface ISickPay
    {
        public Decimal CalculateSickPay(DateTime hireDate, int sickDays, decimal totalWeeklyPay);
    }
}
