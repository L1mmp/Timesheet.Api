using Moq;
using NUnit.Framework;
using System;
using Timesheet.Application.Services;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.Test
{
    class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            //arrange 

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов2";
            var expectedTotal = 16 * 625 + 4 * 625 * 2;
            var expectedTotalHours = 20;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now,
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()

                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 12,
                        Comment = Guid.NewGuid().ToString()

                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-2),
                        WorkingHours = 0,
                        Comment = Guid.NewGuid().ToString()

                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, 100000))
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            var result = service.GetEmployeeReport(expectedLastName);

            //act

            //assert

            timesheetRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }
        [Test]
        public void GetEmployeeReport_ShouldReturnReportForSeveralMonths()
        {
            //arrange

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 35 * 8 * 625m + 1 * 625 * 2;
            var expectedTotalHours = 281;


            //(20 * 8 * 750 + 10 * 8 * 750 * 2) + (1 * 750 * 2) + (5 * 8 * 750) = 120 000 + 120 000 + 1500 + 30 000 = 271500
            //35 * 8 * 625 + 1 * 625 * 2
            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                {
                    TimeLog[] timeLogs = new TimeLog[35];
                    DateTime dateTime = new DateTime(2021, 11, 1);
                    timeLogs[0] = new TimeLog
                    {
                        Date = dateTime,
                        WorkingHours = 9,
                        LastName = expectedLastName,
                        Comment = new Guid().ToString()
                    };
                    for (int i = 1; i < timeLogs.Length; i++)
                    {
                        dateTime = dateTime.AddDays(1);
                        timeLogs[i] = new TimeLog
                        {
                            Date = dateTime,
                            WorkingHours = 8,
                            LastName = expectedLastName,
                            Comment = new Guid().ToString()
                        };

                    }
                    return timeLogs;
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, 100000))
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            var result = service.GetEmployeeReport(expectedLastName);

            //act

            timesheetRepositoryMock.VerifyAll();

            //assert


            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReport_WithoutTimeLogs_ShouldReturnReportForSeveralMonths()
        {
            //arrange

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 0m;
            var expectedTotalHours = 0;

            //(20 * 8 * 750 + 10 * 8 * 750 * 2) + (1 * 750 * 2) + (5 * 8 * 750) = 120 000 + 120 000 + 1500 + 30 000 = 271500

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new TimeLog[0])
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, 120000))
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            var result = service.GetEmployeeReport(expectedLastName);

            //act

            timesheetRepositoryMock.VerifyAll();

            //assert


            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }
        [Test]
        public void GetEmployeeReport_ShouldReturnReportForOneDay()
        {
            //arrange 

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 5000m; // 625 * 8
            var expectedTotalHours = 8;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                   new TimeLog
                   {
                       WorkingHours = 8,
                       LastName = expectedLastName,
                       Date = DateTime.Now.AddDays(-1),
                       Comment = new Guid().ToString()
                   }

                })

                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName,100000))
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            var result = service.GetEmployeeReport(expectedLastName);

            //act

            //assert

            timesheetRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        [TestCase("Иванов", 70000, 4500)]// 8m / 160m * 70000m + 1000 (у руководителей бонус 1000 за день вне зависимости от переаботанных часов)
        [TestCase("Петров", 70000, 7000)]// 8m / 160m * 70000m + 4m / 160m * 70000m * 2
        [TestCase("Сидоров", 1000, 12000)]// 12m * 1000 = 12000
        public void GetEmployeeReport_WithOvertime_ShouldReturnReportForOneDay(string expectedLastName, decimal salary, decimal expectedTotal)
        {
            //arrange 

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedTotalHours = 12;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                   new TimeLog
                   {
                       WorkingHours = 12,
                       LastName = expectedLastName,
                       Date = DateTime.Now.AddDays(-1),
                       Comment = new Guid().ToString()
                   }

                })

                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, salary))
                .Verifiable();

            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            var result = service.GetEmployeeReport(expectedLastName);

            //act

            //assert

            timesheetRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }
    }
}
