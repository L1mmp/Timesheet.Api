using Moq;
using NUnit.Framework;
using Timesheet.Application.Services;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

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
            var employee = new StaffEmployee(lastName, salary);

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
            var employee = new StaffEmployee(lastName, salary);

            var employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository.Setup(x => x.AddEmployee(employee)).Verifiable();

            var service = new EmployeeService(employeeRepository.Object);

            //act
            var result = service.AddEmployee(employee);

            //assert
            employeeRepository.Verify(x => x.AddEmployee(It.IsAny<Employee>()), Times.Never);
            Assert.IsFalse(result);
        }
    }
}
