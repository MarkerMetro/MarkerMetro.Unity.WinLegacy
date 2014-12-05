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
        //[TestMethod]
#if NETFX_CORE
        public void Metro_TcpClient_ReadFromInputStreamAsync_Exception()
#elif WINDOWS_PHONE
        public void WP8_TcpClient_ReadFromInputStreamAsync_Exception()
#else
        public void TcpClient_ReadFromInputStreamAsync_Exception()
#endif
        {
            bool exception = false;
            TcpClient tcpClient = new TcpClient();
            try
            { 
                tcpClient.ReadFromInputStreamAsync(1021, b =>
                    {
                      
                    });
            }
            catch (AggregateException ex)
            {
                exception = true;
            }
            Assert.IsTrue(exception, "no exception found");
        }
    }
}
