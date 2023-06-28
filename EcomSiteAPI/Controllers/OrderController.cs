using Microsoft.AspNetCore.Mvc;
using EcomAPI.Dto;
using EcomAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EcomAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _IOrder;

        public OrderController(IOrder iOrder)
        {
            _IOrder = iOrder;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var order = await _IOrder.GetOrders();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{orderID}", Name = "OrderById")]
        public async Task<IActionResult> GetOrder(long orderID)
        {
            try
            {
                var order = await _IOrder.GetOrder(orderID);
                if (order == null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderForCreationDto order)
        {
            try
            {
                var createdOrder = await _IOrder.CreateOrder(order);
                return CreatedAtRoute("OrderById", new { orderID = createdOrder.OrderID }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{orderID}")]
        public async Task<IActionResult> UpdateOrder(OrderForUpdateDto order)
        {
            try
            {
                var dbOrder = await _IOrder.GetOrder(order.OrderID);
                if (dbOrder == null)
                    return NotFound();

                await _IOrder.UpdateOrder(order);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{orderID}")]
        public async Task<IActionResult> DeleteOrder(long orderID)
        {
            try
            {
                var dbOrder = await _IOrder.GetOrder(orderID);
                if (dbOrder == null)
                    return NotFound();

                await _IOrder.DeleteOrder(orderID);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}