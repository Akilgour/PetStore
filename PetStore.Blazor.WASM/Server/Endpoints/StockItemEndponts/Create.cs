using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Endpoints.StockItemEndponts
{
    public class Create : BaseAsyncEndpoint<StockItemCreateCommand, StockItemCreateResult>
    {
        private readonly IStockManager _stockManager;

        public Create(IStockManager stockManager)
        {
            _stockManager = stockManager ?? throw new System.ArgumentNullException(nameof(stockManager));
        }

        [HttpPost("/api/StockItem")]
        public override async Task<ActionResult<StockItemCreateResult>> HandleAsync(StockItemCreateCommand request)
        {
            var result = await _stockManager.Add(request);
            return Ok(result);
        }
    }
}