using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryTableRowBase : ComponentBase
    {
        [Parameter]
        public StockDeliveryCreate StockDelivery { get; set; }

        [Parameter]
        public EventCallback<string> Delete_Callback { get; set; }

        public async Task Delete_Click(MouseEventArgs e, string stockDeliveryItemName)
        {
            await Delete_Callback.InvokeAsync(stockDeliveryItemName);
        }
    }
}