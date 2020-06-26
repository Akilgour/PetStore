using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Client.Components;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class OrderItemCreateBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        public StockOrderNew StockOrder { get; set; }

        //used to store state of screen
        protected string Message = string.Empty;

        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override void OnInitialized()
        {
            StockOrder = new StockOrderNew();
        }

        protected async Task HandleValidSubmit()
        {
            var response = await Http.PostAsJsonAsync($"api/OrderItem", Mapper.Map<StockOrderCreate>(StockOrder));
            var returnValue = await response.Content.ReadAsAsync<StockOrderOrderResponse>();

            if (returnValue.Success)
            {
                StatusClass = "alert-success";
                Message = returnValue.Message;
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = returnValue.Message;
            }
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

        protected AddOrderItemsNewDialog OrderItemsNewDialog { get; set; }

        public void AddOrderItem(MouseEventArgs e)
        {
            OrderItemsNewDialog.Show();
        }

        public async Task AddOrderItemsNewDialog_OnDialogClose()
        {
            StockOrder.OrderItems.Add(OrderItemsNewDialog.OrderItem);
        }
    }
}