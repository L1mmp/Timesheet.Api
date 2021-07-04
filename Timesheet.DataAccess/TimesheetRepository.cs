using System;
using System.Collections.Generic;
using System.IO;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.DataAccess.CSV
{

    public class TimesheetRepository : ITimesheetRepository
    {

        private readonly string _path;
        private readonly char _delimeter;
        public TimesheetRepository(CsvSettings csvSettings)
        {
            _path = csvSettings.Path + "\\timesheet.csv";
            _delimeter = csvSettings.Delimeter;
        }

        public void Add(TimeLog timeLog)
        {
            var dataRow = $"{timeLog.Comment}{_delimeter}" +
                $"{timeLog.Date}{_delimeter}" +
                $"{timeLog.LastName}{_delimeter}" +
                $"{timeLog.WorkingHours}\n";

            File.AppendAllText(_path, dataRow, System.Text.Encoding.UTF8);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {

            var data = File.ReadAllText(_path);
            var datarows = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            var timeLogs = new List<TimeLog>();

            foreach (var dataRow in datarows)
            {
                var fields = dataRow.Split(_delimeter);

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
