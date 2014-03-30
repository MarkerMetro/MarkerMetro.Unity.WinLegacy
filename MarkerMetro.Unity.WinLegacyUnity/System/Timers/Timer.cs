using System;
#if NETFX_CORE || WINDOWS_PHONE
using Windows.System.Threading;
#endif

namespace MarkerMetro.Unity.WinLegacy.Timers
{
    public delegate void ElapsedEventHandler(Object sender,	ElapsedEventArgs e);
    public class Timer
    {
#if NETFX_CORE || WINDOWS_PHONE
        private ThreadPoolTimer timer;
        private bool running;
#endif
        private bool timeSpanSet = false;
        private TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 100);
        private bool enabled;

        /**
         * The time, in milliseconds, between Elapsed events. The value must be greater
         * than zero, and less than or equal to Int32.MaxValue. The default is 100 milliseconds.
         * 
         * This implementation casts the interval from double to int.
         * 
         * TODO: implement this condition:
         * The interval is greater than Int32.MaxValue, and the timer is currently enabled. 
         * (If the timer is not currently enabled, no exception is thrown until it becomes enabled.) 
         */
        public double Interval
        {
            get { return timeSpan.Milliseconds; }
            set
            {
                if (value <= 0 || value > Int32.MaxValue)
                    throw new ArgumentException("The value of the interval parameter is" +
                        " less than or equal to zero, or greater than Int32.MaxValue.");

                if (!timeSpanSet)
                {
                    timeSpan = new TimeSpan(0, 0, 0, 0, (int)value);
                    timeSpanSet = true;
                }
                else
                    throw new NotImplementedException(
                        "Trying to set interval for the second time is not implemented.");
            }
        }

        /**
         * Gets or sets a value indicating whether the Timer should raise the Elapsed event each
         * time the specified interval elapses or only after the first time it elapses.
         */
        public bool AutoReset { get; set; }

        /** 
         * Gets or sets a value indicating whether the Timer should raise the Elapsed event.
         * 
         * Setting Enabled to true is the same as calling Start, while setting Enabled to
         * false is the same as calling Stop.
         * 
         * TODO: 
         * ObjectDisposedException - This property cannot be set because the timer has been disposed.
         * ArgumentException - The Interval property was set to a value greater than Int32.MaxValue 
         * before the timer was enabled. 
         */
        public bool Enabled
        {
            get { return enabled; }
            set { Start(); }
        }

#pragma warning disable
        public event ElapsedEventHandler Elapsed;
#pragma warning restore

        public Timer() 
        {
#if !NETFX_CORE && !WINDOWS_PHONE
            throw new PlatformNotSupportedException();     
#endif
        }
        public Timer(double interval) : this()
        {
            Interval = interval;
        }

        /**
         * Starts raising the Elapsed event by setting Enabled to true.
         * You can also start timing by setting Enabled to true.
         */
        public void Start()
        {
#if NETFX_CORE || WINDOWS_PHONE
            enabled = true;

            if (!running)
            {
                TimerElapsedHandler handler = (t) =>
                {
                    if (AutoReset)
                        throw new NotImplementedException("If autoreset == true and enabled == true," +
                            " the timer is supposed to restart.");
                    if (Enabled)
                        Elapsed(null, new ElapsedEventArgs());
                };

                timer = ThreadPoolTimer.CreatePeriodicTimer(handler, timeSpan);
                    running = true;
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void Stop()
        {
            enabled = false;
        }

    }

    public class ElapsedEventArgs : EventArgs
    {
        public DateTime SignalTime { get { throw new NotImplementedException(); } }
    }
}
