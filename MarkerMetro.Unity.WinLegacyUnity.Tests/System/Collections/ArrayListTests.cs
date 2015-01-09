using System;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Collections.Tests
{
    [TestClass]
    public class ArrayListTests
    {

#if !(NETFX_CORE || WINDOWS_PHONE)
        /// <summary>
        /// Test WinLegacy ArrayList and compare behaviour with System ArrayList.
        /// </summary>
        [TestMethod]
        public void ArrayList_CompareWithSystemArrayList_ShouldBehaveSame()
        {
            var sysArrayList = new System.Collections.ArrayList();
            var mmArrayList = new MarkerMetro.Unity.WinLegacy.Collections.ArrayList();

            sysArrayList.Add("one");
            sysArrayList.Add(2);
            sysArrayList.Add("three");

            mmArrayList.Add("one");
            mmArrayList.Add(2);
            mmArrayList.Add("three");

            CollectionAssert.AreEquivalent(sysArrayList.ToArray(), mmArrayList.ToArray(), "System ArrayList and WinLegacy ArrayList is different.");
        }
#endif
    }

}
