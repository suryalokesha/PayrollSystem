
namespace Payroll.Logic
{
    public class CompanySickPay : ISickPay
    {
        public Decimal CalculateSickPay(DateTime hireDate, int sickDays, decimal totalWeeklyPay)
        {
            decimal sickPay = 0m;
            if (DateTime.Now.Year - hireDate.Year >= 5)
            {
                sickPay = sickDays < 5 ? ((totalWeeklyPay / 5) * sickDays) : (sickDays >= 5) ? totalWeeklyPay : totalWeeklyPay;
            }
            return sickPay;
        }
    }
}
