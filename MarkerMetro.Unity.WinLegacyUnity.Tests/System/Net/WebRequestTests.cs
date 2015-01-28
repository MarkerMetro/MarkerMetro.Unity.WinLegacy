using System;
using System.IO;
using MarkerMetro.Unity.WinLegacy.Net;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Net.Tests
{
    [TestClass]
    public class WebRequestTests
    {
#if NETFX_CORE || WINDOWS_PHONE

        /// <summary>
        /// Test WebRequest.BeginGetResponse on valid url.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroWebRequestBeginGetResponse_ValidUrl_Succeed()
#else
        public void WP8WebRequestBeginGetResponse_ValidUrl_Succeed()
#endif
        {
            bool success = false;
            bool completed = false;
            string error = String.Empty;

            try
            {
                WebRequest request = WebRequest.Create("http://www.microsoft.com");
                IAsyncResult ar = null;

                request.BeginGetResponse((result) =>
                {
                    ar = result;
                    completed = true;
                }, null);

                while (!completed) ;
                request.EndGetResponse(ar);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test WebRequest.Begin/EndGetResponse on invalid url.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroWebRequestBeginGetResponse_InvalidUrl_Fail()
#else
        public void WP8WebRequestBeginGetResponse_InvalidUrl_Fail()
#endif
        {
            bool success = false;
            bool completed = false;
            string error = String.Empty;

            try
            {
                WebRequest request = WebRequest.Create("http://www.adfasdfgagfsd.com");
                IAsyncResult ar = null;

                request.BeginGetResponse((result) =>
                {
                    ar = result;
                    completed = true;
                }, null);

                while (!completed) ;
                request.EndGetResponse(ar);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Should fail BeginGetResponse on invalid url.");
        }
#endif
    }
}
