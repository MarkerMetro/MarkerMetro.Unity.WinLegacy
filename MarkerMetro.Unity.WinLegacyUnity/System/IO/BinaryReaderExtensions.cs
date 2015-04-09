using System;
using System.Diagnostics;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    public static class BinaryReaderExtensions
    {
        public static void Close(this BinaryReader binaryReader)
        {
            // Just igore it: binaryReader.Close();
            Debug.WriteLine("Ignoring BinaryReader.Close()");
        }
    }
}
