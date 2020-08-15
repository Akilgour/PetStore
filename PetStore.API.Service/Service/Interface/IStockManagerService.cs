using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service.Interface
{
    public interface IStockManagerService
    {
        Task<List<StockItem>> GetAll();
        Task<StockItem> GetById(Guid id);
        Task Update(StockItem stockItem);
    }
}
