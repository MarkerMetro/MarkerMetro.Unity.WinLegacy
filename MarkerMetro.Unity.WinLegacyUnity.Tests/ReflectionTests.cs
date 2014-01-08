using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MarkerMetro.Unity.WinLegacyUnity.Tests
{
    [TestClass]
    public class ReflectionTests
    {
        abstract class BaseClass
        {
            public string BaseProperty { get; set; }
        }

        class DerivedClass : BaseClass
        {
            public string DerivedProperty { get; set; }
        }

        [TestMethod]
        public void GetProperties_OnDerivedType_ShouldReturnUnionOfProperties()
        {
            var actual = typeof(DerivedClass).GetProperties();

            Assert.AreEqual(2, actual.Length);
        }
    }
}
