using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Collections
{
    public class SortedList : SortedDictionary<object, object>
    {
        public virtual object GetByIndex(int index)
        {
            if(index<0 && index>base.Count)
                throw new ArgumentOutOfRangeException();

            return base.Values.Skip(index).First();
        }
    }
}
