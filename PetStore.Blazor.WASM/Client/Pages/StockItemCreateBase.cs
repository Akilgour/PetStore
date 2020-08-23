using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockItemCreateBase : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;

        public StockItemCreateCommand StockItem { get; set; } = new StockItemCreateCommand();

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync($"api/StockItem", StockItem);
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