using Microwave.Controller.Timers;
using System;
using System.Threading;

namespace MicrowaveControllerConsole
{
    class Program
    {
        private static DateTime startTime;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            startTime = DateTime.Now;

            MicrowaveTimer timer = new MicrowaveTimer();
            timer.TimerReachedZero += Timer_TimerReachedZero;
            timer.AddTime(new TimeSpan(0, 0, 5));

            Thread.Sleep(200);
            timer.AddTime(new TimeSpan(0, 0, 4));
            timer.Reset();

            Console.ReadKey();
        }

        private static void Timer_TimerReachedZero()
        {
            Console.WriteLine($"Time elapsed {DateTime.Now - startTime}");
        }
    }
}
