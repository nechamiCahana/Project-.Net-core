using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Interface;
using DAL.Dtos;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _orderStore;

        public OrderController(IOrder orderStore)
        {
            _orderStore = orderStore;
        }

        [HttpPost]
        public ActionResult Post([FromBody] OrderDto newOrder)
        {
            var res = _orderStore.CreateOrder(newOrder);
            if (res != null)
                return Ok();
            return BadRequest("Failed to create booklet");
        }

        [HttpGet]
        public async Task<List<OrderDto>> Get()
        {
            List<OrderDto> result = await _orderStore.GetAllOrder();
            return result;
        }

        [HttpGet("{name}")]
        public async Task<OrderDto> Get([FromBody] string name)
        {
            OrderDto result = await _orderStore.GetOrderByName(name);
            return result;
        }

        [HttpDelete("{id}")]
        public void Delete([FromBody] int id)
        {
            _orderStore.DeleteOrder(id);
        }

    }
}
