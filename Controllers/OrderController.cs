using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _order;

        public OrderController(OrderService orderService)
        {
            _order = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrdersModel>>> GetAll() =>
            Ok(await _order.GetAllAsync());

        [HttpPost]
        public async Task<ActionResult<OrdersModel>> Create(OrdersModel dto)
        {
            await _order.CreateAsync(dto);
            return Ok(dto);
        }
    }
}
