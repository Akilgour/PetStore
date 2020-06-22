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
        private readonly IOrderItemService _stockDeliveryService;

        public OrderItemManager(IOrderItemService stockDeliveryService, IMapper mapper)
            : base(mapper)
        {
            _stockDeliveryService = stockDeliveryService;
        }

        public async Task OrderCreate(StockOrderCreate stockOrderCreate)
        {
            await _stockDeliveryService.OrderCreate(_mapper.Map<StockOrder>(stockOrderCreate));
        }
    }
}