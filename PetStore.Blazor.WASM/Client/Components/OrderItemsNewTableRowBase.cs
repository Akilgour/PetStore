using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class OrderItemsNewTableRowBase : ComponentBase
    {
        [Parameter]
        public OrderItemsCreate OrderItem { get; set; }
    }
}
