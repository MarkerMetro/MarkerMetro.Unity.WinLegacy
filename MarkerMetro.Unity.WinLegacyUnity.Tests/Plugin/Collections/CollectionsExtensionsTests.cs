using System;
using System.Collections.Generic;
using MarkerMetro.Unity.WinLegacy.Plugin.Collections;
using MarkerMetro.Unity.WinLegacy.Collections;
#if NETFX_CORE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Plugin.Collections.Tests
{
    [TestClass]
    public class CollectionsExtensionsTests
    {
        /// <summary>
        /// Test ConvertAll List Extension method with int to string converter.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroCollectionsExtensionsListConvertAll_IntToStringConverter_Succeed()
#else
        public void CollectionsExtensionsListConvertAll_IntToStringConverter_Succeed()
#endif
        {
            List<int> intList = new List<int>();
            List<string> stringList = intList.ConvertAll<int, string>((i) =>
            {
                return i.ToString();
            });
        }

        /// <summary>
        /// Test ConvertAll List Extension method with null converter.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroCollectionsExtensionsListConvertAll_NullConverter_Fail()
#else
        public void CollectionsExtensionsListConvertAll_NullConverter_Fail()
#endif
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                List<int> intList = new List<int>();
                List<string> stringList = intList.ConvertAll<int, string>(null);

                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Should catch exception with null converter.");
        }
    }
}
