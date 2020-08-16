using System;
using System.Threading;

namespace Microwave.Controller.Timers
{
    /// <summary>
    /// Timer logic for the microwave.
    /// </summary>
    public class MicrowaveTimer : ITimer
    {
        private readonly Timer alarmTimer;
        private DateTime? dueTime;

        public MicrowaveTimer()
        {
            alarmTimer = new Timer(InternalTimerElapsed, null, Timeout.Infinite, 0);
        }
        public event Action TimerReachedZero;

        /// <summary>
        /// Increase the timer value with the specified amount. This amount must be positive.
        /// </summary>
        /// <param name="addedTime">A positive value to increase the timer with.</param>
        public void AddTime(TimeSpan addedTime)
        {
            if (addedTime.TotalMilliseconds < 0)
            {
                throw new ArgumentOutOfRangeException("addedTime", "You are only allowed to add positive times.");
            }

            var period = CalculateNewTimerTimespan(addedTime);

            alarmTimer.Change((int)period.TotalMilliseconds, 0);
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void Reset()
        {
            alarmTimer.Change(Timeout.Infinite, 0);
        }

        private void InternalTimerElapsed(object state)
        {
            alarmTimer.Change(Timeout.Infinite, 0);
            TimerReachedZero?.Invoke();
        }

        private TimeSpan CalculateNewTimerTimespan(TimeSpan intervalToAdd)
        {
            TimeSpan remainingTimeSpan = new TimeSpan();
            DateTime currentTime = DateTime.Now;

            if (dueTime.HasValue)
            {
                remainingTimeSpan = dueTime.Value - currentTime;
            }

            var totalTimeSpan = remainingTimeSpan.Add(intervalToAdd);

            dueTime = currentTime.Add(totalTimeSpan);

            return totalTimeSpan;
        }
    }
}
