using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;

namespace PetStore.API.Profiles
{
    public class StockItemProfile : Profile
    {
        public StockItemProfile()
        {
            CreateMap<StockDeliveryCreate, StockItem>();
        }
    }
}