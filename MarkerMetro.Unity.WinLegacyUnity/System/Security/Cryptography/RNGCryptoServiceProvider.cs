using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NETFX_CORE
using Windows.Security.Cryptography;
using System.Runtime.InteropServices.WindowsRuntime;
#endif

namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{
    class RNGCryptoServiceProvider
    {
        public void GetBytes (byte[] buffer)
        {
#if NETFX_CORE
            CryptographicBuffer.GenerateRandom((uint)buffer.Length).ToArray().CopyTo(buffer, 0);
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
