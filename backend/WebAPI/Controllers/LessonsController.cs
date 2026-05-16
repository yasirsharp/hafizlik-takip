using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private ILessonService _lessonService;
        public LessonsController(ILessonService lessonService) { _lessonService = lessonService; }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _lessonService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbystudentid")]
        public IActionResult GetByStudentId(int studentId)
        {
            var result = _lessonService.GetByStudentId(studentId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _lessonService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Lesson lesson)
        {
            var result = _lessonService.Add(lesson);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Lesson lesson)
        {
            var result = _lessonService.Update(lesson);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Lesson lesson)
        {
            var result = _lessonService.Delete(lesson);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
