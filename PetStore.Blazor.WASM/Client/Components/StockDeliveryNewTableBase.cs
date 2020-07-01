using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryNewTableBase : ComponentBase
    {
        [Parameter]
        public List<StockDeliveryCreate> StockDeliveryList { get; set; }
        public StockDeliveryCreate StockDelivery { get; set; }

        public string ModalDisplay = "none";

        //public bool ShowAdd { get;  set; }

        protected override void OnInitialized()
        {
            StockDelivery = new StockDeliveryCreate();       
        }

        //protected StockDeliveryTableRowAdd StockDeliveryTableRow { get; set; }

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
    }
}