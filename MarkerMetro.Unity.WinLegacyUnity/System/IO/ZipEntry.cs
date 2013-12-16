using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class ZipEntry
    {
        public string Key { get; set; }
        public byte[] Bytes { get; set; }
        public MemoryStream MemoryStream { get; set; }

        public Encoding AlternateEncoding { get; set; }
        public ZipOption AlternateEncodingUsage { get; set; }

        public void Extract(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
