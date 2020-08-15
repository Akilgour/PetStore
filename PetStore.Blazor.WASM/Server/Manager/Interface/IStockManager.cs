using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager.Interface
{
    public interface IStockManager
    {
        Task<List<StockItemDisplay>> GetAll();
        Task<StockItemUpdate> GetById(Guid id);
        Task<StockItemUpdate> Update(StockItemUpdate request);
    }
}