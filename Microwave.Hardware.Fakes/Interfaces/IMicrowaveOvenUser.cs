namespace Microwave.Hardware.Fakes.Interfaces
{
    /// <summary>
    /// Represents the actions a user can perform on the microwave hardware. This is usefull for integration tests.
    /// </summary>
    public interface IMicrowaveOvenUser
    {
        /// <summary>
        /// User can press the start button.
        /// </summary>
        void PressStartButton();
        /// <summary>
        /// User can open or close the door.
        /// </summary>
        void ToggleDoor();
    }
}
