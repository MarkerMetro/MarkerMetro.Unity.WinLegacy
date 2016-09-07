using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkerMetro.Unity.WinLegacy.Plugin.IO;

#if NETFX_CORE
using Windows.Storage;
using System.Threading.Tasks;
using System.Collections;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.io.fileinfo.aspx.
    /// </summary>
    public class FileInfo
    {
        

#if NETFX_CORE
        string _path;
        StorageFile _file;
#endif

        public FileInfo(string path)
        {
#if NETFX_CORE
            _path = path.FixPath();
            var thread = GetFileAsync(_path);
            thread.Wait();
            _file = thread.Result;
#else
            throw new PlatformNotSupportedException("new FileInfo");
#endif
        }

        public long Length
        {
            get
            {
#if NETFX_CORE
                var thread = GetSizeAsync(_file);
                thread.Wait();
                return Convert.ToInt64(thread.Result);
#else
                throw new PlatformNotSupportedException("FileInfo.Length");
#endif
            }
        }

        public string Name
        {
            get
            {
#if NETFX_CORE
                return _file.Name;
#else
                throw new PlatformNotSupportedException("FileInfo.Name");
#endif
            }
        }

        public string FullName
        {
            get
            {
#if NETFX_CORE
                return _file.Path;
#else
                throw new PlatformNotSupportedException("FileInfo.FullName");
#endif
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
#if NETFX_CORE
                var thread = GetDateModifiedAsync(_file);
                thread.Wait();
                return thread.Result.DateTime;
#else
                throw new PlatformNotSupportedException("FileInfo.LastWriteTime");
#endif
            }
        }
        public DirectoryInfo Directory
        {
            get
            {
#if NETFX_CORE
                return new DirectoryInfo(System.IO.Path.GetDirectoryName(_path));
#else
                throw new PlatformNotSupportedException("FileInfo.Directory");
#endif
            }
        }

        public int Attributes
        {
            get
            {
#if NETFX_CORE
                return (int)_file.Attributes;
#else
                throw new PlatformNotSupportedException("FileInfo.Attributes");
#endif
            }
        }

        public MarkerMetro.Unity.WinLegacy.Plugin.IO.FileStream OpenRead()
        {
#if NETFX_CORE
            return File.OpenRead(_path);
#else
            throw new PlatformNotSupportedException("FileInfo.OpenRead");
#endif
        }

        public bool Exists
        {
            get
            {
#if NETFX_CORE
            return File.Exists(_path);
#else
            throw new PlatformNotSupportedException("FileInfo.Exists");
#endif
            }
        }

        public void Delete()
        {
#if NETFX_CORE
            File.Delete(_path);
#else
            throw new PlatformNotSupportedException("FileInfo.Delete");
#endif
        }

#if NETFX_CORE

        private async Task<StorageFile> GetFileAsync(string path)
        {
            bool fileExists = File.Exists(path);
            if (fileExists)
            {
                return await StorageFile.GetFileFromPathAsync(path);
            }
            return null;
        }

        private async Task<ulong> GetSizeAsync(IStorageFile file)
        {
            var props = await file.GetBasicPropertiesAsync();
            return props.Size;
        }

        private async Task<DateTimeOffset> GetDateModifiedAsync(IStorageFile file)
        {
            var props = await file.GetBasicPropertiesAsync();
            return props.DateModified;
        }
#endif

    }
}
