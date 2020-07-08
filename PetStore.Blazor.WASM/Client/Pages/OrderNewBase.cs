using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class OrderNewBase : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        public List<StockItemDisplay> StockItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            StockItems = await Http.GetJsonAsync<List<StockItemDisplay>>("api/Stock");
        }
    }
}
