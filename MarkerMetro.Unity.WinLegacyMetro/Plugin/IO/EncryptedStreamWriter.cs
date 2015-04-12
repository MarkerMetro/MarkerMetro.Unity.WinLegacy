using MarkerMetro.Unity.WinLegacy.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel.Store;

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    internal class EncryptedStreamWriter : MarkerMetro.Unity.WinLegacy.Plugin.IO.StreamWriter
    {
        public EncryptedStreamWriter(Stream str)
            : base(str)
        {
        }

        public override void WriteLine(string value)
        {
            try
            {
                value = EncryptionProvider.Encrypt(value, CurrentApp.AppId.ToString());

                base.WriteLine(value);
            }
            catch
            {
            }
        }
    }
}
