using System;
using System.IO;
using MarkerMetro.Unity.WinLegacy.Plugin.IO;

#if NETFX_CORE
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Plugin.IO.Tests
{
    [TestClass]
    public class DirectoryTests
    {
#if NETFX_CORE
        StorageFolder localFolder;
        bool success;
        string error;

        [TestInitialize]
        public void TestInitialize()
        {
            success = false;
            error = string.Empty;
            localFolder = ApplicationData.Current.LocalFolder;
        }


#if NETFX_CORE
        /// <summary>
        /// Test Directory.GetFiles on root directory.
        /// </summary>
        [TestMethod]
        public void MetroDirectoryGetFiles_RootDirectory_Succeed()
        {
            string[] files = Directory.GetFiles(localFolder.Path);
        }

        /// <summary>
        /// Test Directory.GetFiles on sub directory.
        /// </summary>
        [TestMethod]
        public void MetroDirectoryGetFiles_SubDirectory_Succeed()
        {
            string destPath = Path.Combine(localFolder.Path, @"SubFolder1\SubFolder2\");

            string[] files = Directory.GetFiles(destPath);
        }

        /// <summary>
        /// Test Directory.GetFiles with searchPattern.
        /// </summary>
        [TestMethod]
        public void MetroDirectoryGetFiles_WithSearchPattern_Succeed()
        {
            bool success = false;

            string newFile = Path.Combine(localFolder.Path, @"File.txt");
            StreamWriter sw = File.CreateText(newFile);
            sw.Close();

            string[] files = Directory.GetFiles(localFolder.Path, "File");
            success = files.Length > 0;

            Assert.IsTrue(success, "Should find the file just created.");
        }
#endif

        /// <summary>
        /// Test Directory.Exists on existing directory.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryExists_ExistingDirectory_Succeed()
#else
        public void WP8DirectoryExists_ExistingDirectory_Succeed()
#endif
        {
            Assert.IsTrue(Directory.Exists(localFolder.Path + "\\"), "LocalFolder should exist.");
        }

        /// <summary>
        /// Test Directory.Exists on directory that doesn't exist.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryExists_NonExistingDirectory_Fail()
#else
        public void WP8DirectoryExists_NonExistingDirectory_Fail()
#endif
        {
            string destPath = Path.Combine(localFolder.Path, @"SubFolder12\SubFolder23\");

            try
            {
                success = Directory.Exists(destPath);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Directory.Exists returned true on directory that doesn't exist.");
        }

        /// <summary>
        /// Test Directory.CreateDirectory.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryCreateDirectory_SubDirectory_Succeed()
#else
        public void WP8DirectoryCreateDirectory_SubDirectory_Succeed()
#endif
        {
            string destPath = Path.Combine(localFolder.Path, @"SubFolder1\SubFolder2\");

            Directory.CreateDirectory(destPath);
            Assert.IsTrue(Directory.Exists(destPath), "Directory just created should exist.");
        }

        /// <summary>
        /// Test Directory.Delete on existing directory.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryDelete_ExistingDirectory_Succeed()
#else
        public void WP8DirectoryDelete_ExistingDirectory_Succeed()
#endif
        {
            string destPath = Path.Combine(localFolder.Path, @"SubFolder11\SubFolder22\");

            Directory.CreateDirectory(destPath);
            Directory.Delete(destPath);
            Assert.IsFalse(Directory.Exists(destPath), "Directory just deleted shouldn't exist.");
        }

        /// <summary>
        /// Test Directory.Delete on directory that doesn't exist.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryDelete_NonExistingDirectory_Fail()
#else
        public void WP8DirectoryDelete_NonExistingDirectory_Fail()
#endif
        {
            string destPath = Path.Combine(localFolder.Path, @"SubFolder111\SubFolder222\");

            try
            {
                Directory.Delete(destPath);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "Directory.Delete on directory that doesn't exist should throw exception.");
        }

        /// <summary>
        /// Test DirectoryInfo Constructor on existing directory.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryInfoConstructor_ExistingDirectory_Succeed()
#else
        public void WP8DirectoryInfoConstructor_ExistingDirectory_Succeed()
#endif
        {
            var directoryInfo = new DirectoryInfo(localFolder.Path);

            Assert.IsTrue(directoryInfo != null);
        }

        /// <summary>
        /// Test DirectoryInfo Constructor on directory that doesn't exist.
        /// </summary>
        [TestMethod]
#if NETFX_CORE
        public void MetroDirectoryInfoConstructor_NonExistingDirectory_Fail()
#else
        public void WP8DirectoryInfoConstructor_NonExistingDirectory_Fail()
#endif
        {
            string destPath = Path.Combine(localFolder.Path, @"SubFolder123\SubFolder234\");
            var directoryInfo = new DirectoryInfo(destPath);
            Assert.IsFalse(directoryInfo.Exists, "directoryInfo.Exists returned true on directory that doesn't exist.");
        }
#endif
    }
}
