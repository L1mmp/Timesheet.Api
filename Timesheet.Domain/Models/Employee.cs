namespace Timesheet.Domain.Models
{
    public abstract class Employee
    {
        public string LastName { get; set; }
        public decimal Salary { get; set; }

        protected const int MAX_WORKING_HOURS_IN_MONTH = 160;
        protected const int MAX_WORKING_HOURS_IN_DAY = 8;

        protected Employee(string lastName, decimal salary)
        {
            LastName = lastName;
            Salary = salary;
        }
        public abstract decimal CalculateBill(TimeLog[] timeLogs);

    }
}
