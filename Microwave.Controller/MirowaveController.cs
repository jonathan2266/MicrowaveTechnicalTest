using Microwave.Controller.Timers;
using Microwave.Hardware.Interfaces;
using System;

namespace Microwave.Controller
{
    /// <summary>
    /// Custom controller for the microwave oven. Interacts with the hardware and processes user interactions.
    /// </summary>
    public class MirowaveController
    {
        private readonly IMicrowaveOvenHW _microwaveOvenHW;
        private readonly ITimer _timer;

        private readonly TimeSpan DefaultTimerDuration = new TimeSpan(0, 1, 0);

        public MirowaveController(IMicrowaveOvenHW microwaveOvenHW, ITimer timer)
        {
            _microwaveOvenHW = microwaveOvenHW ?? throw new ArgumentNullException("microwaveOvenHW");
            _timer = timer ?? throw new ArgumentNullException("timer");

            Initialize();
        }

        private void Initialize()
        {
            _microwaveOvenHW.DoorOpenChanged += _microwaveOvenHW_DoorOpenChanged;
            _microwaveOvenHW.StartButtonPressed += _microwaveOvenHW_StartButtonPressed;

            _timer.TimerReachedZero += _timer_TimerReachedZero;
        }

        private void _timer_TimerReachedZero()
        {
            _microwaveOvenHW.TurnOffHeater();
        }

        private void _microwaveOvenHW_StartButtonPressed(object sender, EventArgs e)
        {
            if (_microwaveOvenHW.DoorOpen) return;

            _timer.AddTime(DefaultTimerDuration);

            _microwaveOvenHW.TurnOnHeater();
        }

        private void _microwaveOvenHW_DoorOpenChanged(bool IsDoorOpen)
        {
            if (IsDoorOpen)
            {
                _timer.Reset();
                _microwaveOvenHW.TurnOffHeater();
            }
        }
    }
}
