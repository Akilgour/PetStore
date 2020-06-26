using Microsoft.AspNetCore.Mvc;
using PetStore.API.Manager.Interface;
using PetStore.Blazor.WASM.Client.Pages;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetStore.API.Controllers
{
    [Route("api/[controller]")]
    public class StockDeliveryController : Controller
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