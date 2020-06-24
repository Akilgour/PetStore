using AutoMapper;
using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Server.Models;
using PetStore.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockDeliveryCreateBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        public StockDeliveryNew StockDelivery { get; set; }

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override void OnInitialized()
        {
            StockDelivery = new StockDeliveryNew();
        }

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync($"api/StockDelivery", Mapper.Map<StockDeliveryCreate>(StockDelivery));

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