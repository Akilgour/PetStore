using System.Collections.Generic;

namespace PetStore.Blazor.WASM.Shared.Models
{
    public class StockOrderCreate
    {
        public string OrderNumber { get; set; }
        public List<OrderItemsCreate> OrderItems { get; set; } = new List<OrderItemsCreate>();

        public int TotalPrice { get { return 5; } }
    }
}
