using Microwave.Hardware.Fakes.Interfaces;
using Microwave.Hardware.Interfaces;
using System;

namespace Microwave.Hardware.Fakes
{
    public class MicrowaveOvenVirtual : IMicrowaveOvenHW, IMicrowaveOvenUser
    {
        public MicrowaveOvenVirtual()
        {
            IsHeating = false;
            DoorOpen = false;
        }

        public bool IsHeating { get; private set; }
        public bool DoorOpen { get; private set; }

        public event Action<bool> DoorOpenChanged;
        public event EventHandler StartButtonPressed;

        public void PressStartButton()
        {
            StartButtonPressed?.Invoke(this, new EventArgs());
        }

        public void ToggleDoor()
        {
            DoorOpen = !DoorOpen;

            DoorOpenChanged.Invoke(DoorOpen);
        }

        public void TurnOffHeater()
        {
            IsHeating = false;
        }

        public void TurnOnHeater()
        {
            IsHeating = true;
        }
    }
}
