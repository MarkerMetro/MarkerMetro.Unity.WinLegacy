using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void GetInterface_GenericDictionary_ReturnForGenericIDictionary()
        {
            var dict = new Dictionary<string, ReflectionTests>();

            var actual = dict.GetType().GetInterface("System.Collections.Generic.IDictionary`2");

            Assert.IsNotNull(actual, "Must find generic IDictionary");
        }

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
