using System;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Collections.Tests
{
    [TestClass]
    public class HashtableTests
    {

#if !(NETFX_CORE)
        /// <summary>
        /// Test WinLegacy Hashtable and compare behaviour with System Hashtable.
        /// </summary>
        [TestMethod]
        public void Hashtable_CompareWithSystemHashtable_ShouldBehaveSame()
        {
            var sysHashtable = new System.Collections.Hashtable();
            var mmHashtable = new MarkerMetro.Unity.WinLegacy.Collections.Hashtable();

            sysHashtable.Add(1, "one");
            sysHashtable.Add(2, "two");
            sysHashtable.Add(3, "three");

            mmHashtable.Add(1, "one");
            mmHashtable.Add(2, "two");
            mmHashtable.Add(3, "three");

            CollectionAssert.AreEquivalent(sysHashtable.Values, mmHashtable.Values, "System Hashtable and WinLegacy Hashtable is different.");
        }
#endif
    }

}
