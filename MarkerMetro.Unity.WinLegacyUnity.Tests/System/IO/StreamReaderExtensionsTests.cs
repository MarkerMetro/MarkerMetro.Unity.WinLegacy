using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using MarkerMetro.Unity.WinLegacy.IO;

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class StreamReaderExtensionsTests
    {
        [TestMethod]
#if NETFX_CORE
        public void MetroStreamReaderExtensions_Close_Succeed()
#elif WINDOWS_PHONE
        public void WP8StreamReaderExtensions_Close_Succeed()
#else
        public void StreamReaderExtensions_Close_Succeed()
#endif
        {
            using(var ms = new System.IO.MemoryStream())
            {
                using(var sr = new StreamReader(ms))
                {
                    sr.Close();
                }
            }
        }
    }
}
