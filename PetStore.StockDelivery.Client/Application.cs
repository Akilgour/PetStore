//using PetStore.StockDelivery.Client.Client.Interface;
//using System;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace PetStore.StockDelivery.Client
//{
//    public class Application
//    {
//        private readonly IStockDeliveryClient _stockDeliveryClient;

//        public Application(IStockDeliveryClient stockDeliveryClient)
//        {
//            _stockDeliveryClient = stockDeliveryClient;
//        }

//        public async Task Run(string[] args)
//        {
//            var timer = new Stopwatch();
//            timer.Start();
//            try
//            {
//                await _stockDeliveryClient.Receive();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex);
//            }
//            finally
//            {
//                timer.Stop();
//                Console.WriteLine($"Run time: {timer.Elapsed.ToString()}");
//            }
//        }

//    }
//}
