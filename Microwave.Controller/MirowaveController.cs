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
            throw new NotImplementedException();
        }

        private void _microwaveOvenHW_StartButtonPressed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _microwaveOvenHW_DoorOpenChanged(bool obj)
        {
            throw new NotImplementedException();
        }
    }
}
