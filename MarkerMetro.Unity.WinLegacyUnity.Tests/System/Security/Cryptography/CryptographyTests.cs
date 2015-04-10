using System;
using System.Text;
using MarkerMetro.Unity.WinLegacy.Security.Cryptography;

#if NETFX_CORE || WINDOWS_PHONE
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
#if NETFX_CORE
        /// <summary>
        /// Test SHA1.ComputeHash should equal expected result.
        /// </summary>
        [TestMethod]
        public void MetroSHA1ComputeHash_MarkerMetro_Succeed()
        {
            SHA1 sha1 = SHA1.Create();

            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes("MarkerMetro"));

            Assert.IsTrue(HashToString(hash) == "6d0abfbda56702bd259dfb262383329af17f8eb4");
        }

        /// <summary>
        /// Converts byte array to string.
        /// </summary>
        /// <param name="byteHash"></param>
        /// <returns> Converted string. </returns>
        public static string HashToString(byte[] byteHash)
        {
            return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
        }

        [TestMethod]
        public void MetroHmacSHA256ComputeHash_MarkerMetro_Succeed()
        {
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes("secret"));

            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("MarkerMetro"));

            Assert.IsTrue(HashToString(hash) == "fa2383f663bbe784c7e5d9076755e37cd3dcf09e4313d40eb3ae86ba2bcb8dde");
        }

        [TestMethod]
        public void MetroHmacMD5ComputeHash_MarkerMetro_Succeed()
        {
            MD5 md5 = MD5.Create();

            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes("MarkerMetro"));

            Assert.IsTrue(HashToString(hash) == "9c33c811d0c45c41cb66b393ec9f7ed7");
        }
        /// <summary>
        /// Test EncryptionProvider MD5 encrypt and decrypt.
        /// </summary>
        [TestMethod]

        public void MetroEncryptionProvider_EncryptDecrypt_MD5_ShouldEqualOriginal()
        {
            string originalString = "MarkerMetro";
            string key = "encryptionKey";

            //test using MD5 algorithm
            string encryptedString = EncryptionProvider.Encrypt(originalString, key);
            string decryptedString = EncryptionProvider.Decrypt(encryptedString, key);

            Assert.IsTrue(originalString == decryptedString);


        }
        /// <summary>
        /// Test EncryptionProvider HMACSHA256 encrypt and decrypt.
        /// </summary>
        public void MetroEncryptionProvider_EncryptDecrypt_HMACSHA256_ShouldEqualOriginal()
        {
            string originalString = "MarkerMetro";
            string key = "encryptionKey";

            //test using HMACSHA256 algorithm
            var encryptedString = EncryptionProvider.Encrypt(originalString, key, HashFunctionType.HMACSHA256, Encoding.UTF8.GetBytes("secret"));
            var decryptedString = EncryptionProvider.Decrypt(encryptedString, key, HashFunctionType.HMACSHA256, Encoding.UTF8.GetBytes("secret"));

            Assert.IsTrue(originalString == decryptedString);


        }
        /// <summary>
        /// A test method verifying SHA1 hash function failing because of invalid block size
        /// </summary>
        [TestMethod]
        public void MetroHmacSHA1Encryption_MarkerMetro_Failed()
        {
            string originalString = "MarkerMetro";
            string key = "encryptionKey";

            Assert.ThrowsException<Exception>(() => EncryptionProvider.Encrypt(originalString, key, HashFunctionType.SHA1));
            Assert.ThrowsException<Exception>(() => EncryptionProvider.Decrypt(originalString, key, HashFunctionType.SHA1));
        }
#endif
    }
}
