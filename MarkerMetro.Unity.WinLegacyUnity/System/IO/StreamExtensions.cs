using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if NETFX_CORE
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using System.Collections.Concurrent;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public static class StreamExtensions
    {

#if NETFX_CORE
        private static ConcurrentDictionary<Stream, Dictionary<IAsyncResult, int>> readResults =
            new ConcurrentDictionary<Stream, Dictionary<IAsyncResult, int>>();
#endif

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
            AsyncResult res = new AsyncResult();
            res.AsyncState = state;
            res.IsCompleted = false;

            var task = stream.ReadAsync(buffer, offset, count);
            task.ContinueWith((t) =>
            {
                res.IsCompleted = true;
                if (!readResults.ContainsKey(stream))
                    readResults[stream] = new Dictionary<IAsyncResult, int>();
                readResults[stream][res] = t.Result;
                callback(res);
            });
            task.Start();
            return res;
#else
            throw new PlatformNotSupportedException("Stream.BeginRead");
#endif
        }

        public static int EndRead(this Stream stream, IAsyncResult res)
        {
#if NETFX_CORE
            if (!readResults.ContainsKey(stream) || !readResults[stream].ContainsKey(res))
                throw new InvalidOperationException("Pending read operation did not originate from this stream.");
            var result = readResults[stream][res];
            Dictionary<IAsyncResult, int> dictValue = null;
            if (readResults[stream].Count == 1)
            {
                readResults.TryRemove(stream, out dictValue);
            }
            else
            {
                readResults[stream].Remove(res);
            }
            return result;
#else
            throw new PlatformNotSupportedException("Stream.EndRead");
#endif
        }
    }
}