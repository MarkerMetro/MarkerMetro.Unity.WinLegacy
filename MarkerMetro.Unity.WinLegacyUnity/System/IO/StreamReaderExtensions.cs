using System;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public static class StreamReaderExtensions
    {
        public static void Close(this StreamReader streamReader)
        {
            streamReader.Dispose();
        }
    }
}
