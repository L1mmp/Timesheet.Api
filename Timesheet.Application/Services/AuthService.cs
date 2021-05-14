using System.Collections.Generic;
using Timesheet.Application.Services;
using Timesheet.Domain.Models;
using Timesheet.Domain.Repositories;

namespace Timesheet.Application.Services
{
    public class AuthService : IAuthService
    {
        IEmployeeRepository _employeeRepository;

        public AuthService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool Login(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return false;

            StaffEmployee employee = _employeeRepository.GetEmployee(lastName);
            bool IsEmployeeExist = (employee != null);

            if (IsEmployeeExist)
            {
                UserSession.Sessions.Add(lastName);
            }

            return IsEmployeeExist;
        }
    }

    public static class UserSession
    {
        public static HashSet<string> Sessions = new HashSet<string>();
    }
}
