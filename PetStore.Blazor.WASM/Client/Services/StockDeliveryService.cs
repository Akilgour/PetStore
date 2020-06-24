//using PetStore.Blazor.WASM.Client.Services.Interface;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace PetStore.Blazor.WASM.Client.Services
//{
//    public class StockDeliveryService :  IStockDeliveryService
//    {
//        private readonly HttpClient _http;

//        public StockDeliveryService(HttpClient http)
//        {
//            _http = http;
//        }

//        public async Task Add(PetStore.Shared.DTO.StockDeliveryCreate StockDeliveryCreate)
//        {
//           // forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
//             await _http.PostAsJsonAsync($"api/DetailLogComment/{detailLogId}", detailLogCommentCreate);
//        }
//    }
//}
