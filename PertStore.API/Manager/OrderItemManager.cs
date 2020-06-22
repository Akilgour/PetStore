using AutoMapper;
using PetStore.API.Manager.Interface;
using PetStore.API.Service.Service.Interface;
using PetStore.Domain.Models;
using PetStore.Shared.DTO;
using System.Threading.Tasks;

namespace PetStore.API.Manager
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