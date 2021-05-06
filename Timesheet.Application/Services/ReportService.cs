using System;
using Timesheet.Domain.Models;
using System.Collections.Generic;
using Timesheet.Domain.Repositories;
using System.Linq;

namespace Timesheet.Application.Services
{
    public class ReportService
    {
        private const decimal MAX_WORKING_HOURS_IN_MONTH = 160m;
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

            int hours = timeLogs.Sum(x => x.WorkingHours);
            decimal bill = (hours / MAX_WORKING_HOURS_IN_MONTH) * employee.Salary;
            //additionalBill (>160hrs)

            var result = new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                TotalHours = hours,
                Bill = bill
            };

            return result;
        }
    }
}