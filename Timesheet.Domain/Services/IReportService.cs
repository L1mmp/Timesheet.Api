using Timesheet.Domain.Models;

namespace Timesheet.Domain.Services
{
    public interface IReportService
    {
        EmployeeReport GetEmployeeReport(string lastName);
    }
}