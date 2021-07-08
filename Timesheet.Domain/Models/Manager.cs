using System.Linq;

namespace Timesheet.Domain.Models
{
    class Manager : Employee
    {
        public Manager(string lastName, decimal salary) : base(lastName, salary)
        {
        }

        private const decimal MANAGER_OVERTIME_SALARY = 20000m;

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
                    bill += MAX_WORKING_HOURS_IN_DAY * Salary / MAX_WORKING_HOURS_IN_MONTH;
                    bill += MANAGER_OVERTIME_SALARY;
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
