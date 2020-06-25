using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Client.Components;
using PetStore.Blazor.WASM.Shared.Models;
using PetStore.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
            StockOrder  = new StockOrderNew();
        }

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync($"api/OrderItem", Mapper.Map<StockOrderCreate>(StockOrder));
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
