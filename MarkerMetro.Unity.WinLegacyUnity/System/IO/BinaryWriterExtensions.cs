using System;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public static class BinaryWriterExtensions
    {
        public static void Close(this BinaryWriter binaryWriter)
        {
            binaryWriter.Close();
        }
    }
}
