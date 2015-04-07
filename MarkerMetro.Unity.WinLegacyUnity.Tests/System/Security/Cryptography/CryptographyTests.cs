using System;
using System.Text;
using MarkerMetro.Unity.WinLegacy.Security.Cryptography;

#if NETFX_CORE
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography.Tests
{
    [TestClass]
    public class CryptographyTests
    {
#if WINDOWS_PHONE
        /// <summary>
        /// Test MD5.GetMd5String should equal expected result.
        /// </summary>
        [TestMethod]
        public void WP8MD5GetMd5String_MarkerMetro_Succeed()
        {
            Assert.IsTrue(MD5.GetMd5String("MarkerMetro") == "9c33c811d0c45c41cb66b393ec9f7ed7");
        }
#endif

#if NETFX_CORE || WINDOWS_PHONE
        /// <summary>
        /// Test SHA1.ComputeHash should equal expected result.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroSHA1ComputeHash_MarkerMetro_Succeed()
#else
        public void WP8SHA1ComputeHash_MarkerMetro_Succeed()
#endif
        {
            SHA1 sha1 = SHA1.Create();

            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes("MarkerMetro"));
            var sb = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            string hashString = sb.ToString();

            Assert.IsTrue(sb.ToString() == "6d0abfbda56702bd259dfb262383329af17f8eb4");
        }

        /// <summary>
        /// Test EncryptionProvider encrypt and decrypt.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroEncryptionProvider_EncryptDecrypt_ShouldEqualOriginal()
#else
        public void WP8EncryptionProvider_EncryptDecrypt_ShouldEqualOriginal()
#endif
        {
            string originalString = "MarkerMetro";
            string key = "encryptionKey";
            string encryptedString = EncryptionProvider.Encrypt(originalString, key);
            string decryptedString = EncryptionProvider.Decrypt(encryptedString, key);

            Assert.IsTrue(originalString == decryptedString);
        }
#endif
    }
}
