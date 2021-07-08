using System.Linq;

namespace Timesheet.Domain.Models
{
    public class Freelancer : Employee
    {
        public Freelancer(string lastName, decimal salary) : base(lastName, salary)
        {
        }

        public override decimal CalculateBill(TimeLog[] timeLogs)
        {
            decimal bill = 0m;

            var totalHours = timeLogs.Sum(x => x.WorkingHours);

            bill += totalHours * Salary;

            return bill;
        }
    }
}
