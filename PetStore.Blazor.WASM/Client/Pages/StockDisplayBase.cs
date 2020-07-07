using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockDisplayBase : ComponentBase, IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        public List<StockItemDisplay> StockItems { get; set; }

        private bool _invalidOrder;
        private CancellationTokenSource pollingCancellationToken;

        protected override void OnParametersSet()
        {
            // If we were already polling for a different order, stop doing so
            pollingCancellationToken?.Cancel();

            // Start a new poll loop
            PollForUpdates();
        }

        private async void PollForUpdates()
        {
            _invalidOrder = false;
            pollingCancellationToken = new CancellationTokenSource();
            while (!pollingCancellationToken.IsCancellationRequested)
            {
                var result = await Http.GetJsonAsync<IEnumerable<StockItemDisplay>>("api/Stock");
                StockItems = result.ToList();
                StateHasChanged();

                await Task.Delay(4000);
            }
        }

        void IDisposable.Dispose()
        {
            pollingCancellationToken?.Cancel();
        }
    }
}