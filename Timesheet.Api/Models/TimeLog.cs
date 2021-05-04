﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.Models
{
    public class TimeLog
    {
        public DateTime Date { get; set; }

        public int WorkingHourhs { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
    }
}
