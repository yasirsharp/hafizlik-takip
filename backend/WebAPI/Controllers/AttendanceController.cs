using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService) { _attendanceService = attendanceService; }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _attendanceService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbystudentid")]
        public IActionResult GetByStudentId(int studentId)
        {
            var result = _attendanceService.GetByStudentId(studentId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbydate")]
        public IActionResult GetByDate(DateTime date)
        {
            var result = _attendanceService.GetByDate(date);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Attendance attendance)
        {
            var result = _attendanceService.Add(attendance);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Attendance attendance)
        {
            var result = _attendanceService.Update(attendance);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
