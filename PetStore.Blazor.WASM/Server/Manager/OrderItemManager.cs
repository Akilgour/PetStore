using AutoMapper;
using PetStore.API.Service.Service.Interface;
using PetStore.Blazor.WASM.Server.Manager.Interface;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;

using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Server.Manager
{
    public class OrderItemManager : BaseManager, IOrderItemManager
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemManager(IOrderItemService stockDeliveryService, IMapper mapper)
            : base(mapper)
        {
            _orderItemService = stockDeliveryService;
        }

        public async Task<StockOrderOrderResponse> OrderCreate(StockOrderCreate stockOrderCreate)
        {
            return _mapper.Map<StockOrderOrderResponse>(await _orderItemService.OrderCreate(_mapper.Map<StockOrder>(stockOrderCreate)));
        }
    }
}