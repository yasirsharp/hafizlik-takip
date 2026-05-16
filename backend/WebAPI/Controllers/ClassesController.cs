using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private IClassService _classService;
        public ClassesController(IClassService classService) { _classService = classService; }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _classService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _classService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Class classEntity)
        {
            var result = _classService.Add(classEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Class classEntity)
        {
            var result = _classService.Update(classEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Class classEntity)
        {
            var result = _classService.Delete(classEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
