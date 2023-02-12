namespace Payroll.Logic
{
    public interface IDeducionStrategy
    {
        decimal CalculateDeduction(decimal amount);
    }
}