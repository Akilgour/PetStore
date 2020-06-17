using System;

namespace RpcClient
{
    public class ScopedConsoleColour : IDisposable
    {
        private ConsoleColor oldColour;

        public ScopedConsoleColour(ConsoleColor newColour)
        {
            this.oldColour = Console.ForegroundColor;
            Console.ForegroundColor = newColour;
        }

        public void Dispose()
        {
            Console.ForegroundColor = this.oldColour;
        }
    }
}