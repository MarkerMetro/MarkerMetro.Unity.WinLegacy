#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#endif

using System;

namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.security.cryptography.sha1.aspx.
    /// </summary>
    public class SHA1 : IDisposable
    {

        public static SHA1 Create()
        {
            return new SHA1();
        }

        public void Dispose() {}

#if NETFX_CORE        
        private HashAlgorithmProvider hap;

        private SHA1()
        {
            hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            IBuffer buffInput = CryptographicBuffer.CreateFromByteArray(buffer);
            IBuffer buffHash = hap.HashData(buffInput);

            byte[] hash;
            CryptographicBuffer.CopyToByteArray(buffHash, out hash);
            return hash;
        }
        public string ComputeHashString(byte[] buffer)
        {
            byte[] byteHash = ComputeHash(buffer);
            return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
        }
        public string ComputeHashString(string buffer)
        {
            byte[] byteHash = ComputeHash(System.Text.Encoding.UTF8.GetBytes(buffer));
            return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
        }
#elif WINDOWS_PHONE
        public byte[] ComputeHash(byte[] buffer)
        {
            using (var sha = new System.Security.Cryptography.SHA1Managed())
                return sha.ComputeHash(buffer);
        }
#else
        public byte[] ComputeHash(byte[] buffer) { throw new System.NotImplementedException(); }
        public string ComputeHashString(byte[] buffer) { throw new System.NotImplementedException(); }
        public string ComputeHashString(string buffer) { throw new System.NotImplementedException(); }
#endif
    }
}
