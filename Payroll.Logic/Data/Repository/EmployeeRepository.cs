
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Payroll.Logic;

namespace Payroll.Data
{
     public class EmployeeRepository :  IEmployeeRepository 
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees.Find(employeeId);
        }
        public int AddEmployee(Employee employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                _context.Employees.Add(employeeEntity);
                _context.SaveChanges();
                result = employeeEntity.employeeId;
            }
            return result;

        }
        public int UpdateEmployee(Employee employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                _context.Entry(employeeEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = employeeEntity.employeeId;
            }
            return result;
        }
        public void DeleteEmployee(int employeeId)
        {
            Employee employeeEntity = _context.Employees.Find(employeeId);
            _context.Employees.Remove(employeeEntity);
            _context.SaveChanges();

        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
