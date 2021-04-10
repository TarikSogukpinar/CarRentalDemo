using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addcustomer")]
        public IActionResult AddCustomer(Customer customer)
        {
            var result = _customerService.Add(customer);
            return result.Success ? (IActionResult) Ok(result) : BadRequest(result);
        }

        [HttpPut("updatecustomer")]
        public IActionResult UpdateCustomer(Customer customer)
        {
            var result = _customerService.Update(customer);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
     
        }

        [HttpPost("deletecustomer")]
        public IActionResult DeleteCustomer(Customer customer)
        {
            var result = _customerService.Delete(customer);
            return result.Success ? (IActionResult) Ok(result) : BadRequest(result);
        }

        [HttpGet("getcustomerdetails")]
        public IActionResult GetCustomerDetails()
        {
            var result = _customerService.GetCustomerDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcustomerdetailbyid")]
        public IActionResult GetCustomerDetailById(int customerId)
        {
            var result = _customerService.GetCustomerDetailById(customerId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcustomerbyemail")]
        public IActionResult GetCustomerByEmail(string email)
        {
            var result = _customerService.GetCustomerByEmail(email);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
            
        }
        
        [HttpGet("getcustomerbycompany")]
        public IActionResult GetCustomerByCompany(string companyName)
        {
            var result = _customerService.GetCustomerByCompanyName(companyName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
            
        }
    }
}