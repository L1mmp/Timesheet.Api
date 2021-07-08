using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Domain.Models;
using Timesheet.Domain.Services;
using Timesheet.Domain.Repositories;

namespace Timesheet.Application.Services
{
    public class ReportService : IReportService
    {
        private const int MAX_WORKING_HOURS_IN_MONTH = 160;
        private const int MAX_WORKING_HOURS_IN_DAY = 8;
        private const decimal OVERTIME_MULTIPLIER = 2m;
        private const decimal MANAGER_OVERTIME_SALARY = 20000m;
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public ReportService(ITimesheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
        {
            _timesheetRepository = timesheetRepository;
            _employeeRepository = employeeRepository;
        }


        public EmployeeReport GetEmployeeReport(string lastName)
        {
            var employee = _employeeRepository.GetEmployee(lastName);
            var timeLogs = _timesheetRepository.GetTimeLogs(employee.LastName);

            if (timeLogs.Length == 0 || timeLogs == null)
            {
                return new EmployeeReport
                {
                    Bill = 0m,
                    TotalHours = 0,
                    LastName = employee.LastName
                };
            }

            decimal bill = 0m;
            var totalHours = timeLogs.Sum(x => x.WorkingHours);

            switch (lastName)
            {
                //Staff
                case "Петров":
                    {
                        var workingHoursGroupByDay = timeLogs
                            .GroupBy(x => x.Date.ToShortDateString());

                        foreach (var workingLogsPerDay in workingHoursGroupByDay)
                        {
                            int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                            if (dayHours > MAX_WORKING_HOURS_IN_DAY)
                            {
                                var overtimeHours = dayHours - MAX_WORKING_HOURS_IN_DAY;

                                bill += MAX_WORKING_HOURS_IN_DAY * employee.Salary / MAX_WORKING_HOURS_IN_MONTH;
                                bill += overtimeHours * employee.Salary * OVERTIME_MULTIPLIER / MAX_WORKING_HOURS_IN_MONTH;
                            }
                            else
                            {
                                bill += dayHours * employee.Salary / MAX_WORKING_HOURS_IN_MONTH;
                            }

                        }
                        break;
                    }
                case "Иванов":
                    //Freelancer
                    {
                        bill += totalHours * employee.Salary;
                        break;
                    }
                case "Сидоров":
                    //Manager
                    {
                        var workingHoursGroupByDay = timeLogs
                            .GroupBy(x => x.Date.ToShortDateString());

                        foreach (var workingLogsPerDay in workingHoursGroupByDay)
                        {
                            int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                            if (dayHours > MAX_WORKING_HOURS_IN_DAY)
                            {
                                bill += MAX_WORKING_HOURS_IN_DAY * employee.Salary / MAX_WORKING_HOURS_IN_MONTH;
                                bill += MANAGER_OVERTIME_SALARY;
                            }
                            else
                            {
                                bill += dayHours * employee.Salary / MAX_WORKING_HOURS_IN_MONTH;
                            }

                        }
                        break;
                    }

                default:
                    break;
            }



            var result = new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                TotalHours = totalHours,
                Bill = bill
            };

            return result;
        }
    }
}