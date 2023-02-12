using System;
namespace Payroll.Logic
{
    public class BikeScheme: IDeducionStrategy
    {
        //deduct flat rate 75
        public decimal CalculateDeduction(decimal amount)
        {
            return amount - 75;
        }
    }
}
