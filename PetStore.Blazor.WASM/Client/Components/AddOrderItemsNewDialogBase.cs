﻿using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class AddOrderItemsNewDialogBase : ComponentBase
    {
        protected string ModalDisplay = "none;";
        protected string ModalClass = "";
        protected bool ShowBackdrop = false;
        public OrderItemsCreate OrderItem { get; set; } = new OrderItemsCreate();

        [Parameter]
        public EventCallback<bool> AddEventCallback { get; set; }

        public void Show()
        {
            OrderItem = new OrderItemsCreate();
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();
        }

        public void Close()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }

        protected async Task HandleValidAdd()
        {
            await AddEventCallback.InvokeAsync(true);
            Close();
            StateHasChanged();
        }
    }
}