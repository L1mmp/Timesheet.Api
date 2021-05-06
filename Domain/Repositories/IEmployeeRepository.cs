using Timesheet.Domain.Models;

namespace Timesheet.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        StaffEmployee GetEmployee(string lastName);
    }
}