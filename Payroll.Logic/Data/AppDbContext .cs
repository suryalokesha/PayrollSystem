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

     /*   protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("server=Server=SURYA;Database=PayrollDB;Trusted_Connection=True;TrustServerCertificate=true");
            //optionBuilder.UseSqlServer("default");
        } 
        */

    }
}
