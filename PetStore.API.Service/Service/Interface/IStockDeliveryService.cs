using PetStore.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service.Interface
{
    public interface IStockDeliveryService
    {
        Task AddStock(List<StockItem> stockItems);
    }
}