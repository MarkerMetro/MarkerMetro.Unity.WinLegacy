using System;
using System.Diagnostics;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    public static class BinaryWriterExtensions
    {
        public static void Close(this BinaryWriter binaryWriter)
        {
            // binaryWriter.Close();
            Debug.WriteLine("Ignoring BinaryWriter.Close()");
        }
    }
}
