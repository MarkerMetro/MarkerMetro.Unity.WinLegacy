using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;

#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
#endif
namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{

    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx.
    /// </summary>
    public class MD5 : IDisposable
    {
        public void Dispose() { }

        public static MD5 Create()
        {
            return new MD5();
        }

#if NETFX_CORE        
        private HashAlgorithmProvider hap;

        protected MD5()
        {
            //creates algorithm provider
            hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
        }

        /// <summary>
        /// takes in a UTF8 byte array, returns hashed byte array using MD5
        /// </summary>
        /// <param name="buffer"> UTF8 byte array</param>
        /// <param name="cryptSecret"> a secret key to be used in hash function</param>
        /// <returns>hashed byte array</returns>
        public byte[] ComputeHash(byte[] buffer)
        {
            IBuffer buff = CryptographicBuffer.CreateFromByteArray(buffer);
            var hashed = hap.HashData(buff);
            byte[] hash;
            CryptographicBuffer.CopyToByteArray(hashed, out hash);
            return hash;
        }
#else
        public byte[] ComputeHash(byte[] buffer) { throw new System.NotImplementedException(); }
        public byte[] ComputeHash(string value) { throw new System.NotImplementedException(); }

#endif
    }
}
