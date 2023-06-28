using Microsoft.AspNetCore.Mvc;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EcomAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _ICustomer;

        public CustomerController(ICustomer iCustomer)
        {
            _ICustomer = iCustomer;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _ICustomer.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{customerID}", Name = "CustomerById")]
        public async Task<IActionResult> GetCustomer(long customerID)
        {
            try
            {
                var customer = await _ICustomer.GetCustomer(customerID);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCustomer(CustomerForCreationDto customer)
        {
            try
            {
                var createdCustomer = await _ICustomer.CreateCustomer(customer);
                return CreatedAtRoute("CustomerById", new { customerID = createdCustomer.CustomerID }, createdCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{customerID}")]
        public async Task<IActionResult> UpdateCustomer(CustomerForUpdateDto customer)
        {
            try
            {
                var dbCustomer = await _ICustomer.GetCustomer(customer.CustomerID);
                if (dbCustomer == null)
                    return NotFound();

                await _ICustomer.UpdateCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{customerID}")]
        public async Task<IActionResult> DeleteCustomer(long customerID)
        {
            try
            {
                var dbCustomer = await _ICustomer.GetCustomer(customerID);
                if (dbCustomer == null)
                    return NotFound();

                await _ICustomer.DeleteCustomer(customerID);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}