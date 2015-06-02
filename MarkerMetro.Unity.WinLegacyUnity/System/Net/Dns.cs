using System;
using System.Collections.Generic;

#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
#else
using System.Threading;
#endif


namespace MarkerMetro.Unity.WinLegacy.Net
{
    public class Dns
    {
        public static IAsyncResult BeginGetHostEntry(string remoteHostName, AsyncCallback requestCallback, Object stateObject)
        {
#if NETFX_CORE
            var task = ResolveDNS(remoteHostName);
            TaskStateAsyncResult<string> res = new TaskStateAsyncResult<string>(task, stateObject);

            task.ContinueWith((t) =>
            {
                if (requestCallback != null)
                {
                    requestCallback(res);
                }
            });

            return res;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static string EndGetHostEntry(IAsyncResult result)
        {
#if NETFX_CORE
            var ar = (TaskStateAsyncResult<string>)result;
            if (ar.InnerTask.IsFaulted)
                throw ar.InnerTask.Exception;

            return ar.InnerTask.Result;
#else
            throw new PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE
        private static async Task<string> ResolveDNS(string remoteHostName)
        {
            if (string.IsNullOrEmpty(remoteHostName))
                return string.Empty;

            string ipAddress = string.Empty;

            IReadOnlyList<EndpointPair> data =
              await DatagramSocket.GetEndpointPairsAsync(new HostName(remoteHostName), "0");

            if (data != null && data.Count > 0)
            {
                foreach (EndpointPair item in data)
                {
                    if (item != null && item.RemoteHostName != null &&
                                  item.RemoteHostName.Type == HostNameType.Ipv4)
                    {
                        return item.RemoteHostName.CanonicalName;
                    }
                }
            }

            return ipAddress;
        }
#endif
    }
}
