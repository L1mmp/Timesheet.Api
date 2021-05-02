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
                LastName = ""

            };

            var service = new TimesheetService();


            var reuslt = service.TrackTime(timeLog);


            //assert


            Assert.IsTrue(reuslt);
        }
        [Test]
        public void TrackTime_ShouldReturnFalse()
        {
            //arrange

            //act

            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHourhs = 1,
                LastName = ""

            };


            var service = new TimesheetService();

            var reuslt = service.TrackTime(timeLog);


            //assert


            Assert.IsFalse(reuslt);
        }
    }
}
