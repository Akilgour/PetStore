using AutoMapper;
using PetStore.Shared.DTO;
using PetStore.Shared.QueMessages;

namespace PetStore.API.Profiles
{
    public class OrderResponseProfile : Profile
    {
        public OrderResponseProfile()
        {
            CreateMap<OrderResponse, StockOrderOrderResponse>();
        }
    }
}