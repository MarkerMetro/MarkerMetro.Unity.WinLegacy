using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if NETFX_CORE
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using System.Collections.Concurrent;
#endif

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    public static class StreamExtensions
    {
        public static byte[] ToArray(this Stream stream)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static IAsyncResult BeginRead(this Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback, object state = null)
        {
#if NETFX_CORE
            var task = stream.ReadAsync(buffer, offset, count);
            var ar = new TaskStateAsyncResult<int>(task, state);
            if (callback != null)
                task.ContinueWith((t) =>
                {
                    callback(ar);
                });
            return ar;
#else
            throw new PlatformNotSupportedException("Stream.BeginRead");
#endif
        }

        public static int EndRead(this Stream stream, IAsyncResult res)
        {
#if NETFX_CORE
            var ar = (TaskStateAsyncResult<int>)res;
            return ar.InnerTask.Result;
#else
            throw new PlatformNotSupportedException("Stream.EndRead");
#endif
        }
    }
}