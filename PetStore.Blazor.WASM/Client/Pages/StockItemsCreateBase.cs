using AutoMapper;
using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Server.Models;
using PetStore.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockItemsCreateBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }  
        
        [Inject]
        public HttpClient Http { get; set; }

        public StockItemsNew StockItem { get; set; }

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(2000);
            StockItem = new StockItemsNew();
        }

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync($"api/StockDelivery", Mapper.Map<StockDeliveryCreate>(StockItem));

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