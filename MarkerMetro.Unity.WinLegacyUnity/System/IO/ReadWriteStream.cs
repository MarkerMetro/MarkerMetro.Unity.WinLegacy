#if NETFX_CORE
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    /// <summary>
    /// Single stream supporting reading and writing (used as part of TcpClient implementation).
    /// </summary>
    public class ReadWriteStream : Stream
    {
        private readonly Stream readStream;
        private readonly Stream writeStream;

        public ReadWriteStream(Stream readStream, Stream writeStream)
        {
            this.readStream = readStream;
            this.writeStream = writeStream;
        }

        public override bool CanRead { get { return readStream.CanRead; } }
        public override bool CanSeek { get { return readStream.CanSeek; } }
        public override bool CanTimeout { get { return readStream.CanTimeout && writeStream.CanTimeout; } }
        public override bool CanWrite { get { return writeStream.CanWrite; } }

        /// <summary>
        /// Length (uses internal read stream).
        /// </summary>
        public override long Length { get { return readStream.Length; } }

        /// <summary>
        /// Position (uses internal read stream).
        /// </summary>
        public override long Position
        {
            get { return readStream.Position; }
            set { readStream.Position = value; }
        }

        public override int ReadTimeout
        {
            get { return readStream.ReadTimeout; }
            set { readStream.ReadTimeout = value; }
        }
        public override int WriteTimeout
        {
            get { return writeStream.WriteTimeout; }
            set { writeStream.WriteTimeout = value; }
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
        {
            return readStream.CopyToAsync(destination, bufferSize, cancellationToken);
        }

        public override void Flush()
        {
            readStream.Flush();
            writeStream.Flush();
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            await readStream.FlushAsync(cancellationToken);
            await writeStream.FlushAsync(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return readStream.Read(buffer, offset, count);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return readStream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public override int ReadByte()
        {
            return readStream.ReadByte();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return readStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            writeStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            writeStream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return writeStream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            writeStream.WriteByte(value);
        }
    }

}
#endif