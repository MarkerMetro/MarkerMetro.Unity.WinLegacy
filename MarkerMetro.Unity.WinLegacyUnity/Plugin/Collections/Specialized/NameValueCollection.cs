using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Plugin.Collections.Specialized
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.collections.specialized.namevaluecollection.aspx.
    /// </summary>
    public class NameValueCollection : IEnumerable
    {
        IDictionary<string, string> _collection;

        public NameValueCollection()
        {
            IsReadOnly = false;

            Init();
        }

        public NameValueCollection(NameValueCollection col) : this()
        {
            if (col == null)
                throw new ArgumentNullException("col");

            Add(col);
        }

        void Init()
        {
            if(_collection!=null)
            {
                _collection.Clear();
                _collection = null;
            }

            _collection = new Dictionary<string, string>();
        }

        public bool IsReadOnly { get; set; }

        public virtual string[] AllKeys
        {
            get { return _collection.Keys.ToArray(); }
        }

        public string this[int index]
        {
            get { return Get(index); }
        }

        public string this[string name]
        {
            get { return Get(name); }
            set { Set(name, value); }
        }

        public int Count
        {
            get { return AllKeys.Length; }
        }

        public void Add(NameValueCollection c)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Collection is read-only");
            if (c == null)
                throw new ArgumentNullException("c");

            foreach (var key in c._collection.Keys)
                Add(key, c._collection[key]);
        }

        public virtual void Add(string name, string value)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Collection is read-only");

            _collection.Add(name, value);
        }

        public virtual void Clear()
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Collection is read-only");

            Init();
        }

        public void CopyTo(Array dest, int index)
        {
            if (dest == null)
                throw new ArgumentNullException("dest", "Null argument - dest");
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "index is less than 0");
            if (dest.Rank > 1)
                throw new ArgumentException("dest", "multidim");

            try
            {
                GetValues(0).ToArray().CopyTo(dest, index);
            }
            catch (ArrayTypeMismatchException)
            {
                throw new InvalidCastException();
            }
        }
        
        public virtual string Get(int index)
        {
            return _collection.Skip(index).First().Value;
        }

        public virtual string Get(string name)
        {
            return _collection[name];
        }

        public virtual string GetKey(int index)
        {
            return _collection.Keys.Skip(index).First();
        }

        public virtual string[] GetValues(int index)
        {
            return _collection.Values.Skip(index).ToArray();
        }


        public virtual string[] GetValues(string name)
        {
            throw new NotSupportedException();
        }

        public bool HasKeys()
        {
            return AllKeys.Any();
        }

        public virtual void Remove(string name)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Collection is read-only");

            _collection.Remove(name);
        }

        public virtual void Set(string name, string value)
        {
            if (this.IsReadOnly)
                throw new NotSupportedException("Collection is read-only");

            _collection[name] = value;
        }

        public virtual IEnumerator GetEnumerator()
        {
            return _collection.GetEnumerator();
        }
        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string,string>(_collection);
        }
    }
}
