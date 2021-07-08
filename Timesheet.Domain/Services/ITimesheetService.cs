using Timesheet.Domain.Models;

namespace Timesheet.Domain.Services
{
    public interface ITimesheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}