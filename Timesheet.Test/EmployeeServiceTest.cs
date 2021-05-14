using NUnit.Framework;
using Moq;
using Timesheet.Domain.Repositories;
using Timesheet.Domain.Models;
using Timesheet.Application.Services;

namespace Timesheet.Test
{
    class EmployeeServiceTest
    {
        [Test]
        [TestCase("Иванов", 20000)]
        [TestCase("Петров", 30000)]
        [TestCase("Сидоров", 40000)]
        public void AddEmployee_ShouldReturnTrue(string lastName, decimal salary)
        {
            //arrange
            var employee = new StaffEmployee() { LastName = lastName, Salary = salary };

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(x => x.AddEmployee(employee)).Verifiable();

            var service = new EmployeeService(employeeRepositoryMock.Object);

            //act

            var result = service.AddEmployee(employee);

            //assert

            employeeRepositoryMock.Verify(x => x.AddEmployee(employee), Times.Once);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("Иванов", 0)]
        [TestCase("Иванов", -1000)]
        [TestCase("", 30000)]
        [TestCase(null, 40000)]
        public void Add_ShouldReturnFalse(string lastName, int salary)
        {
            //arrange
            var employee = new StaffEmployee() { LastName = lastName, Salary = salary };

            var employeeRepository = new Mock<IEmployeeRepository>();

            var service = new EmployeeService(employeeRepository.Object);

            //act
            var result = service.AddEmployee(employee);

            //assert
            employeeRepository.Verify(x => x.AddEmployee(It.IsAny<StaffEmployee>()), Times.Never);
            Assert.IsFalse(result);
        }
    }
}
