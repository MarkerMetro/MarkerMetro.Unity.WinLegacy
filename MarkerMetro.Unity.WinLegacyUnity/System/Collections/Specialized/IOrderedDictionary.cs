using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

namespace MarkerMetro.Unity.WinLegacy.Collections.Specialized
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.collections.specialized.iordereddictionary.aspx.
    /// </summary>
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable {
		
		Object this[int index] { get; set; }
		
		new IDictionaryEnumerator GetEnumerator();
		void Insert(int index, Object key, Object value);
		void RemoveAt(int index);
	}
}
