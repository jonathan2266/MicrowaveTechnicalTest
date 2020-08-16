using System;

namespace Microwave.Controller.Timers
{
    /// <summary>
    /// Interface for use in a microwave.
    /// </summary>
    public interface ITimer
    {
        event Action TimerReachedZero;
        void AddTime(TimeSpan addedTime);
        void Reset();
    }
}
