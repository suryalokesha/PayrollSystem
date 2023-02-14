
using NuGet.Frameworks;

namespace PayrollTestProject
{
    public class Tests
    {
        private Employee _employee;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        // initialize the instances required for test cases 
        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _employee = new Employee
            {
                firstName = "Sharma",
                lastName = "Rakesh",
                hireDate = new DateTime(2020, 01, 01),
                payFrequency = PayFrequency.Weekly,
                baseSalary = 45000,
                sickPay = SickPay.COSP
            };
        }

        //    TestCase for the to check Valid Input
        [Test]
        public void CreateEmployee_ValidInput_ReturnsEmployee()
        {
            // Arrange
            var firstName = "Sharma";
            var lastName = "Rakesh";
            var hireDate = new DateTime(2020, 01, 01);
            var payFrequency = PayFrequency.Weekly;
            var baseSalary = 1000m;
            var sickPay = SickPay.COSP;

            // Act
            var result = Employee.CreateEmployee(firstName, lastName, hireDate, payFrequency, baseSalary, sickPay);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(firstName, result.firstName);
            Assert.AreEqual(lastName, result.lastName);
            Assert.AreEqual(hireDate, result.hireDate);
            Assert.AreEqual(payFrequency, result.payFrequency);
            Assert.AreEqual(baseSalary, result.baseSalary);
            Assert.AreEqual(sickPay, result.sickPay);
        }

        //    TestCase for the Insert method of Employee repository
        [Test]
        public void CreateEmployee_ShouldInsertEmployeeIntoDb()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.EmployeeRepository.Insert(_employee));
            _unitOfWorkMock.Setup(uow => uow.Save());

            // Act
            var result = Employee.CreateEmployee("Rakesh", "Sharma", new DateTime(2022, 1, 1), PayFrequency.Weekly, 50000, SickPay.COSP);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.employeeId);
        }


        //    TestCase for the Update method of Employee repository
        [Test]
        public void UpdateEmployee_ShouldUpdateEmployeeIntoDb()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.EmployeeRepository.Insert(_employee));
            _unitOfWorkMock.Setup(uow => uow.Save());

            // Act
            var result = Employee.CreateEmployee("Rakesh", "Sharma", new DateTime(2022, 1, 1), PayFrequency.Weekly, 50000m, SickPay.SSP);

            // Assert
            Assert.AreNotEqual(result.hireDate, _employee.hireDate);
            Assert.AreNotEqual(result.baseSalary, _employee.baseSalary);
            Assert.AreNotEqual(result.sickPay, _employee.sickPay);
        }

        //        TestCase for the AddDeduction method:
        public void AddDeduction_ValidInput_AddsDeductionToList()
        {
            // Arrange
            var deductionType = Deduction.Pension;

            // Act
            _employee.AddDeduction(deductionType);

            // Assert
            Assert.AreEqual(1, _employee.deductions.Count);
            Assert.AreEqual(deductionType, _employee.deductions[0]);
        }

        //TestCase for the GetFullName method
        [Test]
        public void GetFullName_ShouldReturnFirstAndLastName()
        {

            // Arrange
           var deductions = new List<Deduction>();
            deductions.Add(Deduction.Pension);
           _employee.deductions = deductions;
          
            // Act
            var result = _employee.GetFullName();
          

            // Assert
            Assert.AreEqual("Sharma Rakesh", result);
        }

    }
}