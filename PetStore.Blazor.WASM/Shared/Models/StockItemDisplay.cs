using System;

namespace PetStore.Blazor.WASM.Shared.Models
{
    public class StockItemDisplay
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
        public decimal CostInPounds { get; set; }
    }
}