using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
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