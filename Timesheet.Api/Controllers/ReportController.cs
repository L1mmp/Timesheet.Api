using Microsoft.AspNetCore.Mvc;
using Timesheet.Application.Services;
using Domain.Services;
using Timesheet.Domain.Models;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public ActionResult<EmployeeReport> Report(string lastName) => _reportService.GetEmployeeReport(lastName);
        
    }
}
