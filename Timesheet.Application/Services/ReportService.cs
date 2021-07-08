using System.Linq;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;
using Timesheet.Domain.Services;

namespace Timesheet.Application.Services
{
    public class ReportService : IReportService
    {
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

            decimal bill = employee.CalculateBill(timeLogs);
            var totalHours = timeLogs.Sum(x => x.WorkingHours);
            


            var result = new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                TotalHours = totalHours,
                Bill = bill,
                StartDate = timeLogs.Select(t => t.Date).Min(),
                EndDate = timeLogs.Select(t => t.Date).Max()
            };

            return result;
        }
    }
}