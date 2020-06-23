using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Shared.DTO;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockDeliveryController : ControllerBase
    {
        private readonly IStockDeliveryManager _stockDeliveryManager;

        public StockDeliveryController(IStockDeliveryManager stockDeliveryManager)
        {
            _stockDeliveryManager = stockDeliveryManager ?? throw new System.ArgumentNullException(nameof(stockDeliveryManager));
        }

        [HttpPost]
        public async Task<ActionResult> Post(StockDeliveryCreate stockDeliveryCreate)
        {
            await _stockDeliveryManager.AddStock(stockDeliveryCreate);
            return Ok();
        }
    }
}