using System;
using System.Collections.Generic;
using Timesheet.Api.Models;

namespace Timesheet.Api.Services
{
    public class TimesheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {
            bool IsTimelogValid = timeLog.WorkingHourhs <= 24 && timeLog.WorkingHourhs > 0
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
