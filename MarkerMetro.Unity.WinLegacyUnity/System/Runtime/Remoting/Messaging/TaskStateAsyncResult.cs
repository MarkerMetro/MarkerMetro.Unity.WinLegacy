#if (NETFX_CORE || WINDOWS_PHONE)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging
{
    public class TaskStateAsyncResult : IAsyncResult
    {
        public readonly Task InnerTask;
        protected IAsyncResult taskAsAR;
        public object AsyncState { get; protected set; }
        public WaitHandle AsyncWaitHandle { get { return taskAsAR.AsyncWaitHandle; } }
        public bool CompletedSynchronously { get { return taskAsAR.CompletedSynchronously; } }
        public bool IsCompleted { get { return taskAsAR.IsCompleted; } }

        public TaskStateAsyncResult(Task task, object state)
        {
            InnerTask = task;
            taskAsAR = task;
            AsyncState = state;
        }
    }

    public class TaskStateAsyncResult<T> : TaskStateAsyncResult
    {
        public readonly new Task<T> InnerTask;

        public TaskStateAsyncResult(Task<T> task, object state)
            : base(task, state)
        {
            InnerTask = task;
            taskAsAR = task;
            AsyncState = state;
        }
    }
}
#endif
