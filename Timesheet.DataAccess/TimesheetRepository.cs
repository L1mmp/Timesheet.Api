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

            File.AppendAllText(path, dataRow, System.Text.Encoding.UTF8);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {

            var data = File.ReadAllText(path);

            var timeLogs = new List<TimeLog>();

            foreach (var dataRow in data.Split('\n'))
            {
                var timeLog = new TimeLog()
                {
                    WorkingHours = 2
                };
                var dataMemebers = dataRow.Split(DELIMETER);

                timeLog.Comment = dataMemebers[0];
                timeLog.Date = DateTime.TryParse(dataMemebers[1], out var date) ? date : new DateTime();
                timeLog.LastName = dataMemebers[2];
                timeLog.WorkingHours = Int32.TryParse(dataMemebers[3], out var workingHours) ? workingHours : 0;
            }

            return timeLogs.ToArray();
        }
    }
}
