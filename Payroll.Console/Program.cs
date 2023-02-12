﻿using Payroll.Logic;

namespace Payroll.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Employee salaryWithCOSP = Employee.CreateEmployee("Eric", "Wimp", new DateTime(2014, 01, 01), PayFrequency.Annual, 25000m, SickPay.COSP);
            salaryWithCOSP.AddDeduction(Deduction.Pension);
            salaryWithCOSP.AddDeduction(Deduction.BikeScheme);
            IEmployee salaryWithLateCOSP = Employee.CreateEmployee("Selina", "Kyle", new DateTime(2019, 01, 01), PayFrequency.Annual, 25000m, SickPay.COSP);
            IEmployee salaryWithoutCOSP = Employee.CreateEmployee("Peter", "Parker", new DateTime(2018, 01, 01), PayFrequency.Annual, 22500m, SickPay.SSP);
            IEmployee weeklyWithoutCOSP = Employee.CreateEmployee("Clark", "Kent", new DateTime(2018, 06, 01), PayFrequency.Weekly, 480m, SickPay.SSP);
            IEmployee hourlyWithCOSP = Employee.CreateEmployee("Bruce", "Wayne", new DateTime(2018, 01, 01), PayFrequency.Hourly, 9m, SickPay.COSP);
            IEmployee hourlyWithoutCOSP = Employee.CreateEmployee("Wade", "Wilson", new DateTime(2017, 01, 01), PayFrequency.Hourly, 8m, SickPay.SSP);

            DateTime weekStart = DateTime.Now.Date;
            DayOfWeek dow = weekStart.DayOfWeek;

            switch (dow)
            {
                case DayOfWeek.Sunday: { weekStart = weekStart.AddDays(-6); break; }
                case DayOfWeek.Saturday: { weekStart = weekStart.AddDays(-5); break; }
                case DayOfWeek.Friday: { weekStart = weekStart.AddDays(-4); break; }
                case DayOfWeek.Thursday: { weekStart = weekStart.AddDays(-3); break; }
                case DayOfWeek.Wednesday: { weekStart = weekStart.AddDays(-2); break; }
                case DayOfWeek.Tuesday: { weekStart = weekStart.AddDays(-1); break; }
            }
            Console.WriteLine("{0}       {1}", "Employee  ", " salary");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("{0}:      {1}", salaryWithCOSP.GetFullName(), salaryWithCOSP.CalculateLabourCost(weekStart, 40, 0, 0));
            Console.WriteLine("{0}:      {1}", salaryWithLateCOSP.GetFullName(), salaryWithLateCOSP.CalculateLabourCost(weekStart, 40, 0, 2));
            Console.WriteLine("{0}:      {1}", salaryWithoutCOSP.GetFullName(), salaryWithoutCOSP.CalculateLabourCost(weekStart, 24, 0, 2));
            Console.WriteLine("{0}:      {1}", weeklyWithoutCOSP.GetFullName(), weeklyWithoutCOSP.CalculateLabourCost(weekStart, 40, 0, 0));
            Console.WriteLine("{0}:      {1}", hourlyWithCOSP.GetFullName(), hourlyWithCOSP.CalculateLabourCost(weekStart, 24, 0, 2));
            Console.WriteLine("{0}:      {1}", hourlyWithoutCOSP.GetFullName(), hourlyWithoutCOSP.CalculateLabourCost(weekStart, 40, 0, 0));
            Console.ReadLine();
        }
    }
}


