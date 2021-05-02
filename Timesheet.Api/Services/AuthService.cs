using System.Collections.Generic;

namespace Timesheet.Api.Services
{
    public class AuthService
    {
        public AuthService()
        {
            Employees = new List<string>
            {
                "Иванов",
                "Петров",
                "Сидоров"
            };

        }

        public List<string> Employees { get; private set; }

        public bool Login(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return false;

            var IsEmployeeExist = Employees.Contains(lastName);

            if(IsEmployeeExist)
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
