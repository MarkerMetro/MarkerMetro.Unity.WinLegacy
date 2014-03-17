using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE || SILVERLIGHT
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel;
using System.IO;
using Windows.Storage.Search;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public static class Directory
    {
        public static string[] GetFiles(string path)
        {
#if NETFX_CORE
            var t = GetFilesAsync(path.Replace('/', '\\'));
            t.Wait();
            return t.Result;
#else
            throw new NotImplementedException();
#endif
        }

        public static string[] GetFiles(string path, string searchPattern)
        {
            return GetFiles(path, searchPattern, SearchOption.AllDirectories);
        }

        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
#if NETFX_CORE || SILVERLIGHT
            var t = GetFilesAsync(path.Replace('/', '\\'));
            t.Wait();
            return t.Result;
#else
            throw new NotImplementedException();
#endif
        }

        public static bool Exists(string path)
        {
#if NETFX_CORE
            var t = ExistsAsync(path.Replace('/', '\\'));
            t.Wait();
            if (t.IsCompleted)
            {
                return t.Result;
            }
            else
            {
                return false;
            }
#else
            throw new NotImplementedException();
#endif
        }

        public static bool CreateDirectory(string path)
        {
#if NETFX_CORE
            var t = CreateDirectoryAsync(path);
            t.Wait();
            if (t.IsCompleted)
            {
                return t.Result;
            }
            else
            {
                return false;
            }
#else
            throw new NotImplementedException();
#endif
        }

        public static void Delete(string path)
        {
#if NETFX_CORE
            path = path.Replace('/', '\\');
            var folderTask = StorageFolder.GetFolderFromPathAsync(path).AsTask<StorageFolder>();
            folderTask.Wait();
            var folder = folderTask.Result;

            var filesTask = folder.GetFilesAsync().AsTask<IReadOnlyList<StorageFile>>();
            if (filesTask.Result.Count > 0)
                throw new IOException("The directory specified by path is not empty.");

            folder.DeleteAsync().AsTask().Wait();            
#else
            throw new NotImplementedException();
#endif
        }

        public static string GetCurrentDirectory()
        {
#if NETFX_CORE
            return Package.Current.InstalledLocation.Path;
#else
            throw new NotImplementedException();
#endif
        }

#if NETFX_CORE

        /// <summary>
        /// Creates a folder in local iso storage
        /// </summary>
        private static async Task<bool> CreateDirectoryAsync(string folderName)
        {
            try
            {
                await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private static async Task<bool> ExistsAsync(string path)
        {
            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

#endif

#if NETFX_CORE || SILVERLIGHT
        private static async Task<string[]> GetFilesAsync(string path)
        {
            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                var files = await folder.GetFilesAsync();
                var result = new string[files.Count];

                for (var i = 0; i < files.Count; i++)
                {
                    result[i] = Path.Combine(path, files[i].Name);
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        private static async Task<string[]> GetFilesAsync(string path, string filter)
        {
            try
            {
                var folder = await StorageFolder.GetFolderFromPathAsync(path);
                var fileQuery = folder.CreateFileQueryWithOptions(new QueryOptions(CommonFileQuery.DefaultQuery, new [] { filter }));
                var files = await fileQuery.GetFilesAsync();

                return files.Select(f => f.Path).ToArray();
            }
            catch
            {
                return null;
            }
        }
#endif

    }
}
