using System;
using System.Threading;

namespace MarkerMetro.Unity.WinLegacy.Threading
{
    public delegate void ParameterizedThreadStart(object target);
    public delegate void ThreadStart();

    public class Thread
    {
        public bool IsBackground { get; set; }

        public Thread(ThreadStart start)
        {
            throw new NotImplementedException();
        }

        public Thread(ParameterizedThreadStart start)
        {
            throw new NotImplementedException();
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public bool Join(int ms)
        {
            return false;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Start(Object thread)
        {
            throw new NotImplementedException();
        }

        public static void Sleep(int ms)
        {
            new ManualResetEvent(false).WaitOne(ms);
        }
    }

    public class ThreadAbortException : Exception
    {
        
    }
}

