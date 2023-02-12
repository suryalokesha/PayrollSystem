
namespace PayrollTestProject
{
    public class Tests
    {
        private Employee _employee;
        private Mock<IUnitOfWork> _unitOfWorkMock;

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
                baseSalary = 50000,
                sickPay = SickPay.COSP
            };
        }

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
            _unitOfWorkMock.Verify(uow => uow.EmployeeRepository.Insert(result), Times.Once());
            _unitOfWorkMock.Verify(uow => uow.Save(), Times.Once());
        }

        //    TestCase for the CalculateLabourCost method
        [Test]
        public void CalculateLabourCost_WithValidInputs_ReturnsExpectedResult()
        {
            // Arrange
            var mockPayStrategy = new Mock<IPayFrequency>();
            mockPayStrategy.Setup(m => m.CalculateSalary(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(10000);

            var mockSickPayStrategy = new Mock<ISickPay>();
            mockSickPayStrategy.Setup(m => m.CalculateSickPay(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .Returns(1000);

            var mockDeductionStrategy = new Mock<IDeducionStrategy>();
            mockDeductionStrategy.Setup(m => m.CalculateDeduction(It.IsAny<decimal>()))
                .Returns(9000);

            Employee employee = new Employee
            {
                payStrategy = mockPayStrategy.Object,
                sickPayStrategy = mockSickPayStrategy.Object,
                deductionStrategy = mockDeductionStrategy.Object
            };

            employee.AddDeduction(Deduction.Pension);

            // Act
            var result = employee.CalculateLabourCost(new DateTime(2021, 01, 01), 8, 0, 0);

            // Assert
            Assert.AreEqual(9000, result);
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
            // Act
            var result = _employee.GetFullName();

            // Assert
            Assert.AreEqual("Rakesh Sharma", result);
        }

    }
}