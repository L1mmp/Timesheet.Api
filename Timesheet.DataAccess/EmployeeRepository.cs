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

        public void AddEmployee(Employee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{_delimeter}{staffEmployee.Salary}{_delimeter}\n";
            File.AppendAllText(_path, dataRow,System.Text.Encoding.UTF8);
        }
        public Employee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(_path);
            var datarows = data.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            Employee empoloyee = null;
            foreach (var dataRow in datarows)
            {
                if(dataRow.Contains(lastName))
                {
                    var fields = dataRow.Split(_delimeter);

                    var position = fields[3];

                    decimal salary;

                    decimal.TryParse(fields[1], out salary);

                    switch (position)
                    {
                        case "Руководитель":
                            empoloyee = new Manager(lastName, salary);
                            break;
                        case "Штатный сотрудник":
                            empoloyee = new StaffEmployee(lastName, salary);
                            break;
                        case "Фрилансер":
                            empoloyee = new Freelancer(lastName, salary);
                            break;
                        default:
                            break;
                    }

                    //empoloyee = new Employee(fields[0], decimal.TryParse(fields[1], out decimal Salary) ? Salary : 0m);

                    break;
                }
            }
            return empoloyee;
        }
    }
}
