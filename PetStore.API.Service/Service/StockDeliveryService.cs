using PetStore.API.Service.Service.Interface;
using PetStore.Domain.Models;
using System;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service
{
    public class StockDeliveryService : IStockDeliveryService
    {
        public StockDeliveryService()
        {
        }

        public Task AddStock(StockItem stockItem)
        {
            throw new NotImplementedException();
        }
    }
}