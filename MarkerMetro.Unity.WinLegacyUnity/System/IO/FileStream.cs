using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarkerMetro.Unity.WinLegacy.IO;
#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Storage;
#endif
using System.Diagnostics;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class FileStream : global::System.IO.Stream
    {
        readonly System.IO.Stream stream;
        bool closed = false;

        // @todo Support usage of this stream.
        internal FileStream(System.IO.Stream stream)
        {
            this.stream = stream;
        }

        public FileStream(string filePath, FileMode mode)
            : this(filePath, mode, FileAccess.ReadWrite)
        {
        }

        public FileStream(string filePath, FileMode mode, FileAccess access)
            : this(filePath, mode, access, FileShare.None)
        {
        }

        public FileStream(string filePath, FileMode mode, FileAccess access, FileShare share)
            : this(filePath, mode, access, share, 4096)
        {
        }

        public FileStream(string filePath, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
        {
#if NETFX_CORE
            filePath = filePath.FixPath();
            var task = OpenFileStreamAsync(filePath, mode, access, share, bufferSize);

            task.Wait();

            if (task.IsFaulted)
                throw task.Exception;

            this.stream = task.Result;
#endif
        }

#if NETFX_CORE
        async Task<System.IO.Stream> OpenFileStreamAsync(string filePath, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath is null or empty.", "filePath");

            if(mode!=FileMode.Open && access==FileAccess.Read)
                throw new ArgumentOutOfRangeException("You can use FileAccess.Read only with FileMode.Open");

            var dirName = Path.GetDirectoryName(filePath);
            var filename = Path.GetFileName(filePath);

            var dir = await StorageFolder.GetFolderFromPathAsync(dirName);

            StorageFile file = null;

            switch (mode)
            {
                case FileMode.Append:
                    file = await dir.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                    break;
                case FileMode.Create:
                    file = await dir.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                    break;
                case FileMode.CreateNew:
                    file = await dir.CreateFileAsync(filename, CreationCollisionOption.FailIfExists);
                    break;
                case FileMode.Open:
                    file = await dir.GetFileAsync(filename);
                    break;
                case FileMode.OpenOrCreate:
                    file = await dir.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                    break;
                case FileMode.Truncate:
                    file = await dir.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                    break;
                default:
                    throw new ArgumentException("Invalid mode provieded: " + mode);
            }

            Debug.Assert(file != null, "File must be not null");

            var stream = await (access==FileAccess.Read ? file.OpenStreamForReadAsync() : file.OpenStreamForWriteAsync());

            if (mode == FileMode.Append)
                stream.Position = stream.Length;

            return stream;
        }
#endif

        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }

        public override bool CanRead
        {
            get { return stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return stream.CanWrite; }
        }

        public override void Flush()
        {
#if NETFX_CORE
            var task = stream.FlushAsync();
            task.Wait();
            if (task.IsFaulted)
                throw task.Exception;
#else
            stream.Flush();
#endif
        }

        public override long Position
        {
            get { return stream.Position; }
            set { stream.Position = value; }
        }

        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
#if NETFX_CORE
            var task = stream.ReadAsync(buffer, offset, count);
            task.Wait();
            if (task.IsFaulted)
                throw task.Exception;
            return task.Result;
#else
            return stream.Read(buffer, offset, count);
#endif
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
#if NETFX_CORE
            var task = stream.WriteAsync(buffer, offset, count);
            task.Wait();
            if (task.IsFaulted)
                throw task.Exception;
#else
            stream.Write(buffer, offset, count);
#endif
        }

        public override long Length
        {
            get { return stream.Length; }
        }

        #if NETFX_CORE
        public void Close()
        {
            if (!closed)
            {
                stream.Dispose();
                closed = true;
            }
        }
        #else
        public override void Close()
        {
            stream.Close();
        }
        #endif

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!closed)
                {
                    stream.Dispose();
                    closed = true;
                }
            }

            base.Dispose(disposing);
        }
    }
}
