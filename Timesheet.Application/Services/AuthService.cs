using System.Collections.Generic;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;
using Timesheet.Domain.Services;

namespace Timesheet.Application.Services
{
    public class AuthService : IAuthService
    {
        readonly IEmployeeRepository _employeeRepository;

        public AuthService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool Login(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return false;

            Employee employee = _employeeRepository.GetEmployee(lastName);
            bool isEmployeeExist = (employee != null);

            if (isEmployeeExist)
            {
                UserSession.Sessions.Add(lastName);
            }

            return isEmployeeExist;
        }
    }

    public static class UserSession
    {
        public static HashSet<string> Sessions = new HashSet<string>();
    }
}
