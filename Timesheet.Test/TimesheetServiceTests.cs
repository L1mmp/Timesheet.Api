using NUnit.Framework;
using System;
using Timesheet.Api.Models;
using Timesheet.Api.Services;

namespace Timesheet.Test
{
    class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            //arrange

            //act
            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHourhs = 1,
                LastName = "Иванов",
                Comment = Guid.NewGuid().ToString()
            };

            var service = new TimesheetService();


            var reuslt = service.TrackTime(timeLog);


            //assert


            Assert.IsTrue(reuslt);
        }

        [TestCase(-1,"")]
        [TestCase(-1,null)]
        [TestCase(-1, "testUser")]
        [TestCase(25,"")]
        [TestCase(25,null)]
        [TestCase(25, "testUser")]
        [TestCase(8, "")]
        [TestCase(8, null)]
        [TestCase(8, "testUser")]
        public void TrackTime_ShouldReturnFalse(int workingHours, string lastName)
        {
            //arrange

            //act

            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHourhs = workingHours,
                LastName = lastName

            };


            var service = new TimesheetService();

            var reuslt = service.TrackTime(timeLog);


            //assert


            Assert.IsFalse(reuslt);
        }
    }
}
