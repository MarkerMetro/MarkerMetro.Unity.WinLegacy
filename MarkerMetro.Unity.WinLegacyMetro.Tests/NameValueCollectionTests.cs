using MarkerMetro.Unity.WinLegacy.Collections.Specialized;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MarkerMetro.Unity.WinLegacyMetro.Tests
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
            public void Count_IsZero()
            {
                Assert.AreEqual(0, target.Count);
            }

            [TestMethod]
            public void HasKeys_IsFalse()
            {
                Assert.IsFalse(target.HasKeys());
            }
        }
    }
}
