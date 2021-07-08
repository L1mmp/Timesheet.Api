using Microsoft.AspNetCore.Mvc;
using Timesheet.Api.ResourceModels;
using Timesheet.Domain.Services;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost]
        public ActionResult<bool> TrackTime(TrackTimeRequest request) => Ok(_timesheetService.TrackTime(request.GetTimeLog()));
    }
}
