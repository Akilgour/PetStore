using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class OrderNewBase : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }
        protected StockItemDisplay SelectedStock;

        public List<StockItemDisplay> StockItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            StockItems = await Http.GetJsonAsync<List<StockItemDisplay>>("api/Stock");
        }

        protected async Task<IEnumerable<StockItemDisplay>> SearchStock(string searchText)
        {
            return await Task.FromResult(StockItems.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
        }
    }
}
