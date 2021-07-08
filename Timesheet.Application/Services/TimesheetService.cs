using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;
using Timesheet.Domain.Services;

namespace Timesheet.Application.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;

        public TimesheetService(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }

        public bool TrackTime(TimeLog timeLog)
        {
            bool IsTimelogValid = timeLog.WorkingHours <= 24 && timeLog.WorkingHours > 0
            && !string.IsNullOrWhiteSpace(timeLog.LastName);

            IsTimelogValid = IsTimelogValid && UserSession.Sessions.Contains(timeLog.LastName);

            if (IsTimelogValid)
            {
                _timesheetRepository.Add(timeLog);
                return true;
            }

            return false;
        }

    }
}
