
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

namespace MarkerMetro.Unity.WinLegacy.Plugin.Collections
{
    /**
     * An ArrayList is just a dynamic array of generic objects... very close to List<> which is supported in Metro.
     */
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.collections.arraylist.aspx.
    /// </summary>
    public class ArrayList : List<object>
    {
        public global::System.Array ToArray(global::System.Type elementType)
        {
            var array = global::System.Array.CreateInstance(elementType, Count);
            global::System.Array.Copy(ToArray(), array, Count);

            return array;
        }

        public ArrayList() { }
        public ArrayList(IEnumerable enumerable) : base(enumerable.Cast<object>()) { }

        /**
         * ArrayList.Clone just creates a shallow copy of the ArrayList.
         */
        public virtual Object Clone()
        {
            return new ArrayList(this);
        }

        public virtual void Sort(IComparer comparer)
        {
            base.Sort(new ArrayListComparer(comparer));
        }

        private struct ArrayListComparer : IComparer<object>
        {
            private IComparer comparer;
            public ArrayListComparer(IComparer comparer) 
            {
                this.comparer = comparer;
            }
            public int Compare(Object x, Object y)
            {
                return comparer.Compare(x, y);
            }
        }
    }
}