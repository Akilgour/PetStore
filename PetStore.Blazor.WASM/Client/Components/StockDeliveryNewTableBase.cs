using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryNewTableBase : ComponentBase
    {
        [Parameter]
        public List<StockDeliveryCreate> StockDeliveryList { get; set; }
        public StockDeliveryCreate StockDelivery { get; set; }

        public string ModalDisplay = "none";

        protected override void OnInitialized()
        {
            StockDelivery = new StockDeliveryCreate();
        }

        public void ShowAddItem()
        {
            StockDelivery = new StockDeliveryCreate();

            if (ModalDisplay == "none")
            {
                ModalDisplay = "block";
            }
            else
            {
                ModalDisplay = "none";
            }
        }

        protected void HandleValidAdd()
        {
            StockDeliveryList.Add(StockDelivery);
            ShowAddItem();
        }

        public void DeleteEvent(string stockDeliveryName)
        {
            StockDeliveryList.Remove(StockDeliveryList.First(x => x.Name == stockDeliveryName));
        }
    }
}