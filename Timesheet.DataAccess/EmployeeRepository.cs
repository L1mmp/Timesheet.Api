using System.IO;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.DataAccess.CSV
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string PATH = "..\\Timesheet.DataAccess\\Data\\employees.csv";
        private const char DELIMETER = ';';
        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{DELIMETER}{staffEmployee.Salary}{DELIMETER}\n";
            File.AppendAllText(PATH, dataRow,System.Text.Encoding.UTF8);
        }
        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(PATH);

            StaffEmployee empoloyee = null;

            foreach (var dataRow in data.Split('\n'))
            {
                if(dataRow.Contains(lastName))
                {
                    var dataMembers = dataRow.Split(DELIMETER);

                    empoloyee = new StaffEmployee()
                    {
                        LastName = lastName,
                        Salary = decimal.TryParse(dataMembers[1], out decimal Salary) ? Salary : 0m
                    };
                }
            }
            return empoloyee;
        }
    }
}
