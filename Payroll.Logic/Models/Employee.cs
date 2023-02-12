using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Data;

namespace Payroll.Logic
{
    public class Employee : IEmployee
    {
        [Key]
        public int employeeId { get; set; }
        [Required]
        [MaxLength(20)]
        public String firstName { get; set; }
        [Required]
        [MaxLength(20)]
        public String lastName { get; set; }
        [Required]
        public DateTime hireDate { get; set; }
        [Required]
        public PayFrequency payFrequency { get; set; }
        [Required]
        public decimal baseSalary { get; set; }
        public SickPay sickPay { get; set; }

        [NotMapped]
        public List<Deduction> deductions { get; set; }

        [NotMapped]
        public IPayFrequency payStrategy;

        [NotMapped]
        public ISickPay sickPayStrategy;

        [NotMapped]
        public IDeducionStrategy deductionStrategy;

        [NotMapped]
        public static string connectionString { get; set; }

        private static IUnitOfWork objUnitOfWork;

        // Create Employee
        public static Employee CreateEmployee(string firstName, string lastName, DateTime hireDate, PayFrequency payFrequency, decimal baseSalary, SickPay sickPay)
        {
            Employee employee = new Employee()
            {
                firstName = firstName,
                lastName = lastName,
                hireDate = hireDate,
                payFrequency = payFrequency,
                baseSalary = baseSalary,
                sickPay = sickPay,
                deductions = new List<Deduction>()
            };

            PayrollConnectionSettings dbConnection = new PayrollConnectionSettings();

            // Save Employee to database
            objUnitOfWork = new UnitOfWork(dbConnection.GetDbContext());
            objUnitOfWork.EmployeeRepository.Insert(employee);
            objUnitOfWork.Save();
            return employee;
        }

        public decimal CalculateLabourCost(DateTime weekStart, int hours, int minutes, int sickDays)
        {
            // Assign appropriate Pay frequency for salary calculation
            switch (payFrequency)
            {
                case PayFrequency.Annual:
                    this.payStrategy = new AnnualPay();
                    break;
                case PayFrequency.Weekly:
                    this.payStrategy = new WeeklyPay();
                    break;
                case PayFrequency.Hourly:
                    this.payStrategy = new HourlyPay();
                    break;
            }

            decimal resultPay = payStrategy.CalculateSalary(weekStart, hours, minutes, sickDays, baseSalary);

            // National Insurance contribution
            decimal nationalInsurance = (resultPay - 162) * (0.138m);
            resultPay = resultPay - nationalInsurance;
            // Holiday Accrual
            resultPay += resultPay * 0.1207m;

            decimal sickPay = 0m;

            switch (this.sickPay)
            {
                // Company Sick Pay
                case SickPay.COSP:
                    sickPayStrategy = new CompanySickPay();
                    break;
                // Statutory Sick Pay 
                case SickPay.SSP:
                    sickPayStrategy = new StatutorySickPay();
                    break;
            }
            sickPay = sickPayStrategy.CalculateSickPay(this.hireDate, sickDays, resultPay);

            // Deductions 
            foreach (Deduction deduction in this.deductions)
            {
                switch (deduction)
                {
                    case Deduction.Pension:
                        deductionStrategy = new PensionScheme();
                        resultPay = deductionStrategy.CalculateDeduction(resultPay + sickPay);
                        break;
                    case Deduction.BikeScheme:
                        deductionStrategy = new BikeScheme();
                        resultPay = deductionStrategy.CalculateDeduction(resultPay);
                        break;
                }
            }

            resultPay = Math.Round(resultPay, 2);
            return resultPay;
        }

        public void AddDeduction(Deduction deductiontype)
        {
            deductions.Add(deductiontype);
        }

        public string GetFullName()
        {
            return this.firstName + " " + this.lastName;
        }

    }
}
