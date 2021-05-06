using System.Collections.Generic;

namespace Timesheet.Application.Services
{
    public interface IAuthService
    {
        List<string> Employees { get; }

        bool Login(string lastName);
    }
}