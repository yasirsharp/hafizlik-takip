using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MistakesController : ControllerBase
    {
        private IMistakeService _mistakeService;
        public MistakesController(IMistakeService mistakeService) { _mistakeService = mistakeService; }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _mistakeService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbylessonid")]
        public IActionResult GetByLessonId(int lessonId)
        {
            var result = _mistakeService.GetByLessonId(lessonId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Mistake mistake)
        {
            var result = _mistakeService.Add(mistake);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Mistake mistake)
        {
            var result = _mistakeService.Delete(mistake);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
