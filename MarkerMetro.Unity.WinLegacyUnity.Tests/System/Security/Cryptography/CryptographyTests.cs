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

            Assert.IsTrue(EncryptionProvider.HashToString(hash) == "6d0abfbda56702bd259dfb262383329af17f8eb4");
        }
        

        [TestMethod]
        public void MetroHmacSHA256ComputeHash_MarkerMetro_Succeed()
        {
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes("secret"));

            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("MarkerMetro"));

            Assert.IsTrue(EncryptionProvider.HashToString(hash) == "fa2383f663bbe784c7e5d9076755e37cd3dcf09e4313d40eb3ae86ba2bcb8dde");
        }

        /// <summary>
        /// Test EncryptionProvider encrypt and decrypt.
        /// </summary>
        [TestMethod]

        public void MetroEncryptionProvider_EncryptDecrypt_ShouldEqualOriginal()
        {
            string originalString = "MarkerMetro";
            string key = "encryptionKey";

            //test using MD5 algorithm
            string encryptedString = EncryptionProvider.Encrypt(originalString, key);
            string decryptedString = EncryptionProvider.Decrypt(encryptedString, key);

            Assert.IsTrue(originalString == decryptedString);

            //test using HMACSHA256 algorithm
            encryptedString = EncryptionProvider.Encrypt(originalString, key, HashFunctionType.HMACSHA256, Encoding.UTF8.GetBytes("secret"));
            decryptedString = EncryptionProvider.Decrypt(encryptedString, key, HashFunctionType.HMACSHA256, Encoding.UTF8.GetBytes("secret"));

            Assert.IsTrue(originalString == decryptedString);


        }
#endif
    }
}
