using MarkerMetro.Unity.WinLegacy.Collections.Specialized;
using System;

#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Collections.Specialized.Tests
{
    /// <summary>
    /// Test Generic implementations of OrderedDictionary.
    /// </summary>
    [TestClass]
    public class OrderedDictionaryTests
    {
        [TestMethod]
#if NETFX_CORE
        public void MetroOrderedDictionary_BasicOperations_Succeed()
#else
        public void OrderedDictionary_BasicOperations_Succeed()
#endif
        {
            OrderedDictionary<int, string> orderedDictionary = new OrderedDictionary<int, string>();

            Assert.IsTrue(orderedDictionary.Count == 0, "Count should be 0 by default.");

            orderedDictionary.Add(3, "Three");
            orderedDictionary.Add(1, "One");
            orderedDictionary.Add(2, "Two");

            Assert.IsTrue(orderedDictionary.ContainsKey(1), "ContainsKey should be true for keys that exist in the dictionary.");
            Assert.IsFalse(orderedDictionary.ContainsKey(5), "ContainsKey should be false for keys that doesn't exist in the dictionary.");
            Assert.IsTrue(orderedDictionary.ContainsValue("One"), "ContainsValue should be true for values that exist in the dictionary.");
            Assert.IsFalse(orderedDictionary.ContainsValue("Five"), "ContainsValue should be false for values that doesn't exist in the dictionary.");

            orderedDictionary.SortKeys();

            Assert.IsTrue(orderedDictionary.IndexOf(1) == 0, "SortKeys failed to sort by keys correctly.");

            orderedDictionary.Insert(0, 4, "Four");

            Assert.IsTrue(orderedDictionary.IndexOf(4) == 0, "Insert should insert the element to specified index.");

            orderedDictionary.Clear();

            Assert.IsTrue(orderedDictionary.Count == 0, "Clear failed to remove all elements in the dictionary.");
        }

        [TestMethod]
#if NETFX_CORE
        public void MetroOrderedDictionary_GetNonExistingValue_Fail()
#else
        public void OrderedDictionary_GetNonExistingValue_Fail()
#endif
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                OrderedDictionary<int, string> orderedDictionary = new OrderedDictionary<int, string>();

                if (orderedDictionary[7] == "Seven") {}

                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Should catch exception trying to access value doesn't exist in the dictionary.");
        }
    }
}
