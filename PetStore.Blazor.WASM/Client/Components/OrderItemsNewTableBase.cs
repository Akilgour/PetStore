using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class OrderItemsNewTableBase : ComponentBase
    {
        [Parameter]
        public List<OrderItemsCreate> OrderItems { get; set; }
    }
}
