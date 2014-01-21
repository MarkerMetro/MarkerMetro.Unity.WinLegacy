using System;
using System.Runtime.Serialization;

namespace MarkerMetro.Unity.WinLegacy.Net
{
#if UNITY_METRO
    [SerializableAttribute]
    public abstract class WebRequest : MarshalByRefObject, ISerializable
    {
        public virtual int Timeout { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
#endif
}
