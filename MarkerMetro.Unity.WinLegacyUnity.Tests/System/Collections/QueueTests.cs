using System;
#if NETFX_CORE || WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Collections.Tests
{
    [TestClass]
    public class QueueTests
    {

#if !(NETFX_CORE || WINDOWS_PHONE)
        /// <summary>
        /// Test WinLegacy Queue.Enqueue and Queue.Dequeue and compare behaviour with System Queue.
        /// </summary>
        [TestMethod]
        public void QueueEnqueueDequeue_CompareWithSystemQueue_ShouldBehaveSame()
        {
            var sysQueue = new System.Collections.Queue();
            var mmQueue = new MarkerMetro.Unity.WinLegacy.Collections.Queue();

            sysQueue.Enqueue(1);
            sysQueue.Enqueue("two");
            sysQueue.Enqueue(3);

            mmQueue.Enqueue(1);
            mmQueue.Enqueue("two");
            mmQueue.Enqueue(3);

            CollectionAssert.AreEquivalent(sysQueue.ToArray(), mmQueue.ToArray(), "System Queue and WinLegacy Queue is different.");
            Assert.AreEqual(sysQueue.Dequeue(), mmQueue.Dequeue(), "Dequeue should return the same value.");
        }
#endif
    }

}
