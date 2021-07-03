using System.IO;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.DataAccess.CSV
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private const string PATH = "..\\Timesheet.DataAccess\\Data\\employees.csv";
        //private const string path = "\\home\\user\\MyProjects\\C#_Projects\\Timesheet.Api\\Timesheet.DataAccess\\employees.csv";
        private const char DELIMETER = ';';
        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{DELIMETER}{staffEmployee.Salary}{DELIMETER}\n";
            File.AppendAllText(PATH, dataRow,System.Text.Encoding.UTF8);
        }
        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(PATH);
            var datarows = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            StaffEmployee empoloyee = null;
            foreach (var dataRow in datarows)
            {
                if(dataRow.Contains(lastName))
                {
                    var fields = dataRow.Split(DELIMETER);
                    
                    empoloyee = new StaffEmployee()
                    {
                        LastName = fields[0],
                        Salary = decimal.TryParse(fields[1], out decimal Salary) ? Salary : 0m
                    };

                    break; // 
                }
            }
            return empoloyee;
        }
    }
}
