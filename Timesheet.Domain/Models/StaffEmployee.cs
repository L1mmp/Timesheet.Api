
using System.Linq;

namespace Timesheet.Domain.Models
{
    public class StaffEmployee : Employee
    {

        private const decimal OVERTIME_MULTIPLIER = 2m;

        public StaffEmployee(string lastName, decimal salary) : base(lastName, salary)
        {
        }

        public override decimal CalculateBill(TimeLog[] timeLogs)
        {
            var workingHoursGroupByDay = timeLogs
                .GroupBy(x => x.Date.ToShortDateString());
            decimal bill = 0m;


            foreach (var workingLogsPerDay in workingHoursGroupByDay)
            {
                int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                if (dayHours > MAX_WORKING_HOURS_IN_DAY)
                {
                    var overtimeHours = dayHours - MAX_WORKING_HOURS_IN_DAY;

                    bill += MAX_WORKING_HOURS_IN_DAY * Salary / MAX_WORKING_HOURS_IN_MONTH;
                    bill += overtimeHours * Salary * OVERTIME_MULTIPLIER / MAX_WORKING_HOURS_IN_MONTH;
                }
                else
                {
                    bill += dayHours * Salary / MAX_WORKING_HOURS_IN_MONTH;
                }

            }


            return bill;
        }
    }
}
