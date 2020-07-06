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

        public bool AddNew { get; set; }

        protected override void OnInitialized()
        {
            AddNew = false;
        }

        public void ShowAddItem()
        {
            AddNew = true;
        }

        public void AddTestData()
        {
            StockDeliveryList.Add(new StockDeliveryCreate() { Name = "Cat food", Quantity = 1 });
            StockDeliveryList.Add(new StockDeliveryCreate() { Name = "Dog food", Quantity = 2 });
            StockDeliveryList.Add(new StockDeliveryCreate() { Name = "Rat food", Quantity = 3 });
            StockDeliveryList.Add(new StockDeliveryCreate() { Name = "Rabbit food", Quantity = 4 });
        }

        public void CancelEvent()
        {
            AddNew = false;
        }

        protected void SaveEvent(StockDeliveryCreate stockDelivery)
        {
            StockDeliveryList.Add(stockDelivery);
            AddNew = false;
        }

        public void DeleteEvent(string stockDeliveryName)
        {
            StockDeliveryList.Remove(StockDeliveryList.First(x => x.Name == stockDeliveryName));
        }
    }
}