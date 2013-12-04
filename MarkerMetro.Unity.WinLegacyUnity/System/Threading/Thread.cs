using System;
using System.Threading;

#if NETFX_CORE
using System.Threading.Tasks;
#endif

namespace MarkerMetro.Unity.WinLegacy.Threading
{
#if NETFX_CORE
    public delegate void ParameterizedThreadStart(object target);
    public delegate void ThreadStart();
#endif

    public class Thread
    {


#if NETFX_CORE

        private ParameterizedThreadStart _paramThreadStart;
        private ThreadStart _threadStart;

        private Task _task = null;
        private CancellationTokenSource _taskCancellationTokenSource;

        public bool IsBackground { get; set; }

        public Thread()
        {
            _taskCancellationTokenSource = new CancellationTokenSource();
        }

        public Thread(ThreadStart start) : this()
        {
            _threadStart = start;
        }

        public Thread(ParameterizedThreadStart start) : this()
        {
            _paramThreadStart = start;
        }

        public void Abort()
        {
            if (_taskCancellationTokenSource != null)
            { 
                _taskCancellationTokenSource.Cancel();
            }
        }

        public bool Join(int ms)
        {
            return _task.Wait(ms, _taskCancellationTokenSource.Token);
        }

        public void Start()
        {
            if (_paramThreadStart != null)
            {
                _task = new Task(() => _paramThreadStart(null), _taskCancellationTokenSource.Token);
            }
            else if (_threadStart != null)
            {
                _task = new Task(() => _threadStart(), _taskCancellationTokenSource.Token);
            }
        }

        public void Start(Object param)
        {
            if (_paramThreadStart != null)
            {
                _task = new Task(() => _paramThreadStart(param), _taskCancellationTokenSource.Token);
            }
            else if (_threadStart != null)
            {
                _task = new Task(() => _threadStart(), _taskCancellationTokenSource.Token);
            }
        }

#endif

        public static void Sleep(int ms)
        {
            new ManualResetEvent(false).WaitOne(ms);
        }
    }

    
#if NETFX_CORE

    public class ThreadAbortException : Exception
    {
        
    }

#endif
}

