using AutoMapper;
using PetStore.API.Service.Service.Interface;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager
{
    public class StockDeliveryManager : BaseManager, IStockDeliveryManager
    {
        private readonly IStockDeliveryService _stockDeliveryService;

        public StockDeliveryManager(IStockDeliveryService stockDeliveryService, IMapper mapper)
            : base(mapper)
        {
            _stockDeliveryService = stockDeliveryService ?? throw new System.ArgumentNullException(nameof(stockDeliveryService));
        }

        public async Task AddStock(StockDeliveryCreate stockDeliveryCreate)
        {
            await _stockDeliveryService.AddStock(_mapper.Map<StockItem>(stockDeliveryCreate));
        }
    }
}