using AutoMapper;
using PetStore.Shared.DTO;
using PetStore.Shared.QueMessages;

namespace PetStore.Blazor.WASM.Server.Profiles
{
    public class OrderResponseProfile : Profile
    {
        public OrderResponseProfile()
        {
            CreateMap<OrderResponse, StockOrderOrderResponse>();
        }
    }
}