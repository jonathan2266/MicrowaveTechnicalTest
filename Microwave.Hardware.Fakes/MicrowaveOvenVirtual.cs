using Microwave.Hardware.Fakes.Interfaces;
using Microwave.Hardware.Interfaces;
using System;

namespace Microwave.Hardware.Fakes
{
    /// <summary>
    /// Represents a virtual Microwave oven for testing and integration tests.
    /// </summary>
    public class MicrowaveOvenVirtual : IMicrowaveOvenHW, IMicrowaveOvenUser
    {
        public MicrowaveOvenVirtual(bool IsDoorOpen)
        {
            IsHeating = false;
            DoorOpen = IsDoorOpen;
        }

        public bool IsHeating { get; private set; }
        public bool IsLightOn => DoorOpen;
        public bool DoorOpen { get; private set; }

        /// <summary>
        /// event fired when door state is changed. Returns boolean IsDoorOpen?
        /// </summary>
        public event Action<bool> DoorOpenChanged;
        public event EventHandler StartButtonPressed;

        public void PressStartButton()
        {
            StartButtonPressed?.Invoke(this, new EventArgs());
        }

        public void ToggleDoor()
        {
            DoorOpen = !DoorOpen;

            DoorOpenChanged?.Invoke(DoorOpen);
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
