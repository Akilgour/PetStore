using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;

namespace PetStore.Blazor.WASM.Server.Profiles
{
    public class StockItemCreate : Profile
    {
        public StockItemCreate()
        {
            CreateMap<StockItemCreateCommand, StockItem>();
            CreateMap<StockItem, StockItemCreateResult>();
        }
    }
}