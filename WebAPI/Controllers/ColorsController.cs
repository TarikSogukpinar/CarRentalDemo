using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("getallcolors")]
        public IActionResult GetAll()
        {
            var result = _colorService.GetAll();
            if (result.Success) return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpPost("addcolors")]
        public IActionResult Add(Color color)
        {
            var result = _colorService.Add(color);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("deletecolors")]
        public IActionResult Delete(Color color)
        {
            var result = _colorService.Delete(color);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("updatecolors")]
        public IActionResult Update(Color color)
        {
            var result = _colorService.Update(color);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}