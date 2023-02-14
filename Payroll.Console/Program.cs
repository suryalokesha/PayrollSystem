using Payroll.Logic;

namespace Payroll.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Employee salaryWithCOSP = Employee.CreateEmployee("Eric", "Wimp", new DateTime(2014, 01, 01), PayFrequency.Annual, 50000m, SickPay.COSP);
            salaryWithCOSP.AddDeduction(Deduction.Pension);
            salaryWithCOSP.AddDeduction(Deduction.BikeScheme);
            IEmployee salaryWithLateCOSP = Employee.CreateEmployee("Selina", "Kyle", new DateTime(2019, 01, 01), PayFrequency.Annual, 45000m, SickPay.COSP);
            IEmployee salaryWithoutCOSP = Employee.CreateEmployee("Peter", "Parker", new DateTime(2018, 01, 01), PayFrequency.Annual, 32500m, SickPay.SSP);
            IEmployee weeklyWithoutCOSP = Employee.CreateEmployee("Clark", "Kent", new DateTime(2018, 06, 01), PayFrequency.Weekly, 480m, SickPay.SSP);
            IEmployee hourlyWithCOSP = Employee.CreateEmployee("Bruce", "Wayne", new DateTime(2018, 01, 01), PayFrequency.Hourly, 15m, SickPay.COSP);
            IEmployee hourlyWithoutCOSP = Employee.CreateEmployee("Wade", "Wilson", new DateTime(2017, 01, 01), PayFrequency.Hourly, 20m, SickPay.SSP);

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
            Console.WriteLine("**********Calculate Labour Cost **********");
            Console.WriteLine("{0,-15}{1}", "Employee  ", " Calculated Pay");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("{0,-15}{1}", salaryWithCOSP.GetFullName(), salaryWithCOSP.CalculateLabourCost(weekStart, 40, 0, 0));
            Console.WriteLine("{0,-15}{1}", salaryWithLateCOSP.GetFullName(), salaryWithLateCOSP.CalculateLabourCost(weekStart, 40, 0, 2));
            Console.WriteLine("{0,-15}{1}", salaryWithoutCOSP.GetFullName(), salaryWithoutCOSP.CalculateLabourCost(weekStart, 24, 0, 2));
            Console.WriteLine("{0,-15}{1}", weeklyWithoutCOSP.GetFullName(), weeklyWithoutCOSP.CalculateLabourCost(weekStart, 40, 0, 0));
            Console.WriteLine("{0,-15}{1}", hourlyWithCOSP.GetFullName(), hourlyWithCOSP.CalculateLabourCost(weekStart, 24, 0, 2));
            Console.WriteLine("{0,-15}{1}", hourlyWithoutCOSP.GetFullName(), hourlyWithoutCOSP.CalculateLabourCost(weekStart, 40, 0, 0));

            // Get all  employees 
            Console.WriteLine("\n**********Get all employees **********");

           List <Employee> employeesList = Employee.GetEmployeesData();
            if (employeesList != null)
            {
                Console.WriteLine("\n-------------------------------------------------------------");

                Console.WriteLine("{0,-15}{1,-10}{2, 15}","Employee_Name","Hire_Date","Base_Salary");

                Console.WriteLine("--------------------------------------------------------------");
                foreach (Employee employee in employeesList)
                {
                    Console.WriteLine("{0,-15}{1,-10}{2, 15}", employee.GetFullName(), employee.hireDate.ToString("dd/MM/yyyy"), employee.baseSalary);
                }
            }
            Console.WriteLine("\n-------------------------------------------------------------");

            // Update an Employee
            Employee employeeToUpdate = (Employee)salaryWithLateCOSP;
            Console.WriteLine("************Employee before update************");
            Console.WriteLine("Employee Name: {0}   Hire_Date:{1}   Base Salary:{2}   Pay_Frequency:{3},  SickPayType:{4}",
                employeeToUpdate.firstName + employeeToUpdate.lastName, employeeToUpdate.hireDate, employeeToUpdate.baseSalary, employeeToUpdate.payFrequency, employeeToUpdate.sickPay);

            Employee employeeUpdated = Employee.UpdateEmployee(employeeToUpdate.employeeId,employeeToUpdate.firstName,employeeToUpdate.lastName,new DateTime(2019, 01, 01), PayFrequency.Annual, 50500m, SickPay.SSP);
            Console.WriteLine("\n************Employee After update**************");
            Console.WriteLine("Employee Name: {0}   Hire_Date:{1}   Base Salary:{2}   Pay_Frequency:{3},  SickPayType:{4}",
                employeeToUpdate.firstName+" "+ employeeUpdated.lastName, employeeUpdated.hireDate, employeeUpdated.baseSalary, employeeUpdated.payFrequency, employeeUpdated.sickPay);
            Console.WriteLine("\n-------------------------------------------------------------");
           
            // Delete an Employee
            Console.WriteLine("************Delete Employee************");
            Employee employeeTodelete = (Employee)hourlyWithoutCOSP;
            string name= employeeTodelete.firstName+" "+employeeTodelete.lastName;
            Console.WriteLine("Deleted Employee:{0} ", name);
            Employee.DeleteEmployee(employeeTodelete.employeeId);

            // Get all  employees 
            Console.WriteLine("\n**********Get all employees after delete**********");

            List<Employee> employeesList1 = Employee.GetEmployeesData();
            if (employeesList != null)
            {
                Console.WriteLine("\n-------------------------------------------------------------");

                Console.WriteLine("{0,-15}{1,-10}{2,15}", "Employee_Name", "Hire_Date", "Base_Salary");

                Console.WriteLine("--------------------------------------------------------------");
                foreach (Employee employee in employeesList1)
                {
                    Console.WriteLine("{0,-15}{1,-10}{2,15}", employee.GetFullName(), employee.hireDate.ToString("dd/MM/yyyy"), employee.baseSalary);
                }
            }
            Console.WriteLine("\n-------------------------------------------------------------");


            Console.ReadLine();
        }
    }
}


