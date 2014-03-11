using System;

namespace MarkerMetro.Unity.WinLegacy.Timers
{
    public delegate void ElapsedEventHandler(Object sender,	ElapsedEventArgs e);
    public class Timer
    {
#pragma warning disable // <-- delete this after implementing.
        public event ElapsedEventHandler Elapsed;
#pragma warning enable

        public Timer(double interval)
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ElapsedEventArgs : EventArgs
    {
        public DateTime SignalTime { get; private set; }
    }
}
