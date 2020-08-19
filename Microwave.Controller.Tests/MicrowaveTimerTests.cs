using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microwave.Controller.Timers;
using System;
using System.Threading;

namespace Microwave.Controller.Tests
{
    [TestClass]
    public class MicrowaveTimerTests
    {
        private ITimer timer;

        [TestInitialize]
        public void Setup()
        {
            timer = new MicrowaveTimer();
        }

        [TestMethod]
        public void MicrowaveTimer_AddOneSecond_FiresEventAfterOneSecond()
        {
            AutoResetEvent block = new AutoResetEvent(false);
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            DateTime timerStart = DateTime.Now;
            TimeSpan? timerdifference = null;

            timer.TimerReachedZero += () =>
            {
                timerdifference = DateTime.Now - timerStart;
                Assert.IsTrue(((int)(timerdifference.Value).TotalSeconds) == (int)oneSecond.TotalSeconds);
                block.Set();
            };

            timer.AddTime(oneSecond);

            //fail the test if it takes longer then expected to fire the event.
            block.WaitOne((int)oneSecond.Add(oneSecond).TotalMilliseconds);

            if (!timerdifference.HasValue)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MicrowaveTimer_AddMultipleSeconds_FiresEventAfterTotalSum()
        {
            AutoResetEvent block = new AutoResetEvent(false);
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);
            TimeSpan ExpectedCumulativeTime = new TimeSpan(0, 0, 2);

            DateTime timerStart = DateTime.Now;
            TimeSpan? timerdifference = null;

            timer.TimerReachedZero += () =>
            {
                timerdifference = DateTime.Now - timerStart;
                Assert.IsTrue(((int)(timerdifference.Value).TotalSeconds) == (int)ExpectedCumulativeTime.TotalSeconds);
                block.Set();
            };

            timer.AddTime(oneSecond);
            timer.AddTime(oneSecond);

            //fail the test if it takes longer then expected to fire the event.
            block.WaitOne((int)(oneSecond.Add(ExpectedCumulativeTime).TotalMilliseconds));

            if (!timerdifference.HasValue)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MicrowaveTimer_SubstractTime_ArgumentOutOfRangeException()
        {
            AutoResetEvent block = new AutoResetEvent(false);
            TimeSpan minusOneSecond = new TimeSpan(0, 0, -1);

            timer.TimerReachedZero += () =>
            {
                //exception should have been thrown
                Assert.Fail();
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => {

                timer.AddTime(minusOneSecond);

                //exception should have been thrown
                Assert.Fail();

            });
        }

        [TestMethod]
        public void MicrowaveTimer_CallingReset_TimerReachedZeroNotCalled()
        {
            AutoResetEvent block = new AutoResetEvent(false);
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            bool timerElapsed = false;

            timer.TimerReachedZero += () =>
            {
                timerElapsed = true;
            };

            timer.AddTime(oneSecond);
            timer.Reset();

            block.WaitOne((int)oneSecond.Add(oneSecond).TotalMilliseconds);

            Assert.IsFalse(timerElapsed);
        }

    }
}
