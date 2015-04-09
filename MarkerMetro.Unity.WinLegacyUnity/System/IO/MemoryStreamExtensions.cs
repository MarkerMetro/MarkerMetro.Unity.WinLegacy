using System;
using System.IO;

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    public static class MemoryStreamExtensions
    {
        public static void Close(this MemoryStream memoryStream)
        {
            // No need to implement, this should be enough to satisfy the signature.
#if NETFX_CORE
            memoryStream.SetLength(0);
#endif
        }
    }
}
