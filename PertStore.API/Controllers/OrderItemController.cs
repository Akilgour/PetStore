using Microsoft.AspNetCore.Mvc;
using PetStore.API.Manager.Interface;
using PetStore.Shared.DTO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetStore.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderItemController : Controller
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
            stockOrderCreate.OrderItems.Add(new OrderItemsCreate() { Name = "a1", Quantity = 100 });

            var result = await _orderItemManager.OrderCreate(stockOrderCreate);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Message);

            }
        }
    }
}
