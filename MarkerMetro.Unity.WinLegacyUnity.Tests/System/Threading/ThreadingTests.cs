using System;
using MarkerMetro.Unity.WinLegacy.Threading;

#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Threading.Tests
{
    [TestClass]
    public class ThreadingTests
    {
#if NETFX_CORE
        /// <summary>
        /// Test Threading, should start and terminate new thread successfully.
        /// </summary>
        [TestMethod]
        public void MetroThread_StartAndTerminate_Succeed()
        {
            bool success = false;
            string error = String.Empty;
            try
            {
                bool shouldStop = false;
                bool newThreadStopped = false;

                Thread newThread = new Thread(() =>
                {
                    while (!shouldStop) ;

                    newThreadStopped = true;
                });
                newThread.Start();
                while (!newThread.IsAlive) ;
                Thread.Sleep(10);
                shouldStop = true;

                newThread.Join(10);

                success = newThreadStopped;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test ThreadPool.
        /// </summary>
        [TestMethod]
        public void MetroThreadPool_QueueUserWorkItem_Succeed()
        {
            bool success = false;
            string error = String.Empty;
            int finishedThreads = 0;
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    // Start threads using ThreadPool.
                    ThreadPool.QueueUserWorkItem(() =>
                    {
                        int sum = 0;
                        for (int j = i * 100; j > 1; j--)
                        {
                            sum += j;
                        }
                        finishedThreads++;
                    });
                }

                // Wait until all threads in ThreadPool are finished.
                while (finishedThreads < 10) ;

                success = finishedThreads == 10;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }
#endif
    }
}
