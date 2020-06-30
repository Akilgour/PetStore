using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryNewTableBase : ComponentBase
    {
        [Parameter]
        public List<StockDeliveryCreate> StockDeliveryList { get; set; }
        protected bool ShowAdd { get; private set; }

        protected override void OnInitialized()
        {
            ShowAdd = false;
        }

        public void ShowAddItem(MouseEventArgs e)
        {
            ShowAdd = true;
        }

        public void HideAddItem(MouseEventArgs e)
        {
            ShowAdd = false;
        }
    }
}