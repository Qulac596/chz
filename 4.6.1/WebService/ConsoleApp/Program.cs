using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new WinService.Service();

            s.Start();
            Console.ReadLine();
            s.Stop();
        }
    }
}
