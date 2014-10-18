using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

#if NETFX_CORE || WINDOWS_PHONE
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using MarkerMetro.Unity.WinLegacy.IO;
#else
using System.Net.Sockets;
using System.Threading;
#endif

namespace MarkerMetro.Unity.WinLegacy.Net.Sockets
{

    public class TcpClient
    {
        public bool UsePlainSocket { get; set; }
   
#if NETFX_CORE || WINDOWS_PHONE
        private StreamSocket _socket = null;
        DataWriter _writer;
        bool _isConnected = false;
        ReadWriteStream _readWriteStream = null;


        private async Task EnsureSocket(string hostName, int port)
        {
            try
            {
                var host = new HostName(hostName);
                _socket = new StreamSocket();
                await _socket.ConnectAsync(host, port.ToString(), UsePlainSocket ? SocketProtectionLevel.PlainSocket : SocketProtectionLevel.SslAllowNullEncryption);
                _readWriteStream = new ReadWriteStream(_socket.InputStream.AsStreamForRead(), _socket.OutputStream.AsStreamForWrite());
                _isConnected = true;
            }
            catch (Exception ex)
            {
                Close();
                // If this is an unknown status it means that the error is fatal and retry will likely fail.
                if (SocketError.GetStatus(ex.HResult) == SocketErrorStatus.Unknown)
                {
                    // TODO abort any retry attempts on Unity side
                    throw;
                }
            }
        }
#endif
        public bool Connected
        {
            get
            {
#if NETFX_CORE || WINDOWS_PHONE
                return _socket != null && _isConnected;
#else
                throw new NotImplementedException();
#endif
            }
        }

        public int SendTimeout { get; set; }
        public int ReceiveTimeout { get; set; }

        public void Connect(string hostName, int port)
        {
#if NETFX_CORE || WINDOWS_PHONE
            var thread = EnsureSocket(hostName, port);
            thread.Wait();
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
        {
#if NETFX_CORE || WINDOWS_PHONE

            AsyncResult res = new AsyncResult();
            res.AsyncState = state;
            res.IsCompleted = false;

            var task = EnsureSocket(host, port);

            task.ContinueWith((t) =>
            {
                res.IsCompleted = true;
                requestCallback(res);
            });

            return res;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void EndConnect(IAsyncResult result)
        {
#if NETFX_CORE || WINDOWS_PHONE
            // Nothing needed
            return;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public Stream GetStream()
        {
#if NETFX_CORE || WINDOWS_PHONE
            if (_socket == null || !_isConnected) return null;
            return _readWriteStream;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void Close()
        {
#if NETFX_CORE || WINDOWS_PHONE  
            _isConnected = false;
            if (_socket != null)
            {
                _socket.Dispose();
                _socket = null;
            }
            if (_readWriteStream != null)
            {
                _readWriteStream.Dispose();
                _readWriteStream = null;
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Helper method to allow easy writing of bytes to the socket stream
        /// </summary>
        /// <param name="bytes"></param>
        public void WriteToOutputStream(byte[] bytes)
        {
#if NETFX_CORE || WINDOWS_PHONE
            var thread = WriteToOutputStreamAsync(bytes);
            thread.Wait();
#else
            throw new PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE || WINDOWS_PHONE

        private async Task WriteToOutputStreamAsync(byte[] bytes)
        {
            if (_socket == null) return;
            _writer = new DataWriter(_socket.OutputStream);
            _writer.WriteBytes(bytes);

            var debugString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            try
            {
                await _writer.StoreAsync();
                await _socket.OutputStream.FlushAsync();

                _writer.DetachStream();
                _writer.Dispose();
            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error if fatal and retry will likely fail.
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    // TODO abort any retry attempts on Unity side
                    throw;
                }
            }
        }

#endif

    }
}
