using AutoMapper;
using PetStore.Blazor.WASM.Server.Models;
using PetStore.Shared.DTO;

namespace PetStore.Blazor.WASM.Client.Profiles
{
    public class StockDeliveryCreateProfile : Profile
    {
        public StockDeliveryCreateProfile()
        {
            CreateMap<StockDeliveryNew, StockDeliveryCreate>();
        }
    }
}