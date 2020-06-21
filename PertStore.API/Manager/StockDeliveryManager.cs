using PetStore.API.Manager.Interface;
using PetStore.API.Service.Service.Interface;
using PetStore.Shared.DTO;
using System.Threading.Tasks;

namespace PetStore.API.Manager
{
    public class StockDeliveryManager : IStockDeliveryManager
    {
        private readonly IStockDeliveryService _stockDeliveryService;

        public StockDeliveryManager(IStockDeliveryService stockDeliveryService)
        {
            _stockDeliveryService = stockDeliveryService ?? throw new System.ArgumentNullException(nameof(stockDeliveryService));
        }

        public async Task AddStock(StockDeliveryCreate stockDeliveryCreate)
        {
            await _stockDeliveryService.AddStock();
        }
    }
}