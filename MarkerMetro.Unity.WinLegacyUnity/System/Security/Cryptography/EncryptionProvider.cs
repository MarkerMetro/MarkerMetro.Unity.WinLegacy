using System;

#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#endif


namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{
    public static class EncryptionProvider
    {

        /// <summary>
        /// Encrypt a string using dual encryption method. Returns an encrypted text.
        /// </summary>
        /// <param name="toEncrypt">String to be encrypted</param>
        /// <param name="key">Unique key for encryption/decryption</param>m>
        /// <param name="type"> Type of Hash function to be used. </param>
        /// <param name="hashKey"> optional key to be used with HMACSHA256 algorithm </param>
        /// <returns>Returns encrypted string.</returns>
        public static string Encrypt(string toEncrypt, string key, HashFunctionType type = HashFunctionType.MD5, byte[] hashKey = null)
        {
#if NETFX_CORE
            try
            {

                byte[] keyHash = Hash(key, type, hashKey);


                // Create a buffer that contains the encoded message to be encrypted.
                var toDecryptBuffer = CryptographicBuffer.ConvertStringToBinary(toEncrypt, BinaryStringEncoding.Utf8);

                // Open a symmetric algorithm provider for the specified algorithm.
                var aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);

                //Since all hashing functions have fixed length some algorithms do not suit to be used as key hashers
                //ie SHA1 will throw an error since its size is 160 bit while Aes requires 16 byte block size (or multiples of 16)
                if (keyHash.Length % aes.BlockLength != 0)
                {
                    //if SHA1 used for encryption please not its block size is 160 bit
                    throw new ArgumentException("Hash function hashed with invalid key block size: " + keyHash.Length);
                }
                // Create a symmetric key.
                var symetricKey = aes.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(keyHash));

                // The input key must be securely shared between the sender of the cryptic message
                // and the recipient. The initialization vector must also be shared but does not
                // need to be shared in a secure manner. If the sender encodes a message string
                // to a buffer, the binary encoding method must also be shared with the recipient.
                var buffEncrypted = CryptographicEngine.Encrypt(symetricKey, toDecryptBuffer, null);

                // Convert the encrypted buffer to a string (for display).
                // We are using Base64 to convert bytes to string since you might get unmatched characters
                // in the encrypted buffer that we cannot convert to string with UTF8.
                var strEncrypted = CryptographicBuffer.EncodeToBase64String(buffEncrypted);

                return strEncrypted;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception("[EncryptionProvider] Error Encrypting a string");
            }
#else
            throw new System.PlatformNotSupportedException();
#endif
        }
        /// <summary>
        /// Hashes given string using specified algorithm.
        /// </summary>
        /// <param name="toHash"> String to be hashed. </param>
        /// <param name="type"> Type of Hash function to be used. </param>
        /// <param name="hashKey"> optional key to be used with HMACSHA256 algorithm </param>
        /// <returns></returns>
        private static byte[] Hash(string toHash, HashFunctionType type = HashFunctionType.MD5, byte[] hashKey = null)
        {
#if NETFX_CORE
            byte[] keyByteArray = System.Text.Encoding.UTF8.GetBytes(toHash);
            switch (type)
            {
                case HashFunctionType.MD5:
                    var md5Algorithm = MD5.Create();
                    return md5Algorithm.ComputeHash(keyByteArray);
                case HashFunctionType.SHA1:
                    var sha1Algorithm = SHA1.Create();
                    return sha1Algorithm.ComputeHash(keyByteArray);
                case HashFunctionType.HMACSHA256:
                    HMACSHA256 hmac = hashKey != null ?
                        new HMACSHA256(hashKey) : new HMACSHA256();
                    return hmac.ComputeHash(keyByteArray);
                default:
                    System.Diagnostics.Debug.WriteLine("Unrecognized Hash function type: " + type);
                    throw new ArgumentException("Unrecognized Hash function type: " + type);
            }
#else
            throw new System.PlatformNotSupportedException();
#endif
        }


        /// <summary>
        /// Decrypt a string using dual encryption method. Return a Decrypted clear string
        /// </summary>
        /// <param name="cipherString">Encrypted string</param>
        /// <param name="key">Unique key for encryption/decryption</param>
        /// <param name="type"> Type of Hash function to be used. </param>
        /// <param name="hashKey"> optional key to be used with HMACSHA256 algorithm </param>
        /// <returns>Returns decrypted text.</returns>
        public static string Decrypt(string cipherString, string key, HashFunctionType type = HashFunctionType.MD5, byte[] hashKey = null)
        {
#if NETFX_CORE
            try
            {
                byte[] keyHash = Hash(key, type, hashKey);

                // Create a buffer that contains the encoded message to be decrypted.
                IBuffer toDecryptBuffer = CryptographicBuffer.DecodeFromBase64String(cipherString);

                // Open a symmetric algorithm provider for the specified algorithm.
                SymmetricKeyAlgorithmProvider aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);

                //Since all hashing functions have fixed length some algorithms do not suit to be used as key hashers
                //ie SHA1 will throw an error since its size is 160 bit while Aes requires 16 byte block size (or multiples of 16)
                if (keyHash.Length % aes.BlockLength != 0)
                {
                    //if SHA1 used for encryption please not its block size is 160 bit
                    throw new ArgumentException("Hash function hashed with invalid key block size: " + keyHash.Length);
                }
                // Create a symmetric key.
                var symetricKey = aes.CreateSymmetricKey(CryptographicBuffer.CreateFromByteArray(keyHash));

                var buffDecrypted = CryptographicEngine.Decrypt(symetricKey, toDecryptBuffer, null);

                string strDecrypted = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffDecrypted);

                return strDecrypted;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception("[EncryptionProvider] Error Decrypting a string");
            }
#else
            throw new System.PlatformNotSupportedException();
#endif
        }
    }

}
