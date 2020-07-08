using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;

namespace PetStore.Blazor.WASM.Server.Profiles
{
    public class StockItemDisplayProfile : Profile
    {
        public StockItemDisplayProfile()
        {
            CreateMap<StockItem, StockItemDisplay>();
        }
    }
}