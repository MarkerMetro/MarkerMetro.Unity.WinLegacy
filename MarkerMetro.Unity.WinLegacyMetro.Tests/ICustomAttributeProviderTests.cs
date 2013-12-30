using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
#pragma warning disable 3021
    [TestClass]
    public class ICustomAttributeProviderTests
    {
        public string _undecorated = "";
        [CLSCompliant(false)]
        public string _decorated = "";

        [TestMethod]
        public void ThisClassToICustomAttributeProvider_Undecorated_Returns0()
        {
            var fieldInfo = GetType().GetField("_undecorated");
            var target = fieldInfo.ToICustomAttributeProvider();

            Assert.AreEqual(0, target.GetCustomAttributes(false).Length);
        }

        [TestMethod]
        public void ThisClassToICustomAttributeProvider_Decorated_Returns1()
        {
            var fieldInfo = GetType().GetField("_decorated");
            var target = fieldInfo.ToICustomAttributeProvider();

            Assert.AreEqual(1, target.GetCustomAttributes(false).Length);
        }

        [TestMethod]
        public void ThisClassToICustomAttributeProvider_ThisMethod_Returns1()
        {
            var memberInfo = GetType().GetMethod("ThisClassToICustomAttributeProvider_ThisMethod_Returns1");
            var target = memberInfo.ToICustomAttributeProvider();

            Assert.AreEqual(1, target.GetCustomAttributes(false).Length);
        }
    }
}