using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Endpoints.StockItemEndponts
{
    public class List : BaseAsyncEndpoint
    {
        private readonly IStockManager _stockManager;

        public List(IStockManager stockManager)
        {
            _stockManager = stockManager ?? throw new System.ArgumentNullException(nameof(stockManager));
        }

        [HttpGet("api/StockItem")]
        public async Task<ActionResult<StockItemDisplay>> HandleAsync()
        {
            var result = await _stockManager.GetAll();
            return Ok(result);
        }
    }
}