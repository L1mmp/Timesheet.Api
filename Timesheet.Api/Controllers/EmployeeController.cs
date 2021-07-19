using Microsoft.AspNetCore.Mvc;
using Timesheet.Domain.Models;
using Timesheet.Domain.Services;

namespace Timesheet.Api.ResourceModels
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public ActionResult<bool> Add(Employee staffEmployee) => Ok(_employeeService.AddEmployee(staffEmployee));
    }
}
