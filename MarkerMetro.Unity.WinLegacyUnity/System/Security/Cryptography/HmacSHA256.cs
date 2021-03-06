﻿using System;
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
    public class HMACSHA256 : IDisposable
    {
        public void Dispose() { }

#if NETFX_CORE
        private MacAlgorithmProvider hmacSha256;
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

        public HMACSHA256(byte[] keyValue)
        {
#if NETFX_CORE
            //creates algorithm provider
            hmacSha256 = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
            KeyValue = keyValue;
#else
            throw new System.PlatformNotSupportedException();
#endif
        }

        public HMACSHA256()
        {
#if NETFX_CORE
            //creates algorithm provider
            KeyValue = new Byte[64];
            hmacSha256 = MacAlgorithmProvider.OpenAlgorithm(MacAlgorithmNames.HmacSha256);
            IBuffer randomBuffer = CryptographicBuffer.GenerateRandom(hmacSha256.MacLength);
            CryptographicBuffer.CopyToByteArray(randomBuffer, out KeyValue);
#else
            throw new System.PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// takes in a UTF8 byte array, returns hashed byte array using HmacSHA256
        /// </summary>
        /// <param name="buffer"> UTF8 byte array</param>
        /// <param name="cryptSecret"> a secret key to be used in hash function</param>
        /// <returns>hashed byte array</returns>
        public byte[] ComputeHash(byte[] buffer)
        {
#if NETFX_CORE
            //creates algorithm with secret provided
            var hmacAlg = hmacSha256.CreateHash(KeyValue.AsBuffer());
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

    }
}
