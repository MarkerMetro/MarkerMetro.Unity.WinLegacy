using MarkerMetro.Unity.WinLegacy.Collections.Specialized;
using System;

#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Collections.Specialized.Tests
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
#if NETFX_CORE
            public void MetroNameValueCollection_Count_IsZero()
#elif WINDOWS_PHONE
            public void WP8NameValueCollection_Count_IsZero()
#else
            public void NameValueCollection_Count_IsZero()
#endif
            {
                Assert.AreEqual(0, target.Count);
            }

            [TestMethod]
#if NETFX_CORE
            public void MetroNameValueCollection_HasKeys_IsFalse()
#elif WINDOWS_PHONE
            public void WP8NameValueCollection_HasKeys_IsFalse()
#else
            public void NameValueCollection_HasKeys_IsFalse()
#endif
            {
                Assert.IsFalse(target.HasKeys());
            }
        }
    }
}
