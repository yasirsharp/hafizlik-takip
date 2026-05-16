using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private IHomeworkService _homeworkService;
        public HomeworksController(IHomeworkService homeworkService) { _homeworkService = homeworkService; }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _homeworkService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbystudentid")]
        public IActionResult GetByStudentId(int studentId)
        {
            var result = _homeworkService.GetByStudentId(studentId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Homework homework)
        {
            var result = _homeworkService.Add(homework);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Homework homework)
        {
            var result = _homeworkService.Update(homework);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Homework homework)
        {
            var result = _homeworkService.Delete(homework);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
