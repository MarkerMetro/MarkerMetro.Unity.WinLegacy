using MarkerMetro.Unity.WinLegacy.Plugin.Collections.Specialized;
using System;

#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Plugin.Collections.Specialized.Tests
{
    [TestClass]
    public class NameValueCollectionTests
    {
        [TestClass]
        public class DefaultConstructor
        {
            NameValueCollection target;

            [TestInitialize]
            public void Initialize()
            {
                target = new NameValueCollection();


            }

            [TestMethod]
            public void MetroNameValueCollection_Count_IsZero()
            {
                Assert.AreEqual(0, target.Count);
            }

            [TestMethod]
            public void MetroNameValueCollection_HasKeys_IsFalse()
            {
                Assert.IsFalse(target.HasKeys());
            }

            [TestMethod]
            public void MetroNameValueCollection_ToDictionary_Converted()
            {
                target.Add("FirstKey", "First Value");
                target.Add("SecondKey", "Second Value");
                Assert.IsTrue(target.ToDictionary()["FirstKey"] == "First Value");
                Assert.IsTrue(target.ToDictionary()["SecondKey"] == "Second Value");

            }
        }
    }
}
