using PetStore.API.Service.Service.Interface;
using PetStore.Domain.Models;
using PetStore.StockDelivery.Client.Client.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStore.API.Service.Service
{
    public class StockDeliveryService : IStockDeliveryService
    {
        private readonly IStockDeliveryClient _stockDeliveryClient;

        public StockDeliveryService(IStockDeliveryClient stockDeliveryClient)
        {
            _stockDeliveryClient = stockDeliveryClient;
        }

        public async Task AddStock(List<StockItem> stockItems)
        {
            foreach (var item in stockItems)
            {
                await _stockDeliveryClient.Send(item);
            }
        }
    }
}