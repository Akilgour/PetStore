using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockItemEditBase : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Parameter]
        public string StockItemId { get; set; }

        public StockItemUpdate StockItem { get; set; }

        protected async override Task OnInitializedAsync()
        {
            StockItem = await Http.GetJsonAsync<StockItemUpdate>($"api/StockItem/{StockItemId}");
        }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;

        protected async Task HandleValidSubmit()
        {
            await Http.PutAsJsonAsync($"api/StockItem", StockItem);
            StatusClass = "alert-success";
            Message = "Comment successfully.";
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }
    }
}