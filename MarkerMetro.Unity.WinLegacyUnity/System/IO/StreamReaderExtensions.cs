using System;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public static class StreamReaderExtensions
    {
        public static void Close(this StreamReader streamReader)
        {
            if (streamReader == null)
                throw new ArgumentNullException("streamReader", "streamReader is null.");

            streamReader.Dispose();
        }
    }
}
