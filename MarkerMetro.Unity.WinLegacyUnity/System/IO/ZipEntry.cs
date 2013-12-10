using System;
using System.IO;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class ZipEntry
    {
        public Encoding AlternateEncoding { get; set; }
        public ZipOption AlternateEncodingUsage { get; set; }

        public void Extract(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
