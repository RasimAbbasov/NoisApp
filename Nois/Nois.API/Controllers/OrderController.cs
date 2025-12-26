using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.OrderDtos;
using Nois.Application.Interfaces;
using Nois.Application.Services;

namespace Nois.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("admin/orders")]
        //[Authorize(Roles = "Admin")] // Critical: Restrict to Admins
        public async Task<ActionResult<List<OrderAdminDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost("checkout/{buyerId}")]
        public async Task<ActionResult<OrderDto>> Checkout(string buyerId)
        {
            // All complex stock validation, payment simulation, and basket clearing 
            // happens within the service layer.
            var orderDto = await _orderService.CreateOrderAsync(buyerId);

            // Return HTTP 201 Created status, pointing to the new resource location
            return Ok(new { message = "Ordered successfully" });

            //return CreatedAtAction(nameof(GetOrder), new { id = orderDto.Id }, orderDto);
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<OrderDto>> GetOrder(int id)
        //{
        //    var orderDto = await _orderService.GetOrderByIdAsync(id);
        //    if (orderDto == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(orderDto);
        //}
        //[HttpGet("history/{buyerId}")]
        //public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrderHistory(string buyerId)
        //{
        //    var history = await _orderService.GetCustomerOrderHistoryAsync(buyerId);
        //    return Ok(history);
        //}
    }
}
