using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using MarkerMetro.Unity.WinLegacy.Plugin.IO;
using Windows.Foundation;
using System.Runtime.ExceptionServices;
#else
using System.Net.Sockets;
using System.Threading;
#endif

namespace MarkerMetro.Unity.WinLegacy.Net.Sockets
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient.aspx.
    /// </summary>
    public class TcpClient
    {
        public bool UsePlainSocket { get; set; }
   
#if NETFX_CORE
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
#if NETFX_CORE
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
#if NETFX_CORE
            var thread = EnsureSocket(hostName, port);
            thread.Wait();
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
        {
#if NETFX_CORE

            var task = EnsureSocket(host, port);
            TaskStateAsyncResult res = new TaskStateAsyncResult(task, state);

            task.ContinueWith((t) =>
            {
                if (requestCallback != null)
                {
                    requestCallback(res);
                }
            });

            return res;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void EndConnect(IAsyncResult result)
        {
#if NETFX_CORE
            var ar = (TaskStateAsyncResult)result;
            if (ar.InnerTask.IsFaulted)
                throw ar.InnerTask.Exception;
            return;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public Stream GetStream()
        {
#if NETFX_CORE
            if (_socket == null || !_isConnected) return null;
            return _readWriteStream;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void Close()
        {
#if NETFX_CORE  
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

        public void ReadFromInputStreamAsync(int size, Action<byte[]> successCallback, Action<Exception> failureCallback)
        {
#if NETFX_CORE
            var task = ReadFromInputStreamAsyncInner(size);
            task.ContinueWith((t) =>
            {
                if (t.IsFaulted && failureCallback != null)
                    failureCallback(t.Exception);
                else if (!t.IsFaulted && successCallback != null)
                    successCallback(t.Result);
            });
            
#else
            throw new PlatformNotSupportedException("TcpClient.ReadFromInputStream");
#endif
        }

#if NETFX_CORE

        private async Task<byte[]> ReadFromInputStreamAsyncInner(int size)
        {
            if (_socket == null) return null;
            DataReader reader = new DataReader(_socket.InputStream);
            reader.InputStreamOptions = InputStreamOptions.Partial;
            var count = await reader.LoadAsync((uint)size);
            byte[] bytesRead = null;
            if (count > 0)
            {
                bytesRead = new byte[count];
                reader.ReadBytes(bytesRead);
            }
            reader.DetachStream();
            reader.Dispose();
            return bytesRead;
        }
#endif

        public void WriteToOutputStream(byte[] bytes)
        {
#if NETFX_CORE
            var thread = WriteToOutputStreamAsyncInner(bytes);
            thread.Wait();
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void WriteToOutputStreamAsync(byte[] bytes, Action successCallback, Action<Exception> failureCallback)
        {
#if NETFX_CORE
            var task = WriteToOutputStreamAsyncInner(bytes);
            task.ContinueWith((t) =>
            {
                if (t.IsFaulted && failureCallback != null)
                    failureCallback(t.Exception);
                else if (!t.IsFaulted && successCallback != null)
                    successCallback();
            });
#else
            throw new PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE
        private async Task WriteToOutputStreamAsyncInner(byte[] bytes)
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
