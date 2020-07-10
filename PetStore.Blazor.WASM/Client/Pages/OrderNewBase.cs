using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Client.Helpers;
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
        public List<StockItemDisplay> SelectedStockItems { get; set; }
        public string SearchText { get; set; }
        public OrderItemsCreate OrderItemsCreate { get; set; }
        public bool ShowingDialog { get; set; } = false;
        public StockOrderCreate StockOrder { get; set; } = new StockOrderCreate();

        protected override async Task OnInitializedAsync()
        {
            StockItems = await Http.GetJsonAsync<List<StockItemDisplay>>("api/Stock");
            SelectedStockItems = StockItems;
        }

        protected async Task<IEnumerable<StockItemDisplay>> SearchStock(string searchText)
        {
            SelectedStockItems = await Task.FromResult(StockItems.Where(x => x.Name.ToLower().Contains(searchText.ToLower())).ToList());
            return SelectedStockItems;
        }

        protected async Task Search_Click()
        {
            SelectedStockItems = string.IsNullOrWhiteSpace(SearchText) ? StockItems : await Task.FromResult(StockItems.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())).ToList());
        }

        public void ShowDialog(StockItemDisplay stockItem)
        {
            OrderItemsCreate = new OrderItemsCreate()
            {
                Name = stockItem.Name,
                CostInPounds = stockItem.CostInPounds,
                Quantity = 1
            };
            ShowingDialog = true;
        }

        public void CancelOrderItemDialog()
        {
            OrderItemsCreate = null;
            ShowingDialog = false;
        }

        public void ConfirmOrderItemDialog()
        {
            CalculateTotalCostInPounds.Resolve(OrderItemsCreate);
            StockOrder.OrderItems.Add(OrderItemsCreate);
            StockOrderCreateCalculateTotalCostInPounds.Resolve(StockOrder);
            OrderItemsCreate = null;
            ShowingDialog = false;
        }

        public void OnRemoved(OrderItemsCreate orderItemsCreate)
        {
            StockOrder.OrderItems.Remove(orderItemsCreate);
            StockOrderCreateCalculateTotalCostInPounds.Resolve(StockOrder);
        }
    }
}