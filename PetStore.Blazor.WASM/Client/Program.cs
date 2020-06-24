using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using PetStore.Blazor.WASM.Client.Profiles;

namespace PetStore.Blazor.WASM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StockDeliveryCreateProfile>();
            });
            builder.Services.AddSingleton(configuration.CreateMapper());

            await builder.Build().RunAsync();
        }
    }
}
