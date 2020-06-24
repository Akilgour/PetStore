using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Components
{
    public class OrderItemsNewTableRowBase : ComponentBase
    {
        [Parameter]
        public  OrderItemsNew OrderItem { get; set; }
    }
}
