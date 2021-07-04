using System;
using System.Threading;

namespace Leder_Efter_Server
{
    class Program
    {
        private static bool isRunning;

        static void Main(string[] args)
        {
            Console.Title = "Leder-Efter-Server";
            isRunning = true;

            Thread mainThread = new Thread(MainThread);
            mainThread.Start();

            Server.Start(30, 26950);
        } 

        private static void MainThread()
        {
            Console.WriteLine("[LEDER EFTER SERVER]");
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
