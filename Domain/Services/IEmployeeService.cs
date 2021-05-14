using Timesheet.Domain.Models;

namespace Timesheet.Domain.Services
{
    public interface IEmployeeService
    {
        bool AddEmployee(StaffEmployee staffEmployee);
    }
}
