using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkerMetro.Unity.WinLegacy.IO;
namespace MarkerMetro.Unity.WinLegacy.Plugin.IO
{
    /// <summary>
    /// MSDN reference: http://msdn.microsoft.com/en-us/library/system.io.streamreader.aspx.
    /// </summary>
    public class StreamReader : System.IO.TextReader
    {
        readonly System.IO.StreamReader _actual;
                
        internal static int DefaultBufferSize { get { return 0x400; } }

        public StreamReader()
        {
            throw new NotSupportedException();
        }

        public StreamReader(string path) : this(new FileStream(path, FileMode.Open))
        { }

        public StreamReader(System.IO.Stream stream) : this(stream, true) 
        { }
        
        public StreamReader(System.IO.Stream stream, bool detectEncodingFromByteOrderMarks) : this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks)
        { }
        
        public StreamReader(System.IO.Stream stream, Encoding encoding) : this(stream, encoding, false)
        { }

        public StreamReader(System.IO.Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks) : this(stream, encoding, detectEncodingFromByteOrderMarks, DefaultBufferSize)
        { }

        public StreamReader(System.IO.Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
        {
            _actual = new System.IO.StreamReader(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize);
        }

        public virtual System.IO.Stream BaseStream 
        {
            get { return _actual.BaseStream; }
        }

        public virtual Encoding CurrentEncoding 
        {
            get { return _actual.CurrentEncoding; } 
        }
        
        public bool EndOfStream 
        {
            get { return _actual.EndOfStream; }
        }

        public void DiscardBufferedData()
        {
            _actual.DiscardBufferedData();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                _actual.Dispose();
        }
        
        public override int Peek()
        {
            return _actual.Peek();
        }
        
        public override int Read()
        {
            return _actual.Read();
        }
        
        public override int Read(char[] buffer, int index, int count)
        {
            return _actual.Read(buffer, index, count);
        }
        
        public override int ReadBlock(char[] buffer, int index, int count)
        {
            return _actual.ReadBlock(buffer, index, count);
        }
        
        public override string ReadLine()
        {
            return _actual.ReadLine();
        }
        
        public override string ReadToEnd()
        {
            return _actual.ReadToEnd();
        }
    }
}
