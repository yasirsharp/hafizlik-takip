using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/teacher-students")]
    [ApiController]
    public class TeacherStudentsController : ControllerBase
    {
        private ITeacherStudentService _teacherStudentService;
        public TeacherStudentsController(ITeacherStudentService teacherStudentService) { _teacherStudentService = teacherStudentService; }

        [HttpGet("getbyteacherid")]
        public IActionResult GetByTeacherId(int teacherUserId)
        {
            var result = _teacherStudentService.GetByTeacherId(teacherUserId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getofficialstudents")]
        public IActionResult GetOfficialStudents(int teacherUserId)
        {
            var result = _teacherStudentService.GetOfficialStudents(teacherUserId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(TeacherStudentAssignment assignment)
        {
            var result = _teacherStudentService.Add(assignment);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(TeacherStudentAssignment assignment)
        {
            var result = _teacherStudentService.Update(assignment);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(TeacherStudentAssignment assignment)
        {
            var result = _teacherStudentService.Delete(assignment);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
