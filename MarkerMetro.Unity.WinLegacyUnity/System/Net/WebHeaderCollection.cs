using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace MarkerMetro.Unity.WinLegacy.Net
{
#if UNITY_METRO
    [SerializableAttribute]
    [ComVisible(true)]
    public class WebHeaderCollection : NameValueCollection, ISerializable
    {
        public override void Set(string name, string value)
        {
            throw new NotImplementedException();
        }
    }
#endif
}
