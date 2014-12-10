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

        [TestMethod]
        public void Metro_StreamReaderExtensions_BeginEndRead_Exception ()
        {
            System.IO.Stream readStream = new System.IO.MemoryStream();
            System.IO.Stream writeStream = new System.IO.MemoryStream();
            ReadWriteStream rwStream = new ReadWriteStream(readStream, writeStream);
            IAsyncResult ar = rwStream.BeginRead(new byte[1], 0, 0, null);
            rwStream.EndRead(ar);
        }
    }
}
