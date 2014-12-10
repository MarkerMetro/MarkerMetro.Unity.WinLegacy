using System;
using System.IO;
using MarkerMetro.Unity.WinLegacy.Net.Sockets;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacyUnity.Tests
{
    [TestClass]
    public class TcpClientTests
    {
        //[TestMethod] //Need to manually add throw in TcpClient.ReadFromInputStreamAsyncInner before enabling at moment
#if NETFX_CORE
        public void Metro_TcpClient_ReadFromInputStreamAsync_Exception()
#elif WINDOWS_PHONE
        public void WP8_TcpClient_ReadFromInputStreamAsync_Exception()
#else
        public void TcpClient_ReadFromInputStreamAsync_Exception()
#endif
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.ReadFromInputStreamAsync(1021, (b, e) =>
                {
                    Assert.IsNotNull(e);
                });
            
        }

        //[TestMethod]
#if NETFX_CORE
        public void Metro_TcpClient_BeginEndConnect_Exception()
#elif WINDOWS_PHONE
        public void WP8_TcpClient_BeginEndConnect_Exception()
#else
        public void TcpClient_BeginEndConnect_Exception()
#endif
        {
            TcpClient tcpClient = new TcpClient();
            IAsyncResult ar = tcpClient.BeginConnect("127.0.0.1", 80, null, null);
            tcpClient.EndConnect(ar);
        }
    }
}
