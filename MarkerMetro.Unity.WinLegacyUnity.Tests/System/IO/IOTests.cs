using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkerMetro.Unity.WinLegacy.IO;

using System.Threading.Tasks;
using System.IO;

#if NETFX_CORE || WINDOWS_PHONE
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.Reflection.Tests
{
    [TestClass]
    public class IOTests
    {

#if NETFX_CORE

        [TestMethod]
        public void Metro_FileInfo_Constructor_Success()
        {
            // create a file
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;

            // create a file
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                };
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            var fileInfo = new FileInfo(path);

            Assert.IsTrue(fileInfo != null && fileInfo.Length > 0);

        }

        [TestMethod]
        public void Metro_FileInfo_Constructor_NotFound_Failure()
        {
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = Path.Combine(localFolder.Path, @"FileNotFound.txt");
            var fileInfo = new FileInfo(path);
            Assert.IsFalse(fileInfo.Exists);
        }

        [TestMethod]
        public void Metro_File_MoveToSubFolder_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;

            // create a file
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                };
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            string destPath = Path.Combine(localFolder.Path, @"SubFolder1\SubFolder2\FileNew.txt");

            // ensure directory exists, will fail otherwise
            Directory.CreateDirectory(Path.GetDirectoryName(destPath));

            try
            {
                File.Move(path, destPath);
                File.Open(destPath);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success);

        }
   
        public void Metro_File_MoveToSubFolder_NotExists_Failure()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;

            // create a file
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                };
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            string destPath = Path.Combine(localFolder.Path, @"SubFolder1\SubFolder2\FileNew.txt");

            // the destination does not exist and so should fail

            try
            {
                File.Move(path, destPath);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Metro_File_CreateText_RootFile_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                };
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Metro_File_CreateText_SubFolder_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = localFolder.Path + @"\fwgsqduw.jh4/out/" + @"File.txt";
            
            try
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                };
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Metro_File_WriteAllText_RootFile_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                File.WriteAllText(path, DateTime.Now.Ticks.ToString());
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Metro_File_WriteAllText_SubFoldersAndFile_Success()
        {
            bool success = false;
            string error = String.Empty;
            var localFolder = ApplicationData.Current.LocalFolder;
            string path = Path.Combine(localFolder.Path, @"Test\Test2\Test3\Test4\Test5\File.txt");
            try
            {
                File.WriteAllText(path, DateTime.Now.Ticks.ToString());
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success);
        }

#endif

        abstract class BaseClass
        {
            public string BaseProperty { get; set; }
        }

        class DerivedClass : BaseClass
        {
            public string DerivedProperty { get; set; }
        }

    }
}
