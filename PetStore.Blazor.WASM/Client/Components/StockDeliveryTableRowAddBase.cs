using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryTableRowAddBase : ComponentBase
    {
        [Parameter]
        public EventCallback<StockDeliveryCreate> Save_Callback { get; set; }

        [Parameter]
        public EventCallback<bool> Cancel_Callback { get; set; }

        public StockDeliveryCreate StockDelivery { get; set; }

        protected override void OnInitialized()
        {
            StockDelivery = new StockDeliveryCreate();
        }

        public async Task Save_Click()
        {
            await Save_Callback.InvokeAsync(StockDelivery);
        }

        public async Task Cancel_Click()
        {
            await Cancel_Callback.InvokeAsync(true);
        }

    }
}