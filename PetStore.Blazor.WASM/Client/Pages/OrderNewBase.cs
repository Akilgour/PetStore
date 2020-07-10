using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Client.State;
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

        [Inject]
        public OrderNewState OrderNewState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            OrderNewState.StockItems = await Http.GetJsonAsync<List<StockItemDisplay>>("api/Stock");
            OrderNewState.SelectedStockItems = OrderNewState.StockItems;
        }
    }
}