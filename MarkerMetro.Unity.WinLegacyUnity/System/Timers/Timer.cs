using System;

namespace MarkerMetro.Unity.WinLegacy.Timers
{
    public delegate void ElapsedEventHandler(Object sender,	ElapsedEventArgs e);
    public class Timer
    {
        public event ElapsedEventHandler Elapsed;
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
