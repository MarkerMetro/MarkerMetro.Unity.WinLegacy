using System;
using MarkerMetro.Unity.WinLegacy.Timers;

#if NETFX_CORE
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Timers.Tests
{
    [TestClass]
    public class TimerTests
    {
#if NETFX_CORE

        /// <summary>
        /// Test Timer with valid interval value.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroTimer_ValidIntervalValue_Succeed()
#else
        public void WP8Timer_ValidIntervalValue_Succeed()
#endif
        {
            bool success = false;
            string error = String.Empty;
            try
            {
                Timer timer = new Timer(100);
                timer.Start();
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test Timer with invalid interval value.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroTimer_IntervalLessThanZero_Fail()
#else
        public void WP8Timer_IntervalLessThanZero_Fail()
#endif
        {
            bool success = false;
            string error = String.Empty;
            try
            {
                Timer timer = new Timer(-100);
                timer.Start();
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Interval less than or equal to 0 should throw exception.");
        }
#endif
    }
}
