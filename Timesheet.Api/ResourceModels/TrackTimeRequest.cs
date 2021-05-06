using System;
using Timesheet.Domain.Models;

namespace Timesheet.Api.ResourceModels
{
    public class TrackTimeRequest
    {
        public string Date { get; set; }
        public int WorkingHours { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }

        public TimeLog GetTimeLog()
        {
            return new TimeLog()
            {
                Date = DateTime.Parse(Date),
                WorkingHours = WorkingHours,
                LastName = LastName,
                Comment = Comment
            };


        }
    }
}
