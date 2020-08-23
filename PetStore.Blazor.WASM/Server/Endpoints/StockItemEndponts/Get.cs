using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Endpoints.StockItemEndponts
{
    public class Get : BaseAsyncEndpoint<Guid, StockItemUpdate>
    {
        private readonly IStockManager _stockManager;

        public Get(IStockManager stockManager)
        {
            _stockManager = stockManager ?? throw new System.ArgumentNullException(nameof(stockManager));
        }

        [HttpGet("/api/StockItem/{id}")]
        public override async Task<ActionResult<StockItemUpdate>> HandleAsync(Guid id)
        {
            var stockItemEdit = await _stockManager.GetById(id);
            if (stockItemEdit == null)
            {
                return NotFound();
            }
            return Ok(stockItemEdit);
        }
    }
}