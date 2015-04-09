using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif
using MarkerMetro.Unity.WinLegacy.Plugin.IO;

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class StreamReaderExtensionsTests
    {
        [TestMethod]
        public void MetroStreamReaderExtensions_Close_Succeed()
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
