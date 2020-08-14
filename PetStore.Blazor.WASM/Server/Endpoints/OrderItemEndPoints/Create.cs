using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Endpoints.OrderItemEndPoints
{
    public class Create : BaseAsyncEndpoint
    {
        private readonly IOrderItemManager _orderItemManager;

        public Create(IOrderItemManager orderItemManager)
        {
            _orderItemManager = orderItemManager ?? throw new System.ArgumentNullException(nameof(orderItemManager));
        }

        [HttpPost("api/OrderItem")]
        public async Task<ActionResult> HandleAsync(StockOrderCreate stockOrderCreate)
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