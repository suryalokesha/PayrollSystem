using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.Logic;

namespace Payroll.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        public IGenericRepository<Employee> employeeRepository;

        public UnitOfWork(AppDbContext context){ _context = context; }
        public IGenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new GenericRepository<Employee>(_context);
                }
                return employeeRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
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

    }
}
