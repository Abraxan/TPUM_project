using GitBay3.Server.Logic;
using System;

namespace GitBay3.Server
{
    class Program
    {
        private static App application;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Dear Admin!");
            using (application = new App())
            {
                application.Init();
                Console.WriteLine("Press ESC to exit...");
                ConsoleKeyInfo k;
                bool bln = true;
                while (true)
                {
                    while (!Console.KeyAvailable)
                    {
                        if (!bln)
                        {
                            Console.WriteLine("{0:s}", "x");
                            Console.WriteLine("Server still alive. " + DateTime.UtcNow.ToString("G"));
                        }
                        bln = true;
                    }
                    bln = false;

                    k = Console.ReadKey(true);
                    if (k.Key == ConsoleKey.Escape)
                    {
                        break;
                    }

                    Console.Write("{0} --- ", k.Key);
                }
                Console.WriteLine("Server closed at " + DateTime.UtcNow.ToString("G"));
                
            }
        }
    }
}
