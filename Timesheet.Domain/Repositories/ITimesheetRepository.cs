using Timesheet.Domain.Models;

namespace Timesheet.Domain.Repositories
{
    public interface ITimesheetRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
        void Add(TimeLog timeLog);
    }
}
