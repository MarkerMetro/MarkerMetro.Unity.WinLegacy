using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;

#if NETFX_CORE
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using MarkerMetro.Unity.WinLegacy.Runtime.Remoting.Messaging;
using MarkerMetro.Unity.WinLegacy.Plugin.IO;
#else
using System.Threading;
#endif

namespace MarkerMetro.Unity.WinLegacy.Net.Sockets
{
    public enum SocketType
    {
        Dgram,
        Raw,
        Rdm,
        Seqpacket,
        Stream,
        Unknown
    }

    public class Socket
    {
#if NETFX_CORE
        DatagramSocket _datagramSocket = null;
        DataWriter _writer = null;
        DataReader _reader = null;
        SocketType _socketType = SocketType.Unknown;
#endif
        public int Available
        {
            get
            {
#if NETFX_CORE
                if (_reader == null)
                {
                    return 0;
                }
                else
                {
                    return (int)_reader.UnconsumedBufferLength;
                }
#else
                return 0;
#endif
            }
        }


        public Socket (SocketType socketType)
        {
#if NETFX_CORE
            _socketType = socketType;
            switch (socketType)
            {
                case SocketType.Dgram:
                    _datagramSocket = new DatagramSocket();
                    _datagramSocket.MessageReceived += DatagramSocketMessageReceived;
                    break;
                default:
                    throw new NotImplementedException();
            }
#endif
        }

        public IAsyncResult BeginConnect(string host, string port, AsyncCallback requestCallback, object state)
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

        public IAsyncResult BeginSend(byte[] buffer, int offset, int size, int socketFlags, AsyncCallback requestCallback, object state)
        {
#if NETFX_CORE
            var task = Send(buffer, offset, size);
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

        public void EndSend(IAsyncResult result)
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

        public void Receive(byte[] buffer)
        {
#if NETFX_CORE
            if (_reader == null)
            {
                return;
            }
            _reader.ReadBytes(buffer);
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public void Close()
        {
#if NETFX_CORE
            if (_reader != null)
            {
                _reader.DetachStream();
                _reader.Dispose();
            }
            if (_datagramSocket != null)
            {
                _datagramSocket.MessageReceived -= DatagramSocketMessageReceived;
                _datagramSocket.Dispose();
                _datagramSocket = null;
            }
#else
            throw new PlatformNotSupportedException();
#endif
        }

#if NETFX_CORE
        private async Task EnsureSocket(string hostName, string port)
        {
            try
            {
                var host = new HostName(hostName);
                switch (_socketType)
                {
                    case SocketType.Dgram:
                        await _datagramSocket.ConnectAsync(host, port);
                        break;
                }
            }
            catch (Exception ex)
            {
                Close();

                // If this is an unknown status it means that the error is fatal and retry will likely fail.
                if (SocketError.GetStatus(ex.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }

        private async Task Send(byte[] buffer, int offset, int size)
        {
            if (_datagramSocket == null) return;
            _writer = new DataWriter(_datagramSocket.OutputStream);
            _writer.WriteBytes(buffer);

            try
            {
                await _writer.StoreAsync();
                await _datagramSocket.OutputStream.FlushAsync();

                _writer.DetachStream();
                _writer.Dispose();
            }
            catch (Exception exception)
            {
                // If this is an unknown status it means that the error if fatal and retry will likely fail.
                if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                {
                    throw;
                }
            }
        }

        void DatagramSocketMessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            _reader = args.GetDataReader();
        }
#endif
    }
}
