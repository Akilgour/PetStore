using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Endpoints.StockItemEndponts
{
    public class Update : BaseAsyncEndpoint<StockItemUpdate, StockItemUpdate>
    {
        private readonly IStockManager _stockManager;

        public Update(IStockManager stockManager)
        {
            _stockManager = stockManager ?? throw new System.ArgumentNullException(nameof(stockManager));
        }

        [HttpPut("/api/StockItem")]
        public async override Task<ActionResult<StockItemUpdate>> HandleAsync(StockItemUpdate request)
        {
            var result = await _stockManager.Update(request);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
