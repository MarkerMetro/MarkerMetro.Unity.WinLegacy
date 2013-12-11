using System.IO;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public static class TextReaderExtensions
    {
        public static void Close(this TextReader reader)
        {
            reader.Dispose();
        }
    }
}
