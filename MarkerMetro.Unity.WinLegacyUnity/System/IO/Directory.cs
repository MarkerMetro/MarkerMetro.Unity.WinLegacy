using System;
using System.Collections.Generic;
using System.Linq;
#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel;
using System.IO;
using Windows.Storage.Search;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.io.directory.aspx.
    /// </summary>
    public static class Directory
    {
        public static string[] GetFiles(string path)
        {
#if NETFX_CORE
            var t = GetFilesAsync(path.FixPath());
            t.Wait();
            return t.Result == null ? new string[0] : t.Result;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static string[] GetFiles(string path, string searchPattern)
        {
            return GetFiles(path, searchPattern, SearchOption.AllDirectories);
        }

        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
#if NETFX_CORE
            var t = GetFilesAsync(path.FixPath());
            t.Wait();
            return t.Result == null ? new string[0] : t.Result;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static bool Exists(string path)
        {
#if NETFX_CORE
            var t = ExistsAsync(path.FixPath());
            try
            {
                t.Wait();
                return t.Result;
            }
            catch
            {
                return false;
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        // Any and all directories specified in path are created, unless they already exist or unless 
        // some part of path is invalid. If the directory already exists, this method does not create a 
        // new directory.
        // The path parameter specifies a directory path, not a file path, and it must in 
        // the ApplicationData domain.
        // Trailing spaces are removed from the end of the path parameter before creating the directory.
        public static void CreateDirectory(string path)
        {
#if NETFX_CORE
             path = path.FixPath().TrimEnd('\\');
            StorageFolder folder = null;

            foreach(var f in new StorageFolder[] {
                ApplicationData.Current.LocalFolder, 
                ApplicationData.Current.RoamingFolder, 
                ApplicationData.Current.TemporaryFolder } )
            {
                string p = ParsePath(path, f);
                if (f != null)
                {
                    path = p;
                    folder = f;
                    break;
                }
            }

            if(path == null)
                throw new NotSupportedException("This method implementation doesn't support " +
                "parameters outside paths accessible by ApplicationData.");

            string[] folderNames = path.Split('\\');
            for (int i = 0; i < folderNames.Length; i++)
            {
                var task = folder.CreateFolderAsync(folderNames[i], CreationCollisionOption.OpenIfExists).AsTask();
                task.Wait();
                folder = task.Result;
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static void Delete(string path, bool recursive = false)
        {
#if NETFX_CORE
            path = path.FixPath();
            var folderTask = StorageFolder.GetFolderFromPathAsync(path).AsTask<StorageFolder>();
            folderTask.Wait();
            var folder = folderTask.Result;

            if (!recursive)
            {
                var filesTask = folder.GetFilesAsync().AsTask<IReadOnlyList<StorageFile>>();
                if (filesTask.Result.Count > 0)
                    throw new IOException("The directory specified by path is not empty.");
            }

            folder.DeleteAsync().AsTask().Wait();  
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static string GetCurrentDirectory()
        {
#if NETFX_CORE
            return Package.Current.InstalledLocation.Path;
#else
            throw new PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE
        private static string ParsePath(string path, StorageFolder folder)
        {
            if (path.Contains(folder.Path))
                {
                    path = path.Substring(path.LastIndexOf(folder.Path) + folder.Path.Length + 1);
                    return path;
                }
            return null;
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

#if NETFX_CORE
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
