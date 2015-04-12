using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class ReflectionTests
    {

        [TestMethod]
#if NETFX_CORE
        public void Metro_GetInterface_GenericDictionary_ReturnForGenericIDictionary()
#else
        public void GetInterface_GenericDictionary_ReturnForGenericIDictionary()
#endif
        {
            var dict = new Dictionary<string, ReflectionTests>();
            var actual = dict.GetType().GetInterface("System.Collections.Generic.IDictionary`2", true);

            Assert.IsNotNull(actual, "Must find generic IDictionary");
        }

        [TestMethod]
        public void Metro_GetProperties_OnDerivedType_ShouldReturnUnionOfProperties()
        {
            var actual = typeof(DerivedClass).GetProperties();
            Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void Metro_GetFields_OnDerivedType_ShouldReturnUnionOfProperties()
        {
            var actual = typeof(DerivedClass).GetFields();
#if NETFX_CORE
            // WinRT returns fields for the backing values for auto properties 
            Assert.AreEqual(3, actual.Length);
#else
            Assert.AreEqual(2, actual.Length);
#endif
        }

        [TestMethod]
#if NETFX_CORE
        public void Metro_GetCustomAttributes_NoInherit()
#else
        public void GetCustomAttributes_NoInherit()
#endif
        {
            var customAttrs = typeof(DerivedClass).GetCustomAttributes(false);
            Assert.IsTrue(customAttrs.Count() == 0);
        }

        [TestMethod]
#if NETFX_CORE
        public void Metro_GetCustomAttributes_Inherit()
#else
        public void GetCustomAttributes_Inherit()
#endif
        {
            var customAttrs = typeof(DerivedClass).GetCustomAttributes(true);
            Assert.IsTrue(customAttrs.Count() == 1);
        }

        [TestMethod]
#if NETFX_CORE
        public void Metro_GetCustomAttributesOfType_NoInherit()
#else
        public void GetCustomAttributesOfType_NoInherit()
#endif
        {
            var customAttrs = typeof(DerivedClass).GetCustomAttributes(typeof(TestAttribute), false);
            Assert.IsTrue(customAttrs.Count() == 0);
        }

        [TestMethod]
#if NETFX_CORE
        public void Metro_GetCustomAttributesOfType_Inherit()
#else
        public void GetCustomAttributesOfType_Inherit()
#endif
        {
            var customAttrs = typeof(DerivedClass).GetCustomAttributes(typeof(TestAttribute), true);
            Assert.IsTrue(customAttrs.Count() == 1);
        }

        [TestAttribute]
        abstract class BaseClass
        {
            [TestAttribute]
            public string BaseProperty { get; set; }

            public string BaseField;
        }

        class DerivedClass : BaseClass
        {
            public string DerivedProperty { get; set; }


            public string DerivedField;
        }

        public class TestAttribute : System.Attribute
        {

        }
    }
}
