using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class OrderNewDialogBase : ComponentBase
    {
        [Parameter] public OrderItemsCreate OrderItem { get; set; } = new OrderItemsCreate();
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public EventCallback OnConfirm { get; set; }

        protected readonly int MinimumQuantity = 1;
        protected readonly int MaximumQuantity = 50;
    }
}