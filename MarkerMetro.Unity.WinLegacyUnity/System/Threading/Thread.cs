using System;
using System.Threading;

#if NETFX_CORE
using System.Threading.Tasks;
using System.Diagnostics;
#endif

namespace MarkerMetro.Unity.WinLegacy.Threading
{
    public delegate void ParameterizedThreadStart(object target);
    public delegate void ThreadStart();

    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.threading.thread.aspx.
    /// </summary>
    public class Thread
    {
        public static int CurrentManagedThreadId
        {
            get
            {
#if NETFX_CORE
                return Environment.CurrentManagedThreadId;
#else
                throw new PlatformNotSupportedException();
#endif
            }
        }

        /*
         * pretty sure Task.Start doesn't always spin up a new thread (depends on synccontext).
         * pretty sure that we'll need try/catching as tasks can throw exceptions when their state isn't as expected (e.g. waiting on a completed task?).
         * */

#if NETFX_CORE
        ParameterizedThreadStart _paramThreadStart;
        ThreadStart _threadStart;
        Task _task = null;
        CancellationTokenSource _taskCancellationTokenSource;
#endif

        /// <summary>
        /// Currently this value is ignored, not sure how to implement this.
        /// </summary>
        public bool IsBackground
        {
            get { return true; }
            set
            {
#if NETFX_CORE
                Debug.WriteLine("Thread.IsBackground ignored.");
#else
                throw new PlatformNotSupportedException();
#endif
            }
        }

        /// <summary>
        /// Determine if the thread is Alive, not implemented.
        /// </summary>
        public bool IsAlive
        {
            get
            {
#if NETFX_CORE
                return _task != null && !_task.IsCompleted;
#else
                throw new PlatformNotSupportedException();
#endif
            }
            set { throw new NotImplementedException(); }
        }

        public Thread(ThreadStart start)
        {
#if NETFX_CORE
            _taskCancellationTokenSource = new CancellationTokenSource();
            _threadStart = start;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public Thread(ParameterizedThreadStart start)
        {
#if NETFX_CORE
            _taskCancellationTokenSource = new CancellationTokenSource();
            _paramThreadStart = start;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public string Name { get; set; }

        public void Abort()
        {
#if NETFX_CORE
            if (_taskCancellationTokenSource != null)
            { 
                _taskCancellationTokenSource.Cancel();
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public bool Join(int ms)
        {
#if NETFX_CORE
            EnsureTask();
            return _task.Wait(ms, _taskCancellationTokenSource.Token);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void Start()
        {
#if NETFX_CORE
            EnsureTask();
            _task.Start(TaskScheduler.Default);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void Start(Object param)
        {
#if NETFX_CORE
            EnsureTask(param);
            _task.Start(TaskScheduler.Default);
#else
            throw new PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE
        /// <summary>
        /// Ensures the underlying Task is created and initialized correctly.
        /// </summary>
        /// <param name="paramThreadStartParam"></param>
        private void EnsureTask(object paramThreadStartParam = null)
        {
            if (_task == null)
            { 
                if (_paramThreadStart != null)
                {
                    _task = new Task(() => _paramThreadStart(paramThreadStartParam), _taskCancellationTokenSource.Token);
                }
                else if (_threadStart != null)
                {
                    _task = new Task(() => _threadStart(), _taskCancellationTokenSource.Token);
                }
            }
        }
#endif

        public static void Sleep(int ms)
        {
            new ManualResetEvent(false).WaitOne(ms);
        }
    }

    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.threading.threadabortexception.aspx.
    /// </summary>
    public class ThreadAbortException : Exception
    {

    }

    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.threading.eventwaithandle.aspx.
    /// </summary>
    public class EventWaitHandle : WaitHandle
    {

    }

    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.threading.autoresetevent.aspx.
    /// </summary>
    public class AutoResetEvent : EventWaitHandle
    {

    }
}

