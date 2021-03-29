using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getallbrands")]
        public IActionResult GetAllCars()
        {
            var result = _brandService.GetAll();
            if (result.Success) return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpPost("addbrands")]
        public IActionResult Add(Brand brand)
        {
            var result = _brandService.Add(brand);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("deletebrands")]
        public IActionResult Delete(Brand brand)
        {
            var result = _brandService.Delete(brand);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("updatebrands")]
        public IActionResult Update(Brand brand)
        {
            var result = _brandService.Update(brand);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}