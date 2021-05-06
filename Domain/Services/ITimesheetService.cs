using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
    public interface ITimesheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}