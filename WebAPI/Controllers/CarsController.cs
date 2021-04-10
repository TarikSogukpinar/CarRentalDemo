using System.Threading;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getallcars")]
        public IActionResult GetAllCars()
        {
            const int sleepTime = 5000;
            Thread.Sleep(sleepTime);
            
            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbycarıd")]
        public IActionResult GetById(int carId)
        {
            var result = _carService.GetById(carId);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
        
        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _carService.GetAllByCategoryId(categoryId);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }


        [HttpPost("addcars")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("deletecars")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("updatecars")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}