using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Net
{
    public class WebHeaderCollection : MarkerMetro.Unity.WinLegacy.Collections.Specialized.NameValueCollection 
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
            _actual.Add(name, value);
        }

        public override string[] AllKeys
        {
            get { return _actual.AllKeys; }
        }

        public override void Clear()
        {
            _actual.Clear();
        }

        public override string Get(int index)
        {
            return _actual.Get(index);
        }

        public override string Get(string name)
        {
            return _actual.Get(name);
        }

        public override string GetKey(int index)
        {
            return _actual.GetKey(index);
        }

        public override string[] GetValues(int index)
        {
            return _actual.GetValues(index);
        }

        public override string[] GetValues(string name)
        {
            return _actual.GetValues(name);
        }

        public override void Remove(string name)
        {
            _actual.Remove(name);
        }

        public override void Set(string name, string value)
        {
            _actual.Set(name, value);
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return _actual.GetEnumerator();
        }
    }
}
