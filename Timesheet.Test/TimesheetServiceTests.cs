using NUnit.Framework;
using System;
using Timesheet.Domain.Models;
using Timesheet.Application.Services;
using Moq;
using Timesheet.Domain.Repositories;

namespace Timesheet.Test
{
    class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            //arrange
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();

            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHours = 1,
                LastName = "Иванов",
                Comment = Guid.NewGuid().ToString()
            };

            timesheetRepositoryMock
                .Setup(x => x.Add(timeLog))
                .Verifiable();



            var service = new TimesheetService(timesheetRepositoryMock.Object);
            //act



            var reuslt = service.TrackTime(timeLog);


            //assert
            timesheetRepositoryMock.Verify(x => x.Add(timeLog), Times.Once);

            Assert.IsTrue(reuslt);
        }

        [TestCase(-1, "")]
        [TestCase(-1, null)]
        [TestCase(-1, "testUser")]
        [TestCase(25, "")]
        [TestCase(25, null)]
        [TestCase(25, "testUser")]
        [TestCase(8, "")]
        [TestCase(8, null)]
        [TestCase(8, "testUser")]
        public void TrackTime_ShouldReturnFalse(int workingHours, string lastName)
        {
            //arrange

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();

            var timeLog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHours = workingHours,
                LastName = lastName

            };

            timesheetRepositoryMock
                .Setup(x => x.Add(timeLog))
                .Verifiable();

            var service = new TimesheetService(timesheetRepositoryMock.Object);

            //act

            var reuslt = service.TrackTime(timeLog);

            //assert

            timesheetRepositoryMock.Verify(x => x.Add(timeLog), Times.Never);

            Assert.IsFalse(reuslt);
        }
    }
}
