using Microwave.Hardware.Interfaces;
using System;

namespace Microwave.Controller
{
    public class MirowaveController
    {
        private readonly IMicrowaveOvenHW _microwaveOvenHW;

        public MirowaveController(IMicrowaveOvenHW microwaveOvenHW)
        {
            _microwaveOvenHW = microwaveOvenHW;

            Initialize();
        }

        private void Initialize()
        {
            _microwaveOvenHW.DoorOpenChanged += _microwaveOvenHW_DoorOpenChanged;
            _microwaveOvenHW.StartButtonPressed += _microwaveOvenHW_StartButtonPressed;
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
