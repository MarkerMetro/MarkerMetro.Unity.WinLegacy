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
    public static class File
    {
        public static void Move(string source, string destination)
        {
#if NETFX_CORE
            source = source.FixPath();
            destination = destination.FixPath();

            var thread = MoveAsync(source, destination);
            thread.Wait();
#elif SILVERLIGHT
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().MoveFile(source, destination);
#else
            throw new NotImplementedException();
#endif
        }

        public static bool Exists(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = ExistsAsync(path);
            try
            {
                thread.Wait();

                if (thread.IsCompleted)
                    return thread.Result;
                else
                    return false;
            }
            catch
            {
                return false;
            }
#elif SILVERLIGHT
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().FileExists(path);
#else
            throw new NotImplementedException();
#endif
        }

        public static string ReadAllText(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = _ReadAllText(path);

            if (thread != null)
            {
                thread.Wait();
                return thread.Result;
            }

            return null;
#elif SILVERLIGHT
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
#else
            throw new NotImplementedException();
#endif
        }

#if NETFX_CORE
        public static async Task<string> _ReadAllText(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(fileName);

            if (file != null)
            {
                return await FileIO.ReadTextAsync(file);
            }

            return null;
        }
#endif

        public static byte[] ReadAllBytes(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = ReadAllBytesAsync(path);
            thread.Wait();

            if (thread.IsCompleted)
                return thread.Result;

            throw thread.Exception;
#elif SILVERLIGHT
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open))
            {
                var data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                return data;
            }
#else
            throw new NotImplementedException();
#endif
        }

        public static void WriteAllBytes(string path, byte[] data)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = PathIO.WriteBytesAsync(path, data).AsTask();
            thread.Wait();
#elif SILVERLIGHT
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Create))
            {
                stream.Write(data, 0, (int)data.Length);
            }
#else
            throw new NotImplementedException();
#endif
        }

        public static void WriteAllText(string path, string data)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = _WriteAllText(path, data);
            thread.Wait();
#elif SILVERLIGHT
            using (var stream = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Create))
            {
                using (var sw = new StreamWriter(stream))
                {
                    sw.Write(data);
                    sw.Flush();
                }
            }
#else
            throw new NotImplementedException();
#endif
        }

#if NETFX_CORE
        private static async Task _WriteAllText(string fileName, string data)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var fileOption = CreationCollisionOption.ReplaceExisting;
            var file = await folder.CreateFileAsync(fileName, fileOption);
            await FileIO.WriteTextAsync(file, data);
        }
#endif

        public static void Copy(string sourceFileName, string destFileName)
        {
#if NETFX_CORE
           sourceFileName = sourceFileName.FixPath();
            destFileName = destFileName.FixPath();
            CopyAsync(sourceFileName, destFileName, true).Wait();
#elif SILVERLIGHT
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CopyFile(sourceFileName, destFileName, true);
#else
            throw new NotImplementedException();
#endif
        }

        public static void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
#if NETFX_CORE
            sourceFileName = sourceFileName.FixPath();
            destFileName = destFileName.FixPath();
            CopyAsync(sourceFileName, destFileName, overwrite).Wait();
#elif SILVERLIGHT
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CopyFile(sourceFileName, destFileName, overwrite);
#else
            throw new NotImplementedException();
#endif
        }

        public static void Delete(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = DeleteAsync(path);
            thread.Wait();
#elif SILVERLIGHT
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().DeleteFile(path);
#else
            throw new NotImplementedException();
#endif
        }


        public static DateTime GetLastWriteTime(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = GetLastWriteTimeAsync(path); 
            thread.Wait();

            if (thread.IsCompleted)
                return thread.Result;

            throw thread.Exception;
#elif SILVERLIGHT
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().GetLastWriteTime(path).DateTime;
#else
            throw new NotImplementedException();
#endif
        }


        public static System.IO.Stream Open(string path)
        {            
#if NETFX_CORE
            path = path.FixPath();
            var thread = OpenAsync(path);
            thread.Wait();

            if (thread.IsCompleted)
                return thread.Result;

            throw thread.Exception;
#elif SILVERLIGHT
            return System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open);
#else
            throw new NotImplementedException();
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
            var thread = CreateAsync(path);
            thread.Wait();

            if (thread.IsCompleted)
                return new FileStream(thread.Result);

            throw thread.Exception;
#elif SILVERLIGHT
            return new FileStream(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CreateFile(path));
#else
            throw new NotImplementedException();
