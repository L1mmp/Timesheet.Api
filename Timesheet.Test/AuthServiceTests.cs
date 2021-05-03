using NUnit.Framework;
using Timesheet.Api.Services;

namespace Timesheet.Test
{
    public class AuthServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        

        [TestCase("Иванов")]
        [TestCase("Петров")]
        [TestCase("Сидоров")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            //arrange

            var service = new AuthService();

            //act qwe

            var result = service.Login(lastName);

            //assert

            Assert.IsNotNull(UserSession.Sessions);
            Assert.IsNotEmpty(UserSession.Sessions);
            Assert.IsTrue(UserSession.Sessions.Contains(lastName));
            Assert.IsTrue(result);
        }
        [Test]
        public void Login_InvokeLoginTwiceForOneLastName_ShouldReturnTrue()
        {
            //arrange
            var lastName = "Иванов";

            var service = new AuthService();


            //wqe
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
        [TestCase("Test123")]
        public void Login_ShouldReturnFalse(string lastName)
        {
            //arrange

            var service = new AuthService();

            //act 

            var result = service.Login(lastName);

            //assert

            Assert.IsFalse(result);
        }
    }
}