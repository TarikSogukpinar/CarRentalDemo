using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getallrentals")]
        public IActionResult GetAllRentals()
        {
            var result = _rentalService.GetAllRentals();
            if (result.Success) return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpGet("getallrentals")]
        public IActionResult GetByRentId(int id)
        {
            var result = _rentalService.GetByRentId(id);
            if (result.Success) return Ok(result.Data);

            return BadRequest(result);
        }

        [HttpPost("addrentals")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("deleterentals")]
        public IActionResult Delete(Rental rental)
        {
            var result = _rentalService.Delete(rental);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("updaterentals")]
        public IActionResult Update(Rental rental)
        {
            var result = _rentalService.Update(rental);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}