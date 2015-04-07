using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MarkerMetro.Unity.WinLegacy.IO;

using System.Threading.Tasks;

#if NETFX_CORE
using Windows.Storage;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO.Tests
{
    [TestClass]
    public class FileTests
    {
#if NETFX_CORE
        bool success;
        string error;
        StorageFolder localFolder;

        [TestInitialize]
        public void TestInitialize()
        {
            success = false;
            error = string.Empty;
            localFolder = ApplicationData.Current.LocalFolder;
        }


        /// <summary>
        /// Test FileInfo constructor on existing file.
        /// </summary>
        [TestMethod]
        public void MetroFileInfoConstructor_ExistingFile_Succeed()
        {
            // create a file.
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

            Assert.IsTrue(fileInfo != null && fileInfo.Length > 0, error);
        }

        /// <summary>
        /// Test FileInfo constructor on file that doesn't exist.
        /// </summary>
        [TestMethod]
        public void MetroFileInfoConstructor_NonExistingFile_Fail()
        {
            string path = Path.Combine(localFolder.Path, @"FileNotFound.txt");
            var fileInfo = new FileInfo(path);
            Assert.IsFalse(fileInfo.Exists, "fileInfo.Exists returned true on file that doesn't exist.");
        }

        /// <summary>
        /// Test File.Move to existing sub folder.
        /// </summary>
        [TestMethod]
        public void MetroFileMove_ToExistingSubFolder_Succeed()
        {
            // create a file.
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

            // ensure directory exists, will fail otherwise.
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

            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test File.Move to sub folder that doesn't exist.
        /// </summary>
        [TestMethod]
        public void MetroFileMove_ToNonExistingSubFolder_Fail()
        {
            // create a file.
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

            // the destination does not exist and so should fail.

            try
            {
                File.Move(path, destPath);
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsFalse(success, "For some reason File.Move to directory that doesn't exist was successful.");
        }

        /// <summary>
        /// Test File.CreateText in root directory.
        /// </summary>
        [TestMethod]
        public void MetroFileCreateText_RootFile_Succeed()
        {
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
            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test File.CreateText in existing sub folder.
        /// </summary>
        [TestMethod]
        public void MetroFileCreateText_SubFolder_Succeed()
        {
            string path = localFolder.Path + @"\fwgsqduw.jh4/out/" + @"File.txt";

            Directory.CreateDirectory(Path.GetDirectoryName(path));

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
            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test File.WriteAllText in root directory.
        /// </summary>
        [TestMethod]
        public void MetroFileWriteAllText_RootFile_Succeed()
        {
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
            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test File.WriteAllText in exisiting sub folder.
        /// </summary>
        [TestMethod]
        public void MetroFileWriteAllText_SubFoldersAndFile_Succeed()
        {
            string path = Path.Combine(localFolder.Path, @"Test\Test2\Test3\Test4\Test5\File.txt");

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            try
            {
                File.WriteAllText(path, DateTime.Now.Ticks.ToString());
                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test File.ReadAllText in root directory.
        /// </summary>
        [TestMethod]
        public void MetroFileReadAllText_RootFile_Succeed()
        {
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                File.WriteAllText(path, DateTime.Now.Ticks.ToString());
                string content = File.ReadAllText(path);
                success = !string.IsNullOrEmpty(content) && content.Length > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }

        /// <summary>
        /// Test FileStream write and read.
        /// </summary>
        [TestMethod]
        public void MetroFileStream_WriteAndRead_Succeed()
        {
            string path = Path.Combine(localFolder.Path, @"File.txt");
            try
            {
                string originalString = "MarkerMetro";
                byte[] writeBuffer = Encoding.UTF8.GetBytes(originalString);
                byte[] readBuffer = new byte[writeBuffer.Length];

                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    Assert.IsTrue(fs.CanWrite);

                    fs.Write(writeBuffer, 0, writeBuffer.Length);

                    fs.Flush();
                }

                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    fs.Read(readBuffer, 0, readBuffer.Length);

                    string readString = Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length);

                    success = readString == originalString;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            Assert.IsTrue(success, error);
        }


        /// <summary>
        /// Test ReadWriteStream begin and end successfully.
        /// </summary>
        [TestMethod]
        public void MetroReadWriteStream_BeginEndRead_Succeed()
        {
            System.IO.Stream readStream = new System.IO.MemoryStream();
            System.IO.Stream writeStream = new System.IO.MemoryStream();
            ReadWriteStream rwStream = new ReadWriteStream(readStream, writeStream);
            IAsyncResult ar = rwStream.BeginRead(new byte[1], 0, 0, null);
            rwStream.EndRead(ar);
        }
#endif
    }
}
