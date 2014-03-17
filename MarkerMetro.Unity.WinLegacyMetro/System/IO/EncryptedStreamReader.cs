using MarkerMetro.Unity.WinLegacy.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.Store;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    internal class EncryptedStreamReader : MarkerMetro.Unity.WinLegacy.IO.StreamReader
    {
        public EncryptedStreamReader(Stream stream)
            : base(stream)
        {
        }

        public override string ReadToEnd()
        {
            var sb = new StringBuilder();

            string line;
            try
            {
                while ((line = ReadLine()) != null)
                {
                    sb.Append(EncryptionProvider.Decrypt(line, CurrentApp.AppId.ToString()));
                }
            }
            catch
            {
            }

            return sb.ToString();
        }
    }
}
