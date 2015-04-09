using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#endif

namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{
    public enum HashFunctionType
    {
        MD5,
        SHA1,
        HMACSHA256
    }
    public static class HashProvider
    {
        /// <summary>
        /// Encrypts using specified Hash Function Type. 
        /// </summary>
        /// <param name="toEncrypt"> string to be encrypted. </param>
        /// <param name="key"> secret to be used for encryption. Key value can be null for 
        /// some hash functions (ie SHA1 & MD5 do not require key). </param>
        /// <param name="type">type of the hash function to be used</param>
        /// <returns>encrypted string</returns>
        public static string HashString(string toHash, string key = null, HashFunctionType type = HashFunctionType.MD5)
        {
            switch (type)
            {
                case HashFunctionType.MD5:
                    return GetMD5HexString(toHash);
                case HashFunctionType.SHA1:
                    return SHA1.Create().ComputeHashString(toHash);
                case HashFunctionType.HMACSHA256:
                    return HmacSHA256.Create().ComputeHashString(toHash, key);
                default:
                    System.Diagnostics.Debug.WriteLine("Unrecognized Hash function type: " + type);
                    throw new Exception("Unrecognized Hash function type: " + type);
            }
        }

        public static byte[] ComputeMD5(byte[] input)
        {
#if NETFX_CORE
            var key = CryptographicBuffer.CreateFromByteArray(input);
            var result = GetHashBytes(HashAlgorithmNames.Md5, key);
            byte[] resultBytes;
            CryptographicBuffer.CopyToByteArray(result, out resultBytes);
            return resultBytes;
#else
            throw new System.PlatformNotSupportedException();
#endif
        }


        public static string GetMD5HexString(string input)
        {
#if NETFX_CORE
            return CryptographicBuffer.EncodeToHexString(GetMD5Hash(input));
#else
            throw new System.PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE
        internal static IBuffer GetMD5Hash(string key)
        {
            return GetHash(HashAlgorithmNames.Md5, key);
        }
        internal static IBuffer GetSHA1Hash(string key)
        {
            return GetHash(HashAlgorithmNames.Sha1, key);
        }

        private static IBuffer GetHashBytes(string algorithm, IBuffer key)
        {
            // Create a HashAlgorithmProvider object.
            HashAlgorithmProvider objAlgProv = HashAlgorithmProvider.OpenAlgorithm(algorithm);

            // Hash the message.
            IBuffer buffHash = objAlgProv.HashData(key);

            // Verify that the hash length equals the length specified for the algorithm.
            if (buffHash.Length != objAlgProv.HashLength)
            {
                throw new Exception("There was an error creating the hash");
            }

            return buffHash;
        }

        private static IBuffer GetHash(string algorithm, string key)
        {
            // Convert the message string to binary data.
            IBuffer buffUtf8Msg = CryptographicBuffer.ConvertStringToBinary(key, BinaryStringEncoding.Utf8);

            return GetHashBytes(algorithm, buffUtf8Msg);
        }
#endif
    }
}
