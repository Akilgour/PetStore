using System;
using System.Threading.Tasks;

namespace RpcClient
{
    public class Rpc
    {
        private static async Task Main(string[] args)
        {
            Console.Title = "RabbitMQ RPC Client";
            //I stole this from https://gigi.nullneuron.net/gigilabs/abstracting-rabbitmq-rpc-with-taskcompletionsource/

            using (var rpcClient = new RpcClient())
            {
                Console.WriteLine("Press ENTER or Ctrl+C to exit.");

                while (true)
                {
                    string message = null;

                    Console.Write("Enter a message to send: ");
                    using (var colour = new ScopedConsoleColour(ConsoleColor.Blue))
                    {
                        message = Console.ReadLine();
                    }

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        break;
                    }
                    else
                    {
                        var response = await rpcClient.SendAsync(message);

                        Console.Write("Response was: ");
                        using (var colour = new ScopedConsoleColour(ConsoleColor.Green))
                        {
                            Console.WriteLine(response.Success);
                            Console.WriteLine(response.Message);
                        }
                    }
                }
            }
        }
    }
}