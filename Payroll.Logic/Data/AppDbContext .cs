using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payroll.Logic;

namespace Payroll.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }

    }
}
