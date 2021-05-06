using System.Collections.Generic;
using Timesheet.Application.Services;

namespace Timesheet.Application.Services
{
    public class AuthService : IAuthService
    {
        public List<string> Employees { get; private set; }
        public AuthService()
        {
            Employees = new List<string>
            {
                "Иванов",
                "Петров",
                "Сидоров"
            };

        }

        public bool Login(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return false;

            var IsEmployeeExist = Employees.Contains(lastName);

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
