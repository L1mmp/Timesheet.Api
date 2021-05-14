using System.Collections.Generic;

namespace Timesheet.Application.Services
{
    public interface IAuthService
    {
        bool Login(string lastName);
    }
}