using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
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