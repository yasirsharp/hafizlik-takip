using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/duty-schedule")]
    [ApiController]
    public class DutyScheduleController : ControllerBase
    {
        private IDutyScheduleService _dutyScheduleService;
        public DutyScheduleController(IDutyScheduleService dutyScheduleService) { _dutyScheduleService = dutyScheduleService; }

        // Constraints
        [HttpGet("constraints/getall")]
        public IActionResult GetConstraints()
        {
            var result = _dutyScheduleService.GetConstraints();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("constraints/add")]
        public IActionResult AddConstraint(DutyScheduleConstraint constraint)
        {
            var result = _dutyScheduleService.AddConstraint(constraint);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("constraints/update")]
        public IActionResult UpdateConstraint(DutyScheduleConstraint constraint)
        {
            var result = _dutyScheduleService.UpdateConstraint(constraint);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // Weeks
        [HttpGet("weeks/getbyid")]
        public IActionResult GetWeekById(int id)
        {
            var result = _dutyScheduleService.GetWeekById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("weeks/getbydaterange")]
        public IActionResult GetWeeksByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = _dutyScheduleService.GetWeeksByDateRange(startDate, endDate);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("weeks/generate")]
        public IActionResult GenerateWeek(int constraintId, DateTime weekStartDate, int createdByUserId)
        {
            var result = _dutyScheduleService.GenerateWeek(constraintId, weekStartDate, createdByUserId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("weeks/approve")]
        public IActionResult ApproveWeek(int weekId)
        {
            var result = _dutyScheduleService.ApproveWeek(weekId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // Entries
        [HttpGet("entries/getbyweekid")]
        public IActionResult GetEntriesByWeekId(int weekId)
        {
            var result = _dutyScheduleService.GetEntriesByWeekId(weekId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("entries/add")]
        public IActionResult AddEntry(DutyScheduleEntry entry)
        {
            var result = _dutyScheduleService.AddEntry(entry);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("entries/delete")]
        public IActionResult DeleteEntry(int entryId)
        {
            var result = _dutyScheduleService.DeleteEntry(entryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
