using System;
using System.Threading;

#if NETFX_CORE
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.System.Threading;
#endif

namespace MarkerMetro.Unity.WinLegacy.Threading
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.threading.threadpool.aspx.
    /// </summary>
    public class ThreadPool
    {
        public static bool QueueUserWorkItem(Action waitCallback)
        {
#if NETFX_CORE
            try
            {
                var thread = QueueUserWorkItemAsync(waitCallback);
                return true;
            }
            catch
            {
                return false;
            }
#else
            throw new PlatformNotSupportedException("ThreadPool.QueueUserWorkItem");
#endif
        }

#if NETFX_CORE

        private static async Task QueueUserWorkItemAsync(Action waitCallback)
        {
            await Task.Run(waitCallback);
        }

#endif

    }
}

