using Microwave.Controller;
using Microwave.Controller.Timers;
using Microwave.Hardware.Fakes;
using System;
using System.Threading;

namespace MicrowaveControllerConsole
{
    /// <summary>
    /// class used for debugging.
    /// </summary>
    class Program
    {
        private static DateTime startTime;

        static void Main(string[] args)
        {
            TestController();
            TestTimer();

            Console.ReadKey();
        }

        private static void TestTimer()
        {
            startTime = DateTime.Now;

            MicrowaveTimer timer = new MicrowaveTimer();
            timer.TimerReachedZero += Timer_TimerReachedZero;
            timer.AddTime(new TimeSpan(0, 0, 5));

            Thread.Sleep(200);
            timer.AddTime(new TimeSpan(0, 0, 4));
            //timer.Reset();
        }

        private static void TestController()
        {
            var hardware = new MicrowaveOvenVirtual(false);
            var timer = new MicrowaveTimer();

            using (var controller = new MirowaveController(hardware, timer))
            {
                if (hardware.DoorOpen)
                {
                    hardware.ToggleDoor();
                }

                hardware.PressStartButton();
            }

            Console.ReadKey();

        }

        private static void Timer_TimerReachedZero()
        {
            Console.WriteLine($"Time elapsed {DateTime.Now - startTime}");
        }
    }
}
