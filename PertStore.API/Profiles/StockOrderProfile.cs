using AutoMapper;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Domain.Models;

namespace PetStore.API.Profiles
{
    public class StockOrderProfile : Profile
    {
        public StockOrderProfile()
        {
            CreateMap<StockOrderCreate, StockOrder>();
            CreateMap<OrderItemsCreate, StockItem>();
        }
    }
}