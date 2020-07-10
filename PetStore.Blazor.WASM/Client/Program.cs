using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PetStore.Blazor.WASM.Client.State;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStore.Blazor.WASM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<OrderNewState>();

            await builder.Build().RunAsync();
        }
    }
}
