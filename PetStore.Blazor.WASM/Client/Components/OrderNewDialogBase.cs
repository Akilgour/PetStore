using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class OrderNewDialogBase : ComponentBase
    {
        [Parameter] public OrderItemsCreate OrderItem { get; set; } = new OrderItemsCreate();
        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public EventCallback OnConfirm { get; set; }

        protected string ModalDisplay = "none;";
        protected string ModalClass = "";
        protected bool ShowBackdrop = false;

        //public void Show()
        //{
        //    OrderItem = new OrderItemsCreate();
        //    ModalDisplay = "block;";
        //    ModalClass = "Show";
        //    ShowBackdrop = true;
        //  StateHasChanged();
        //}

        //public void Close()
        //{
        //    ModalDisplay = "none";
        //    ModalClass = "";
        //    ShowBackdrop = false;
        //     StateHasChanged();
        //}


    }
}
