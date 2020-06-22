using PetStore.API.Service.Service.Interface;
using PetStore.Domain.Models;
using PetStore.StockDelivery.Client.Client.Interface;
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

        public async Task AddStock(StockItem stockItem)
        {
            await _stockDeliveryClient.Send(stockItem);
        }
    }
}