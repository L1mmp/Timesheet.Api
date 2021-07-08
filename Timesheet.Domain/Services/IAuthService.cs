using System.Collections.Generic;

namespace Timesheet.Domain.Services
{
    public interface IAuthService
    {
        bool Login(string lastName);
    }
}