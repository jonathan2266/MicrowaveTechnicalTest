using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Hardware.Fakes;

namespace Microwave.Controller.Tests
{
    [TestClass]
    public class MicrowaveOvenVirtualTests
    {
        private MicrowaveOvenVirtual hardware;

        [TestInitialize]
        public void Setup()
        {
            //always start with a closed door.
            hardware = new MicrowaveOvenVirtual(false);
        }

        [TestMethod]
        public void MicrowaveController_DoorIsClosed_LightsAreOff()
        {
            Assert.IsTrue(!hardware.DoorOpen && !hardware.IsLightOn);
        }

        [TestMethod]
        public void MicrowaveController_DoorIsOpen_LightsAreOn()
        {
            hardware.ToggleDoor();

            Assert.IsTrue(hardware.DoorOpen && hardware.IsLightOn);
        }
    }
}
