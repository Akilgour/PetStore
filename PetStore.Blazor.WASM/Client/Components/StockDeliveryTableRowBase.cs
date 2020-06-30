using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryTableRowBase : ComponentBase
    {
        [Parameter]
        public StockDeliveryCreate StockDelivery { get; set; }
    }
}