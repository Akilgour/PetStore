using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockManager _stockManager;

        public StockController(IStockManager stockManager)
        {
            _stockManager = stockManager ?? throw new System.ArgumentNullException(nameof(stockManager));
        }

        [HttpPost]
        public async Task<ActionResult> GetAll()
        {
            var result = await _stockManager.GetAll();
            return Ok(result);
        }
    }
}