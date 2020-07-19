using Microsoft.AspNetCore.Components;
using PetStore.Blazor.WASM.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client.Pages
{
    public class StockDisplayBase : ComponentBase, IDisposable
    {
        [Inject]
        public HttpClient Http { get; set; }

        public List<StockItemDisplay> StockItems { get; set; }

        private CancellationTokenSource _pollingCancellationToken;

        protected override void OnParametersSet()
        {
            // If we were already polling for a different order, stop doing so
            _pollingCancellationToken?.Cancel();
            // Start a new poll loop
            PollForUpdates();
        }

        private async void PollForUpdates()
        {
            _pollingCancellationToken = new CancellationTokenSource();
            while (!_pollingCancellationToken.IsCancellationRequested)
            {
                StockItems = await Http.GetJsonAsync<List<StockItemDisplay>>("api/StockItem");
                StateHasChanged();
                await Task.Delay(4000);
            }
        }

        void IDisposable.Dispose()
        {
            _pollingCancellationToken?.Cancel();
        }
    }
}