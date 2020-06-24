using AutoMapper;
using PetStore.Domain.Models;
using PetStore.Shared.DTO;

namespace PetStore.Blazor.WASM.Server.Profiles
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