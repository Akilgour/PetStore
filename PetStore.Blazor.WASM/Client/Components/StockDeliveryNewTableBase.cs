using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryNewTableBase : ComponentBase
    {
        [Parameter]
        public List<StockDeliveryCreate> StockDeliveryList { get; set; }

        public StockDeliveryCreate StockDelivery { get; set; }
        public bool AddNew { get; set; }

        protected override void OnInitialized()
        {
            StockDelivery = new StockDeliveryCreate();
            AddNew = false;
        }

        public void ShowAddItem()
        {
            StockDelivery = new StockDeliveryCreate();
            AddNew = true;
        }

        public void Cancel_Click()
        {
            StockDelivery = new StockDeliveryCreate();
            AddNew = false;
        }

        protected void HandleValidAdd()
        {
            StockDeliveryList.Add(StockDelivery);
            AddNew = false;
        }

        public void DeleteEvent(string stockDeliveryName)
        {
            StockDeliveryList.Remove(StockDeliveryList.First(x => x.Name == stockDeliveryName));
        }
    }
}