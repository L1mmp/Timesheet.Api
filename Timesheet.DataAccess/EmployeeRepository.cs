using System.IO;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.DataAccess.CSV
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _path;
        
        private readonly char _delimeter = ';';

        public EmployeeRepository(CsvSettings csvSettings)
        {
            _path = csvSettings.Path + "\\employees.csv";
            _delimeter = csvSettings.Delimeter;
        }

        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{_delimeter}{staffEmployee.Salary}{_delimeter}\n";
            File.AppendAllText(_path, dataRow,System.Text.Encoding.UTF8);
        }
        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(_path);
            var datarows = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            StaffEmployee empoloyee = null;
            foreach (var dataRow in datarows)
            {
                if(dataRow.Contains(lastName))
                {
                    var fields = dataRow.Split(_delimeter);
                    
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
