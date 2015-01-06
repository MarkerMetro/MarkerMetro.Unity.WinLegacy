using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Storage.FileProperties;
using System.IO;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.io.file.aspx.
    /// </summary>
    public static class File
    {

        public static void Move(string source, string destination)
        {
#if NETFX_CORE
            source = source.FixPath();
            destination = destination.FixPath();

            var thread = MoveAsync(source, destination);
            thread.Wait();
#elif WINDOWS_PHONE
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().MoveFile(source, destination);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static string ReadAllText(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = PathIO.ReadTextAsync(path).AsTask();
            thread.Wait();
            return thread.Result;
#elif WINDOWS_PHONE
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Used only internally, use UnityEngine.Windows.File within Unity.
        /// </summary>
        internal static bool Exists(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = ExistsAsync(path);
            try
            {
                thread.Wait();
                return thread.Result;
            }
            catch
            {
                return false;
            }
#elif WINDOWS_PHONE
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().FileExists(path);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Used only internally, use UnityEngine.Windows.File within Unity.
        /// </summary>
        internal static void Delete(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = DeleteAsync(path);
            thread.Wait();
#elif WINDOWS_PHONE
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().DeleteFile(path);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Used only internally, use UnityEngine.Windows.File within Unity.
        /// </summary>
        internal static byte[] ReadAllBytes(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = ReadAllBytesAsync(path);
            thread.Wait();
            return thread.Result;
#elif WINDOWS_PHONE
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return data;
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        internal static void WriteAllBytes(string path, byte[] data)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = WriteAllBytesAsync(path, data);
            thread.Wait();
#elif WINDOWS_PHONE
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Create))
            {
                stream.Write(data, 0, (int)data.Length);
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static string[] ReadAllLines(string path)
        {     
#if NETFX_CORE
            path = path.FixPath();
            var thread = PathIO.ReadLinesAsync(path).AsTask();
            thread.Wait();
            return thread.Result.ToArray();
#elif WINDOWS_PHONE
            List<string> lines = new List<string>();
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                        lines.Add(line);
                }
            }
            return lines.ToArray();
#else
            throw new PlatformNotSupportedException();
#endif

        }

        public static void WriteAllText(string path, string data)
        {
#if NETFX_CORE
            path = FixPath(path);
            var thread = WriteAllTextAsync(path, data);
            thread.Wait();
#elif WINDOWS_PHONE
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Create))
            {
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(data);
                    sw.Flush();
                }
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }
        public static void Copy(string sourceFileName, string destFileName)
        {
#if NETFX_CORE
            sourceFileName = sourceFileName.FixPath();
            destFileName = destFileName.FixPath();
            CopyAsync(sourceFileName, destFileName, true).Wait();
#elif WINDOWS_PHONE
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CopyFile(sourceFileName, destFileName, true);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
#if NETFX_CORE
            sourceFileName = sourceFileName.FixPath();
            destFileName = destFileName.FixPath();
            CopyAsync(sourceFileName, destFileName, overwrite).Wait();
#elif WINDOWS_PHONE
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CopyFile(sourceFileName, destFileName, overwrite);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static DateTime GetLastWriteTime(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = GetLastWriteTimeAsync(path); 
            thread.Wait();
            return thread.Result;
#elif WINDOWS_PHONE
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().GetLastWriteTime(path).DateTime;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static System.IO.Stream Open(string path)
        {            
#if NETFX_CORE
            path = path.FixPath();
            var thread = OpenAsync(path);
            thread.Wait();
            return thread.Result;
#elif WINDOWS_PHONE
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static System.IO.Stream Open(string path, FileMode fileMode)
        {
            return new FileStream(path, fileMode);
        }

        public static System.IO.Stream Open(string path, FileMode fileMode, FileAccess fileAccess)
        {
            return new FileStream(path, fileMode);
        }

        public static FileStream Create(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = CreateFileStreamAsync(path);
            thread.Wait();
            return new FileStream(thread.Result);
#elif WINDOWS_PHONE
            return new FileStream(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CreateFile(path));
#else
            throw new PlatformNotSupportedException();
#endif
        }
        public static StreamWriter CreateText(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = CreateTextAsync(path);
            thread.Wait();
            return thread.Result;
#elif WINDOWS_PHONE
            return new StreamWriter(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CreateFile(path));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static StreamReader OpenText(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = OpenTextAsync(path);
            thread.Wait();
            return thread.Result;
#elif WINDOWS_PHONE
            return new StreamReader(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static FileStream OpenRead(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
#elif WINDOWS_PHONE
            return new FileStream(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static FileStream OpenWrite(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
#elif WINDOWS_PHONE
            return new FileStream(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite));
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Ensures we clean up unix paths with incorrect slash direction passed from Unity.
        /// </summary>
        internal static string FixPath(this string path)
        {
            return path.Replace('/', '\\');
        }

#if NETFX_CORE

        private static async Task WriteAllBytesAsync(string path, byte[] data)
        {
            bool fileExists = await ExistsAsync(path);
            if (!fileExists)
            {
                await CreateFileAsync(path);
            }
            await PathIO.WriteBytesAsync(path, data);
        }

        private static async Task WriteAllTextAsync(string path, string data)
        {
            bool fileExists = await ExistsAsync(path);
            if (!fileExists)
            {
                await CreateFileAsync(path);
            }
            await PathIO.WriteTextAsync(path, data);
        }

        private static async Task MoveAsync(string sourceFileName, string destFileName)
        {
            var sourceFile = await StorageFile.GetFileFromPathAsync(sourceFileName);
            var destDirName = Path.GetDirectoryName(destFileName);
            var destDir = await StorageFolder.GetFolderFromPathAsync(destDirName);
            await sourceFile.MoveAsync(destDir, Path.GetFileName(destFileName), NameCollisionOption.FailIfExists);
        }

        private static async Task<Stream> OpenAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            var stream = await file.OpenStreamForReadAsync();
            return stream;
        }

        private static async Task<StreamReader> OpenTextAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            var stream = await file.OpenStreamForReadAsync();
            return new StreamReader(stream);
        }

        private static async Task CopyAsync(string sourceFileName, string destFileName, bool overwrite)
        {
            var exists = await ExistsAsync(destFileName);
            if (!overwrite && exists)
                return;

            var sourceDirName = Path.GetDirectoryName(sourceFileName);
            var destDirName = Path.GetDirectoryName(destFileName);

            var sourceDir = await StorageFolder.GetFolderFromPathAsync(sourceDirName);
            var destDir = await StorageFolder.GetFolderFromPathAsync(destDirName);

            var sourceFile = await sourceDir.GetFileAsync(Path.GetFileName(sourceFileName));
            var destFile = await destDir.CreateFileAsync(Path.GetFileName(destFileName), CreationCollisionOption.ReplaceExisting);

            await sourceFile.CopyAndReplaceAsync(destFile);

        }

        private static async Task<StreamWriter> CreateTextAsync(string path)
        {
            var str = await CreateFileStreamAsync(path);
            return new StreamWriter(str);
        }

        private static async Task<byte[]> ReadAllBytesAsync(string path)
        {
            var buffer = await PathIO.ReadBufferAsync(path);
            using (var dr = DataReader.FromBuffer(buffer))
            {
                // await dr.LoadAsync(buffer.Length); <- exception happening here "The operation identifier is not valid".
                byte[] data = new byte[buffer.Length];
                dr.ReadBytes(data);
                return data;
            }
        }

        private static async Task<bool> ExistsAsync(string path)
        {
            bool exists = false;
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(path);
                exists = true;
            }
            catch { }
            return exists;
        }

        private static async Task DeleteAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            if (file != null)
                await file.DeleteAsync();
        }

        private static async Task<Stream> CreateFileStreamAsync(string path)
        {
            var file = await CreateFileAsync(path);
            return await file.OpenStreamForWriteAsync();
        }

        private static async Task<StorageFile> CreateFileAsync(string path)
        {
            var localFolder = ApplicationData.Current.LocalFolder;

            // strip containing local folder
            var localFolderPath = localFolder.Path.ToLower();
            if (path.ToLower().StartsWith(localFolderPath))
            {
                path = path.Substring(localFolderPath.Length + 1);
            }
            var filename = Path.GetFileName(path);
            var parentFolder = ApplicationData.Current.LocalFolder;

            // check sub folders if required
            if (path != filename)
            {
                // strip file name from end
                if (path.EndsWith(filename))
                {
                    path = path.Substring(0, path.Length - filename.Length - 1);
                }

                // get a list of all the sub folders 
                var folderNames = path.Split('\\');

                // loop through and ensure each folder exists
                foreach (var folderName in folderNames)
                {
                    var folderPath = Path.Combine(parentFolder.Path, folderName);
                    StorageFolder newParentFolder = null;
                    bool folderExists = false;
                    try
                    {
                        newParentFolder = await StorageFolder.GetFolderFromPathAsync(folderPath);
                        folderExists = true;
                    }
                    catch { }
                    if (!folderExists)
                    {
                        newParentFolder = await parentFolder.CreateFolderAsync(folderName);
                    }
                    parentFolder = newParentFolder;
                }
            }

            // create file
            var file = await parentFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            return file;
        }

        private static async Task<DateTime> GetLastWriteTimeAsync(string path)
        {
            path = path.FixPath();
            StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
            BasicProperties fileProperties = await file.GetBasicPropertiesAsync();
            return fileProperties.DateModified.DateTime;
        }
#endif

    }
}
