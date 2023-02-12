using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Logic;

namespace Payroll.Data
{
    public interface IEmployeeRepository : IDisposable
    {
        Employee GetEmployeeById(int employeeId);
        IEnumerable<Employee> GetAllEmployees();
        int AddEmployee(Employee employeeEntity);
        int UpdateEmployee(Employee employeeEntity);
        void DeleteEmployee(int employeeId);
    }
}
