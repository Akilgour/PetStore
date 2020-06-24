using System.Collections.Generic;

namespace PetStore.Blazor.WASM.Shared.Models
{
    public class StockOrderNew
    {
        public string OrderNumber { get; set; }
        public List<OrderItemsNew> OrderItems { get; set; }
    }
}