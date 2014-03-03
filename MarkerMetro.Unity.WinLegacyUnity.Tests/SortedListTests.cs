using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarkerMetro.Unity.WinLegacyUnity.Tests
{
    [TestClass]
    public class SortedListTests
    {
        [TestMethod]
        public void IntegerSortedList_ShouldBehaveSame()
        {
            var sysList = new System.Collections.SortedList();
            var mmList = new MarkerMetro.Unity.WinLegacy.Collections.SortedList();

            sysList.Add(1, "one");
            sysList.Add(0, "zero");
            sysList.Add(2, "two");

            mmList.Add(1, "one");
            mmList.Add(0, "zero");
            mmList.Add(2, "two");

            CollectionAssert.AreEquivalent(sysList.Values, mmList.Values);
        }

        [TestMethod]
        public void IntegerSortedList_SecondEleemntByIndex_ShouldBeTheSame()
        {
            var sysList = new System.Collections.SortedList();
            var mmList = new MarkerMetro.Unity.WinLegacy.Collections.SortedList();

            sysList.Add(1, "one");
            sysList.Add(0, "zero");
            sysList.Add(2, "two");

            mmList.Add(1, "one");
            mmList.Add(0, "zero");
            mmList.Add(2, "two");

            Assert.AreEqual(sysList.GetByIndex(1), mmList.GetByIndex(1));
        }
    }
}
