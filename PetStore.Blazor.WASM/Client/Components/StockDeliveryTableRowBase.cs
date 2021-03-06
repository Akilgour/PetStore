﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PetStore.Blazor.WASM.Shared.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class StockDeliveryTableRowBase : ComponentBase
    {
        public bool Display = true;

        [Parameter]
        public StockDeliveryCreate StockDelivery { get; set; }

        [Parameter]
        public EventCallback<string> Delete_Callback { get; set; }

        public async Task Delete_Click(MouseEventArgs e, string stockDeliveryItemName)
        {
            await Delete_Callback.InvokeAsync(stockDeliveryItemName);
        }

        public void Edit_Click()
        {
            StockDelivery.BeginEdit();
            Display = !Display;
        }

        public void Save_Click()
        {
            if (StockDelivery.IsValid())
            {
                StockDelivery.EndEdit();
                Display = !Display;
            }
        }

        public void Cancel_Click()
        {
            StockDelivery.CancelEdit();
            Display = !Display;
        }
    }
}