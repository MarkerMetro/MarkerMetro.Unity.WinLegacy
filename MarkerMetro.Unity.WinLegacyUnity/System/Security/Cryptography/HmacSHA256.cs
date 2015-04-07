using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
#endif

namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{
    public class HmacSHA256 : IDisposable
    {
        public static HmacSHA256 Create()
        {
            return new HmacSHA256();
        }

        public void Dispose() { }

#if NETFX_CORE        
        private MacAlgorithmProvider hap;

        private HmacSHA256()
        {
            //creates algorithm provider
            hap = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
        }

        /// <summary>
        /// takes in a UTF8 byte array, returns hashed byte array using HmacSHA256
        /// </summary>
        /// <param name="buffer"> UTF8 byte array</param>
        /// <param name="cryptSecret"> a secret key to be used in hash function</param>
        /// <returns>hashed byte array</returns>
        public byte[] ComputeHash(byte[] buffer, string cryptSecret)
        {
            //creates algorithm with secret provided
            var hmacAlg = hap.CreateHash(Encoding.UTF8.GetBytes(cryptSecret).AsBuffer());
            //adds data
            hmacAlg.Append(CryptographicBuffer.CreateFromByteArray(buffer));
            byte[] hash;
            //converts to byte array
            CryptographicBuffer.CopyToByteArray(hmacAlg.GetValueAndReset(), out hash);
            return hash;
        }

        /// <summary>
        /// takes in a string, encodes into UTF8 and returns hash
        /// </summary>
        /// <param name="value"> string data to be converted into UTF8 </param>
        /// <param name="cryptSecret"> a secret key to be used in hash function</param>
        /// <returns>hashed byte array</returns>
        public byte[] ComputeHash(string value, string cryptSecret)
        {
            return ComputeHash(Encoding.UTF8.GetBytes(value), cryptSecret);
        }
#else
        public byte[] ComputeHash(byte[] buffer, string cryptSecret) { throw new System.NotImplementedException(); }
        public byte[] ComputeHash(string value, string cryptSecret) { throw new System.NotImplementedException(); }

#endif
    }
}
