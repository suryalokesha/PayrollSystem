
using Payroll.Logic;

namespace Payroll.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Employee> EmployeeRepository { get; }
        void Save();
    }
}
