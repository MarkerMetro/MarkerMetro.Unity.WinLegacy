using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Net
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.net.webheadercollection.aspx.
    /// </summary>
    public class WebHeaderCollection : MarkerMetro.Unity.WinLegacy.Plugin.Collections.Specialized.NameValueCollection 
    {
        readonly System.Net.WebHeaderCollection _actual;
        internal WebHeaderCollection(System.Net.WebHeaderCollection actual)
        {
            if (actual == null)
                throw new ArgumentNullException("actual", "actual is null.");

            _actual = actual;
        }

        public override void Add(string name, string value)
        {
#if NETFX_CORE
            _actual[name] = value;
#else
            _actual.Add(name, value);
#endif
        }

        public override string[] AllKeys
        {
            get { return _actual.AllKeys; }
        }

        public override void Clear()
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            _actual.Clear();
#endif
        }

        public override string Get(int index)
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            return _actual.Get(index);
#endif
        }

        public override string Get(string name)
        {
#if NETFX_CORE
            return _actual[name];
#else
            return _actual.Get(name);
#endif
        }

        public override string GetKey(int index)
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            return _actual.GetKey(index);
#endif
        }

        public override string[] GetValues(int index)
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            return _actual.GetValues(index);
#endif
        }

        public override string[] GetValues(string name)
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            return _actual.GetValues(name);
#endif
        }

        public override void Remove(string name)
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            _actual.Remove(name);
#endif
        }

        public override void Set(string name, string value)
        {
#if NETFX_CORE
            _actual[name] = value;
#else
            _actual.Set(name, value);
#endif
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
#if NETFX_CORE
            throw new PlatformNotSupportedException();
#else
            return _actual.GetEnumerator();
#endif
        }
    }
}
