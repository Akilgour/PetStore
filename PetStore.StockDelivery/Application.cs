
using PetStore.StockDelivery.Client.Client.Interface;
using System;
using System.Diagnostics;

namespace PetStore.StockDelivery
{
    public class Application
    {
        private readonly IStockDeliveryClient _stockDeliveryClient;


        public Application(IStockDeliveryClient stockDeliveryClient)
        {
            _stockDeliveryClient = stockDeliveryClient;
        }

        public void Run(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            try
            {
                _stockDeliveryClient.Receive();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                timer.Stop();
                Console.WriteLine($"Run time: {timer.Elapsed.ToString()}");
            }
        }

    }
}
