using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockDeliveryNewBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        public StockDeliveryCreate StockDelivery { get; set; }

        public List<StockDeliveryCreate> StockDeliveryList { get; set; }


        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override void OnInitialized()
        {
            StockDelivery = new StockDeliveryCreate();
            StockDeliveryList = new List<StockDeliveryCreate>()
            {
                new StockDeliveryCreate(){ Name = Guid.NewGuid().ToString(), Quantity = 1},
                new StockDeliveryCreate(){ Name = Guid.NewGuid().ToString(), Quantity = 2}
            };
        }

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync($"api/StockDelivery", StockDeliveryList);

            StatusClass = "alert-success";
            Message = "Comment successfully.";
            Saved = true;
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void NavigateBack()
        {
            NavigationManager.NavigateTo("");
        }
    }
}