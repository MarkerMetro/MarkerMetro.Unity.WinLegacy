using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using MarkerMetro.Unity.WinLegacy.IO;

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class StreamReaderExtensionsTests
    {
        [TestMethod]
        public void Close_DNF()
        {
            using(var ms = new System.IO.MemoryStream())
            {
                using(var sr = new System.IO.StreamReader(ms))
                {
                    sr.Close();
                }
            }
        }
    }
}
