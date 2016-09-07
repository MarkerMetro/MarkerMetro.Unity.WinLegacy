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
    public class HMACSHA1 : IDisposable
    {
        public void Dispose() { }

#if NETFX_CORE
        private MacAlgorithmProvider hmacSha1;
        private byte[] KeyValue;
#endif

        public byte[] Key
        {
#if NETFX_CORE
            get { return KeyValue; }
            set { KeyValue = value; }
#else
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
#endif
        }

        public HMACSHA1(byte[] keyValue)
        {
#if NETFX_CORE
            //creates algorithm provider
            hmacSha1 = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            KeyValue = new byte[keyValue.Length];
            keyValue.CopyTo(KeyValue, 0);
#else
            throw new System.PlatformNotSupportedException();
#endif
        }

        public HMACSHA1()
        {
#if NETFX_CORE
            //creates algorithm provider
            KeyValue = new Byte[64];
            hmacSha1 = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha1);
            IBuffer randomBuffer = CryptographicBuffer.GenerateRandom(hmacSha1.MacLength);
            CryptographicBuffer.CopyToByteArray(randomBuffer, out KeyValue);
#else
            throw new System.PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// takes in a UTF8 byte array, returns hashed byte array using HMACSHA1
        /// </summary>
        /// <param name="buffer"> UTF8 byte array</param>
        /// <param name="cryptSecret"> a secret key to be used in hash function</param>
        /// <returns>hashed byte array</returns>
        public byte[] ComputeHash(byte[] buffer)
        {
#if NETFX_CORE
            //creates algorithm with secret provided
            var hmacAlg = hmacSha1.CreateHash(KeyValue.AsBuffer());
            //adds data
            hmacAlg.Append(CryptographicBuffer.CreateFromByteArray(buffer));
            byte[] hash;
            //converts to byte array
            CryptographicBuffer.CopyToByteArray(hmacAlg.GetValueAndReset(), out hash);
            return hash;
#else
            throw new System.NotImplementedException();
#endif
        }

        /// <summary>
        /// Computes the hash value for the specified region of the input byte array
        //     and copies the specified region of the input byte array to the specified
        //     region of the output byte array.
        /// </summary>
        /// <param name="inputBuffer">The input to compute the hash code for.</param>
        /// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
        /// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
        /// <param name="outputBuffer">A copy of the part of the input array used to compute the hash code.</param>
        /// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
        /// <returns>The number of bytes written.</returns>
        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            return 0;
        }

        /// <summary>
        /// Computes the hash value for the specified region of the specified byte array.
        /// </summary>
        /// <param name="inputBuffer">The input to compute the hash code for.</param>
        /// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
        /// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
        /// <returns>An array that is a copy of the part of the input that is hashed.</returns>
        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            return null;
        }

    }
}
