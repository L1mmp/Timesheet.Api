using Timesheet.Domain.Models;

namespace Domain.Services
{
    public interface IReportService
    {
        EmployeeReport GetEmployeeReport(string lastName);
    }
}