#endif
        }

        public static StreamWriter CreateText(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = CreateTextAsync(path);
            thread.Wait();

            if (thread.IsCompleted)
                return thread.Result;

            throw thread.Exception;
#elif SILVERLIGHT
            return new StreamWriter(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().CreateFile(path));
#else
            throw new NotImplementedException();
#endif
        }

        public static StreamReader OpenText(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var thread = OpenTextAsync(path);
            thread.Wait();

            if (thread.IsCompleted)
                return thread.Result;

            throw thread.Exception;
#elif SILVERLIGHT
            return new StreamReader(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open));
#else
            throw new NotImplementedException();
#endif
        }


        public static string StripLocalFolder(string path)
        {
#if NETFX_CORE
            path = path.FixPath();
            var localPath = ApplicationData.Current.LocalFolder.Path.ToLower();

            if (path.ToLower().StartsWith(localPath))
                path = path.Substring(localPath.Length + 1);
            return path;
#else
            throw new NotImplementedException();
#endif
        }

        public static FileStream OpenRead(string path)
        {
#if NETFX_CORE
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
#elif SILVERLIGHT
            return new FileStream(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open));
#else
            throw new NotImplementedException();
#endif
        }

        public static FileStream OpenWrite(string path)
        {
#if NETFX_CORE
            return new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
#elif SILVERLIGHT
            return new FileStream(System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication().OpenFile(path, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite));
#else
            throw new NotImplementedException();
#endif
        }

#if NETFX_CORE


        private static EncryptedStreamReader OpenEncryptedText(string path)
        {
            path = path.FixPath();
            if (Exists(path))
            {
                var thread = OpenEncryptedTextAsync(path);
                thread.Wait();

                if (thread.IsCompleted)
                    return thread.Result;

                throw thread.Exception;
            }
            else
            {
                return null;
            }
        }

        private static EncryptedStreamWriter CreateEncryptedText(string path)
        {

            path = path.FixPath();
            var thread = CreateEncryptedTextAsync(path);
            thread.Wait();

            if (thread.IsCompleted)
                return thread.Result;

            throw thread.Exception;
        }

        private static async Task MoveAsync(string source, string destination)
        {
            var file = await StorageFile.GetFileFromPathAsync(source);
            var destinatinoFolder = await StorageFolder.GetFolderFromPathAsync(destination);
            if (file != null && destinatinoFolder != null)
            {
                await file.MoveAsync(destinatinoFolder);
            }
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

        private static async Task<EncryptedStreamReader> OpenEncryptedTextAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);

            var stream = await file.OpenStreamForReadAsync();
            return new EncryptedStreamReader(stream);
        }

        /* Copy ********************************************************************/

        private static async Task CopyAsync(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!overwrite && Exists(destFileName))
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
            var str = await CreateAsync(path);
            return new StreamWriter(str);
        }


        private async static void CopyInitialisationFile(string fileName)
        {
            var db = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Integration/Initialization/" + fileName, UriKind.RelativeOrAbsolute));

            string localFolder = ApplicationData.Current.LocalFolder.Path;
            await db.CopyAsync(ApplicationData.Current.LocalFolder, fileName, NameCollisionOption.ReplaceExisting);
        }

        private static async Task<bool> ExistsAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            return file != null;
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
        private static async Task DeleteAsync(string path)
        {
            var file = await StorageFile.GetFileFromPathAsync(path);
            if (file != null)
                await file.DeleteAsync();
        }

        private static async Task<Stream> CreateAsync(string path)
        {
            var dirName = Path.GetDirectoryName(path);
            var filename = Path.GetFileName(path);

            var dir = await StorageFolder.GetFolderFromPathAsync(dirName);
            var file = await dir.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            return await file.OpenStreamForWriteAsync();
        }

        private static async Task<EncryptedStreamWriter> CreateEncryptedTextAsync(string path)
        {
            var str = await CreateAsync(path);
            return new EncryptedStreamWriter(str);
        }

        public static async Task<DateTime> GetLastWriteTimeAsync(string path)
        {
            path = path.FixPath();
            StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
            BasicProperties fileProperties = await file.GetBasicPropertiesAsync();
            return fileProperties.DateModified.DateTime;
        }
#endif
        internal static string FixPath(this string path)
        {
            path = path.Replace('/', '\\');
#if NETFX_CORE
            path = path.Replace(ApplicationData.Current.LocalFolder.Path, "");
            while (path.IndexOf('\\') == 0) {
                path = path.Substring(1);
            }
#endif
            return path;        
        }
    }
}