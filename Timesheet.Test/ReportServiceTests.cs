using NUnit.Framework;
using Timesheet.Api.Services;
using Timesheet.Api.Models;


namespace Timesheet.Test
{
    class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            //arrange 

            var service = new ReportService();

            var expectedLastName = "Иванов";
            var expectedTotal = 100m;

            var result = service.GetEmployeeReport(expectedLastName);


            //act


            //assert

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.timeLogs);
            Assert.IsNotEmpty(result.timeLogs);
            Assert.AreEqual(expectedTotal, result.Bill);
        }
    }


}
