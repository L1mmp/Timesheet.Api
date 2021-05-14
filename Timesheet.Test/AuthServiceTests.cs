using NUnit.Framework;
using Timesheet.Application.Services;
using Moq;
using Timesheet.Domain.Repositories;
using Timesheet.Domain.Models;

namespace Timesheet.Test
{
    public class AuthServiceTests
    {
        [TestCase("Иванов")]
        [TestCase("Петров")]
        [TestCase("Сидоров")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            //arrange
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(x => x.GetEmployee(It.Is<string>(y => y == (lastName))))
                .Returns(() => new StaffEmployee()
                {
                    LastName = lastName,
                    Salary = 120000
                }
                )
                .Verifiable()
            ;

            var service = new AuthService(employeeRepository.Object);

            //act

            var result = service.Login(lastName);

            //assert
            employeeRepository.VerifyAll();

            Assert.IsNotNull(UserSession.Sessions);
            Assert.IsNotEmpty(UserSession.Sessions);
            Assert.IsTrue(UserSession.Sessions.Contains(lastName));
            Assert.IsTrue(result);
        }
        [Test]
        [TestCase("Иванов")]
        [TestCase("Петров")]
        [TestCase("Сидоров")]
        public void Login_InvokeLoginTwiceForOneLastName_ShouldReturnTrue(string lastName)
        {
            //arrange

            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(x => x.GetEmployee(It.Is<string>(y => y == (lastName))))
                .Returns(() => new StaffEmployee()
                {
                    LastName = lastName,
                    Salary = 120000
                }
                )
                .Verifiable()
            ;

            var service = new AuthService(employeeRepository.Object);

            //act 

            var result = service.Login(lastName);
            result = service.Login(lastName);

            //assert
            Assert.IsNotNull(UserSession.Sessions);
            Assert.IsNotEmpty(UserSession.Sessions);
            Assert.IsTrue(UserSession.Sessions.Contains(lastName));
            Assert.IsTrue(result);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Login_ShouldReturnFalse(string lastName)
        {
            //arrange

            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(x => x.GetEmployee(lastName))
                .Returns(() => null)
                .Verifiable();


            var service = new AuthService(employeeRepository.Object);

            //act 

            var result = service.Login(lastName);

            //assert

            employeeRepository.Verify(x => x.GetEmployee(lastName), Times.Never);



            Assert.IsFalse(UserSession.Sessions.Contains(lastName));
            Assert.IsFalse(result);
        }

        [TestCase("Test123")]
        public void Login_UserDoesntExist_ShouldReturnFalse(string lastName)
        {
            //arrange

            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(x => x.GetEmployee(lastName))
                .Returns(() => null)
                .Verifiable();


            var service = new AuthService(employeeRepository.Object);

            //act 

            var result = service.Login(lastName);

            //assert

            employeeRepository.Verify(x => x.GetEmployee(lastName), Times.Once);



            Assert.IsFalse(UserSession.Sessions.Contains(lastName));
            Assert.IsFalse(result);
        }
    }
}