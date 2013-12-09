using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    internal class EncryptedStreamWriter : System.IO.StreamWriter
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
            catch { }
        }
    }
}