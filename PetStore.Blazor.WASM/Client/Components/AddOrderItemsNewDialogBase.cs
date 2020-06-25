using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class AddOrderItemsNewDialogBase : ComponentBase
    {
        public Guid Guid = Guid.NewGuid();
        public string ModalDisplay = "none;";
        public string ModalClass = "";
        public bool ShowBackdrop = false;



        public void Show()
        {
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

        protected OrderItemsNew OrderItem { get; set; } = new OrderItemsNew() { Name = "asdf", Quantity = 2 };

        protected void HandleValidAdd()
        {
            Close();
            StateHasChanged();
        }
    }
}
