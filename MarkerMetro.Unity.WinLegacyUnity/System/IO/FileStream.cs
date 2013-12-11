using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarkerMetro.Unity.WinLegacy.IO;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class FileStream : global::System.IO.Stream
    {
        public FileStream(string filePath, FileMode mode)
            : this(filePath, mode, FileAccess.ReadWrite)
        {
            throw new NotImplementedException();
        }

        public FileStream(string filePath, FileMode mode, FileAccess access)
            : this(filePath, mode, access, FileShare.None)
        {
            throw new NotImplementedException();
        }

        public FileStream(string filePath, FileMode mode, FileAccess access, FileShare share)
            : this(filePath, mode, access, share, 4096)
        {
            throw new NotImplementedException();
        }
        public FileStream(string filePath, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite
        {
            get { throw new NotImplementedException(); }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
