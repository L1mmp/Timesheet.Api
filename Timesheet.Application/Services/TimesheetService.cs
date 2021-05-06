using System;
using System.Collections.Generic;
using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
    public class TimesheetService : ITimesheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {
            bool IsTimelogValid = timeLog.WorkingHours <= 24 && timeLog.WorkingHours > 0
            && !string.IsNullOrWhiteSpace(timeLog.LastName);

            IsTimelogValid = IsTimelogValid && UserSession.Sessions.Contains(timeLog.LastName);

            if (IsTimelogValid)
            {
                Timesheets.TimeLogs.Add(timeLog);
                return true;
            }

            return false;
        }

    }
    public static class Timesheets
    {
        public static List<TimeLog> TimeLogs = new List<TimeLog>();
    }
}
