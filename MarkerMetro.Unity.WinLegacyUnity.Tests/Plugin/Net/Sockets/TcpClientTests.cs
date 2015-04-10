using System;
using System.IO;
using MarkerMetro.Unity.WinLegacy.Net.Sockets;
#if NETFX_CORE
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Net.Sockets.Tests
{
    [TestClass]
    public class TcpClientTests
    {
#if NETFX_CORE

        /// <summary>
        /// Test TcpClient.ReadFromInputStreamAsync catch exception thrown in TcpClient.ReadFromInputStreamAsyncInner.
        /// </summary>
        //[TestMethod] //Need to manually add throw in TcpClient.ReadFromInputStreamAsyncInner before enabling at moment.
#if NETFX_CORE
        public void MetroTcpClient_ReadFromInputStreamAsync_Fail()
#else
        public void WP8TcpClient_ReadFromInputStreamAsync_Fail()
#endif
        {
            bool success = false;
            bool completed = false;
            TcpClient tcpClient = new TcpClient();
            tcpClient.ReadFromInputStreamAsync(1021, (result) =>
            {
                completed = true;
                success = true;
            }, (e) =>
            {
                completed = true;
            });

            while (!completed) ;

            Assert.IsFalse(success, "Should catch exception in TcpClient.ReadFromInputStreamAsyncInner.");
        }

        /// <summary>
        /// Test TcpClient.WriteToOutputStreamAsync catch exception thrown in TcpClient.WriteToOutputStreamAsyncInner.
        /// </summary>
        //[TestMethod] //Need to manually add throw in TcpClient.WriteToOutputStreamAsyncInner before enabling at moment.
#if NETFX_CORE
        public void MetroTcpClient_WriteToOutputStreamAsync_Fail()
#else
        public void WP8TcpClient_WriteToOutputStreamAsync_Fail()
#endif
        {
            bool success = false;
            bool completed = false;
            TcpClient tcpClient = new TcpClient();
            tcpClient.WriteToOutputStreamAsync(new byte[1], () =>
            {
                completed = true;
                success = true;
            }, (e) =>
            {
                completed = true;
            });

            while (!completed) ;

            Assert.IsFalse(success, "Should catch exception in TcpClient.WriteToOutputStreamAsyncInner.");
        }

        /// <summary>
        /// Test TcpClient.BeginConnect and EndConnect with valid IP.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroTcpClientBeginEndConnect_ValidIP_Succeed()
#else
        public void WP8TcpClientBeginEndConnect_ValidIP_Succeed()
#endif
        {
            bool success = false;
            string error = String.Empty;

            try
            {
                TcpClient tcpClient = new TcpClient();
                TaskStateAsyncResult ar = (TaskStateAsyncResult)tcpClient.BeginConnect("127.0.0.1", 80, null, null);

                while (!ar.IsCompleted) ;

                tcpClient.EndConnect(ar);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test TcpClient.BeginConnect and EndConnect with invalid IP.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroTcpClientBeginEndConnect_InvalidIP_Fail()
#else
        public void WP8TcpClientBeginEndConnect_InvalidIP_Fail()
#endif
        {
            bool success = false;
            string error = String.Empty;

            try
            {
                TcpClient tcpClient = new TcpClient();
                TaskStateAsyncResult ar = (TaskStateAsyncResult)tcpClient.BeginConnect(null, 80, null, null);

                while (!ar.IsCompleted) ;

                tcpClient.EndConnect(ar);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Should fail with invalid IP.");
        }

        /// <summary>
        /// Test TcpClient.Connect.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroTcpClientConnect_CheckConnection_Fail()
#else
        public void WP8TcpClientConnect_CheckConnection_Fail()
#endif
        {
            bool success = false;
            string error = String.Empty;

            try
            {
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect("127.0.0.1", 80);

                success = tcpClient.Connected;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Should fail connection.");
        }
#endif
    }
}
