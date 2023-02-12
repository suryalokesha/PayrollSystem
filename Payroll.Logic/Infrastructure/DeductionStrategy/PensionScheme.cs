
namespace Payroll.Logic
{
    public class PensionScheme: IDeducionStrategy
    {
        // weeklypay+sickpay
        public decimal CalculateDeduction(decimal amount)
        {
            return amount - (amount) / (50);
        }
    }
}
