using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemManager _orderItemManager;

        public OrderItemController(IOrderItemManager orderItemManager)
        {
            _orderItemManager = orderItemManager ?? throw new System.ArgumentNullException(nameof(orderItemManager));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post(StockOrderCreate stockOrderCreate)
        {
            var result = await _orderItemManager.OrderCreate(stockOrderCreate);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}