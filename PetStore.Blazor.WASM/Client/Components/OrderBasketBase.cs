using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class OrderBasketBase : ComponentBase
    {
        [Parameter]
        public StockOrderCreate StockOrder { get; set; } = new StockOrderCreate();

        [Parameter]
        public EventCallback<OrderItemsCreate> OnRemoved { get; set; }

        public async Task OnRemovedClick(OrderItemsCreate orderItemsCreate)
        {
            await OnRemoved.InvokeAsync(orderItemsCreate);
        }
    }
}