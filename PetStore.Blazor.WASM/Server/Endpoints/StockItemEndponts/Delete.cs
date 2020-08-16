using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Endpoints.StockItemEndponts
{
    public class Delete : BaseAsyncEndpoint<Guid, StockItemDeleteResult>
    {
        private readonly IStockManager _stockManager;

        public Delete(IStockManager stockManager)
        {
            _stockManager = stockManager ?? throw new System.ArgumentNullException(nameof(stockManager));
        }

        [HttpDelete("/api/StockItem/{id}")]
        public override async Task<ActionResult<StockItemDeleteResult>> HandleAsync(Guid id)
        {
            var stockItem = await _stockManager.GetById(id);

            if (stockItem is null)
            {
                return NotFound(id);
            }

            await _stockManager.Delete(id);

            // return NoContent(); another option; see https://restfulapi.net/http-methods/#delete
            return Ok(new StockItemDeleteResult { Id = id });
        }
    }
}