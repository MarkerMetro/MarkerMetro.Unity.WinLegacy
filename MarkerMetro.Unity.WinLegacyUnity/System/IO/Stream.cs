using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class Stream
    {
        public long Position { get; set; }

        public byte[] ToArray()
        {
            return new byte[0];
        }
    }
}
