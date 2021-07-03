using System;
using System.Collections.Generic;
using System.IO;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.DataAccess.CSV
{

    public class TimesheetRepository : ITimesheetRepository
    {
        private const string PATH = "..\\Timesheet.DataAccess\\Data\\timesheet.csv";
        //private const string path = "\\home\\user\\MyProjects\\C#_Projects\\Timesheet.Api\\Timesheet.DataAccess\\timesheet.csv";
        private const char DELIMETER = ';';
        public void Add(TimeLog timeLog)
        {
            var dataRow = $"{timeLog.Comment}{DELIMETER}" +
                $"{timeLog.Date}{DELIMETER}" +
                $"{timeLog.LastName}{DELIMETER}" +
                $"{timeLog.WorkingHours}\n";

            File.AppendAllText(PATH, dataRow, System.Text.Encoding.UTF8);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {

            var data = File.ReadAllText(PATH);
            var datarows = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            var timeLogs = new List<TimeLog>();

            foreach (var dataRow in datarows)
            {
                var fields = dataRow.Split(DELIMETER);

                var timeLog = new TimeLog()
                {
                    Comment = fields[0],
                    Date = DateTime.TryParse(fields[1], out var date) ? date : new DateTime(),
                    LastName = fields[2],
                    WorkingHours = Int32.TryParse(fields[3], out var workingHours) ? workingHours : 0
                };
                timeLogs.Add(timeLog);
            }
            
            return timeLogs.ToArray();
        }
    }
}
