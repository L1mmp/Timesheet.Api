using System.Collections.Generic;
using Timesheet.Domain.Models;

namespace Timesheet.Domain.Repositories
{
    public interface ITimesheetRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
    }
}
