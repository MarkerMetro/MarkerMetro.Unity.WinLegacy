using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    /// <summary>
    /// We need this to prevent WACK failures due to Unity 3D constructor re-writting.
    /// This will add new .ctor with UIntPtr dummy parameter in every StreamWriter descendant
    /// that will be calling default .ctr in StreamWriter (not supported in WinRT/NetFxCore)
    /// </summary>
    public class StreamWriter : System.IO.TextWriter
    {
        readonly System.IO.StreamWriter _actual;

        public StreamWriter()
        {
            throw new NotSupportedException();
        }

        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using UTF-8 encoding and the default buffer size.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        // Exceptions:
        //   System.ArgumentException:
        //     stream is not writable.
        //
        //   System.ArgumentNullException:
        //     stream is null.
        public StreamWriter(System.IO.Stream stream)
            : this(stream, Encoding.UTF8)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using the specified encoding and the default buffer size.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        //   encoding:
        //     The character encoding to use.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     stream or encoding is null.
        //
        //   System.ArgumentException:
        //     stream is not writable.
        public StreamWriter(System.IO.Stream stream, Encoding encoding)
            : this(stream, encoding, DefaultBufferSize)
        {
        }

        //
        // Summary:
        //     Initializes a new instance of the StreamWriter class for the specified file by using the default
        //     encoding and buffer size. If the file exists, it can be either overwritten or appended to.
        //     If the file does not exist, this constructor creates a new file.
        //
        // Parameters:
        //   path:
        //     The complete file path to write to. 
        //
        //   append:
        //     true to append data to the file; false to overwrite the file. If the specified file does not
        //     exist, this parameter has no effect, and the constructor creates a new file. 
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     path is null.
        //
        //   System.UnauthorizedAccessException:
        //     Access is denied.
        //
        //   System.ArgumentException:
        //     path is empty.
        //     -or-
        //     path contains the name of a system device (com1, com2, and so on).
        //
        //   System.IO.DirectoryNotFoundException:
        //     The specified path is invalid (for example, it is on an unmapped drive).
        //
        //   System.IO.IOException:
        //     path includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. 
        //
        //   System.IO.PathTooLongException:
        //     The specified path, file name, or both exceed the system-defined maximum length. For example, on
        //     Windows-based platforms, paths must not exceed 248 characters, and file names must not
        //     exceed 260 characters. 
        //
        //   System.Security.SecurityException:
        //     The caller does not have the required permission. 
        public StreamWriter(string path, bool append) 
        {
            var fileWriter = new FileStream(path, append? FileMode.Append : FileMode.OpenOrCreate);
            _actual = new System.IO.StreamWriter(fileWriter);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.IO.StreamWriter class for the specified
        //     stream by using the specified encoding and buffer size.
        //
        // Parameters:
        //   stream:
        //     The stream to write to.
        //
        //   encoding:
        //     The character encoding to use.
        //
        //   bufferSize:
        //     The buffer size, in bytes.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     stream or encoding is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     bufferSize is negative.
        //
        //   System.ArgumentException:
        //     stream is not writable.
        public StreamWriter(System.IO.Stream stream, Encoding encoding, int bufferSize)
        {
            _actual = new System.IO.StreamWriter(stream, encoding, bufferSize);
        }

        // Summary:
        //     Gets or sets a value indicating whether the System.IO.StreamWriter will flush
        //     its buffer to the underlying stream after every call to System.IO.StreamWriter.Write(System.Char).
        //
        // Returns:
        //     true to force System.IO.StreamWriter to flush its buffer; otherwise, false.
        public virtual bool AutoFlush
        {
            get { return _actual.AutoFlush; }
            set { _actual.AutoFlush = value; }
        }
        //
        // Summary:
        //     Gets the underlying stream that interfaces with a backing store.
        //
        // Returns:
        //     The stream this StreamWriter is writing to.
        public virtual System.IO.Stream BaseStream { get { return _actual.BaseStream; } }
        //
        // Summary:
        //     Gets the System.Text.Encoding in which the output is written.
        //
        // Returns:
        //     The System.Text.Encoding specified in the constructor for the current instance,
        //     or System.Text.UTF8Encoding if an encoding was not specified.
        public override Encoding Encoding { get { return _actual.Encoding; } }

        // Summary:
        //     Releases the unmanaged resources used by the System.IO.StreamWriter and optionally
        //     releases the managed resources.
        //
        // Parameters:
        //   disposing:
        //     true to release both managed and unmanaged resources; false to release only
        //     unmanaged resources.
        //
        // Exceptions:
        //   System.Text.EncoderFallbackException:
        //     The current encoding does not support displaying half of a Unicode surrogate
        //     pair.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _actual.Dispose();
        }
        //
        // Summary:
        //     Clears all buffers for the current writer and causes any buffered data to
        //     be written to the underlying stream.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     The current writer is closed.
        //
        //   System.IO.IOException:
        //     An I/O error has occurred.
        //
        //   System.Text.EncoderFallbackException:
        //     The current encoding does not support displaying half of a Unicode surrogate
        //     pair.
        public override void Flush()
        {
            base.Flush();

            _actual.Flush();
        }
        //
        // Summary:
        //     Writes a character to the stream.
        //
        // Parameters:
        //   value:
        //     The character to write to the stream.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     An I/O error occurs.
        //
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        public override void Write(char value)
        {
            _actual.Write(value);
        }
        //
        // Summary:
        //     Writes a character array to the stream.
        //
        // Parameters:
        //   buffer:
        //     A character array containing the data to write. If buffer is null, nothing
        //     is written.
        //
        // Exceptions:
        //   System.IO.IOException:
        //     An I/O error occurs.
        //
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        public override void Write(char[] buffer)
        {
            _actual.Write(buffer);
        }
        //
        // Summary:
        //     Writes a string to the stream.
        //
        // Parameters:
        //   value:
        //     The string to write to the stream. If value is null, nothing is written.
        //
        // Exceptions:
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        //
        //   System.IO.IOException:
        //     An I/O error occurs.
        public override void Write(string value)
        {
            _actual.Write(value);
        }
        //
        // Summary:
        //     Writes a subarray of characters to the stream.
        //
        // Parameters:
        //   buffer:
        //     A character array that contains the data to write.
        //
        //   index:
        //     The character position in the buffer at which to start reading data.
        //
        //   count:
        //     The maximum number of characters to write.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     buffer is null.
        //
        //   System.ArgumentException:
        //     The buffer length minus index is less than count.
        //
        //   System.ArgumentOutOfRangeException:
        //     index or count is negative.
        //
        //   System.IO.IOException:
        //     An I/O error occurs.
        //
        //   System.ObjectDisposedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and current writer is closed.
        //
        //   System.NotSupportedException:
        //     System.IO.StreamWriter.AutoFlush is true or the System.IO.StreamWriter buffer
        //     is full, and the contents of the buffer cannot be written to the underlying
        //     fixed size stream because the System.IO.StreamWriter is at the end the stream.
        public override void Write(char[] buffer, int index, int count)
        {
            _actual.Write(buffer, index, count);
        }

#pragma warning disable
        public void Close()
        {
            _actual.Dispose();
        }
#pragma warning restore

        internal static int DefaultBufferSize { get { return 0x400; } }
    }
}
