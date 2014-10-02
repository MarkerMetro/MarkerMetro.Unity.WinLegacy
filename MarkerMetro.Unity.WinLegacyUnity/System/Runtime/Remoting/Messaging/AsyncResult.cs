#if (NETFX_CORE || WINDOWS_PHONE)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging
{
    public class AsyncResult : IAsyncResult
    {
        public object AsyncState { get; set; }
        public System.Threading.WaitHandle AsyncWaitHandle { get { return null; } }
        public bool CompletedSynchronously { get { return false; } }
        public bool IsCompleted { get; set; }
    }
}
#endif
