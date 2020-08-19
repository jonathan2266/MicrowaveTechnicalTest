using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Controller.Timers;
using Microwave.Hardware.Interfaces;
using Moq;
using System;

namespace Microwave.Controller.Tests
{
    [TestClass]
    public class MirowaveControllerTests
    {
        private MirowaveController controller;
        private Mock<ITimer> timerMock;
        private Mock<IMicrowaveOvenHW> hardwareMock;

        [TestInitialize]
        public void Setup()
        {
            timerMock = new Mock<ITimer>();
            hardwareMock = new Mock<IMicrowaveOvenHW>();

            controller = new MirowaveController(hardwareMock.Object, timerMock.Object);
        }

        [TestMethod]
        public void MicrowaveController_StartButtonpressedWithOpenDoor_NothingHappens()
        {
            hardwareMock.Setup(hardware => hardware.DoorOpen).Returns(true);

            hardwareMock.Raise(exp => exp.StartButtonPressed += null, EventArgs.Empty);

            //verify results
            timerMock.Verify(timer => timer.AddTime(It.IsAny<TimeSpan>()), Times.Never);
            hardwareMock.Verify(hardware => hardware.TurnOnHeater(), Times.Never);
        }

        [TestMethod]
        public void MicrowaveController_StartButtonpressedWithClosedDoor_StartsHeaterRunsTimerOneMinute()
        {
            hardwareMock.Setup(hardware => hardware.DoorOpen).Returns(false);
            var oneMinuteTimespan = new TimeSpan(0, 1, 0);

            hardwareMock.Raise(exp => exp.StartButtonPressed += null, EventArgs.Empty);

            //verify results
            timerMock.Verify(timer => timer.AddTime(It.Is<TimeSpan>(span => span == oneMinuteTimespan)));
            hardwareMock.Verify(hardware => hardware.TurnOnHeater(), Times.AtLeastOnce);
        }

        [TestMethod]
        public void MicrowaveController_StartButtonpressedMultipleTimesWithClosedDoor_StartsHeaterIncreasesTimerWithOneMinute()
        {
            hardwareMock.Setup(hardware => hardware.DoorOpen).Returns(false);
            var oneMinuteTimespan = new TimeSpan(0, 1, 0);

            int startButtonPressed = 3;

            for (int i = 0; i < startButtonPressed; i++)
            {
                hardwareMock.Raise(exp => exp.StartButtonPressed += null, EventArgs.Empty);
            }

            //verify results
            timerMock.Verify(timer => timer.AddTime(It.Is<TimeSpan>(span => span == oneMinuteTimespan)), Times.Exactly(startButtonPressed));
            hardwareMock.Verify(hardware => hardware.TurnOnHeater(), Times.AtLeastOnce);
        }

        [TestMethod]
        public void MicrowaveController_DoorOpens_StopsHeater()
        {
            hardwareMock.Raise(expr => expr.DoorOpenChanged += null, true);

            //verify results
            timerMock.Verify(timer => timer.Reset(), Times.AtLeastOnce);
            hardwareMock.Verify(hardware => hardware.TurnOffHeater(), Times.AtLeastOnce);
        }

        [TestMethod]
        public void MicrowaveController_TimerReachedZero_StopsHeater()
        {

            timerMock.Raise(expr => expr.TimerReachedZero += null);

            //verify results
            hardwareMock.Verify(hardware => hardware.TurnOffHeater(), Times.AtLeastOnce);
        }
    }
}
