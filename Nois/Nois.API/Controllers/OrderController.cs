using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nois.Application.DTOs.OrderDtos;
using Nois.Application.Interfaces;
using Nois.Application.Services;
using Nois.Domain.Common;

namespace Nois.API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
		private readonly ILogger<OrderController> _logger;

		public OrderController(IOrderService orderService,ILogger<OrderController> logger)
        {
            _orderService = orderService;
			_logger = logger;
        }

		[HttpGet("admin/orders")]
		public async Task<IActionResult> GetPaged([FromQuery] PaginationRequest request)
		{
			var result = await _orderService.GetPagedAsync(request);
			_logger.LogInformation("Orders GetPaged endpoint called at {Time}", DateTime.Now);
			return Ok(result);
		}

		[HttpGet("admin/orders/all")]
        //[Authorize(Roles = "Admin")] // Critical: Restrict to Admins
        public async Task<ActionResult<List<OrderAdminDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
			_logger.LogInformation("Orders GetAll endpoint called at {Time}", DateTime.Now);
			return Ok(orders);
        }

		[HttpGet("admin/{userId}/orders")]
		//[Authorize(Roles = "Admin")] // Critical: Restrict to Admins
		public async Task<ActionResult<List<OrderAdminDto>>> GetOrders(string userId)
		{
			var orders = await _orderService.GetOrderByUserAsync(userId);
			_logger.LogInformation("Orders Get endpoint called at {Time}", DateTime.Now);
			return Ok(orders);
		}

		[HttpPost("checkout")]
        public async Task<ActionResult<OrderDto>> Checkout(CreateOrderRequestDto requestDto)
        {
            // All complex stock validation, payment simulation, and basket clearing 
            // happens within the service layer.
            var orderDto = await _orderService.CreateOrderAsync(requestDto);

            // Return HTTP 201 Created status, pointing to the new resource location
            return Ok(new { message = "Ordered successfully" });
			//return Ok(orderDto);
		}
		//return CreatedAtAction(nameof(GetOrder), new { id = orderDto.Id }, orderDto);
	}


		//Latest
		//[HttpPost("checkout/{buyerId}")]
		//public async Task<ActionResult<OrderDto>> Checkout(string buyerId)
		//{
		//	// All complex stock validation, payment simulation, and basket clearing 
		//	// happens within the service layer.
		//	var orderDto = await _orderService.CreateOrderAsync(buyerId);

		//	// Return HTTP 201 Created status, pointing to the new resource location
		//	return Ok(new { message = "Ordered successfully" });

		//	//return CreatedAtAction(nameof(GetOrder), new { id = orderDto.Id }, orderDto);
		//}


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
