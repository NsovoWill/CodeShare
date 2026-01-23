using Microsoft.AspNetCore.Mvc;
using Models;
using System.Globalization;
using UnitOfWork;
using static System.Net.Mime.MediaTypeNames;

namespace MEIBCLocationAPI.Controllers
{
    [Route("[controller]")]
    public class CalendarController : Controller
    {

        private readonly ILogger<CalendarController> _logger;
        public IConfiguration _configuration { get; set; }
        public ICalendarUnitOfWork _calendarUnitOfWork { get; set; }



        public CalendarController(IConfiguration configuration, ILogger<CalendarController> logger, ICalendarUnitOfWork calendarUnitOfWork)
        {
            _logger = logger;
            _configuration = configuration;
            _calendarUnitOfWork = calendarUnitOfWork;
        }

        [HttpGet(Name = "Index")]
        public IActionResult Index(int? RegionId, string? AllocatedTo, int? CommissionerID)
        {
            CalendarEvents calendarData = new CalendarEvents();
            _logger.LogInformation("CalendarController Index called with RegionId: {RegionId}, AllocatedTo: {AllocatedTo}", RegionId, AllocatedTo);
            if(CommissionerID != null && CommissionerID != 0)
            {
                _logger.LogInformation("CommissionerID: {CommissionerID}", CommissionerID);
                calendarData = _calendarUnitOfWork.GetCommissionerCalenderData(CommissionerID);

                return View(calendarData);


            }
            else
            {
                calendarData = _calendarUnitOfWork.GetCalendarData(RegionId, AllocatedTo);
            }
              

            return View(calendarData);
        }
    }
}
