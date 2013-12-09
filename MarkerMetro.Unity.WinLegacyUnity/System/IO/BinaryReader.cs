using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class BinaryReader
    {
        public Stream BaseStream { get; set; }

        public BinaryReader(Stream input)
        {
            
        }

        public string ReadString()
        {
            return "";
        }

        public int ReadInt32()
        {
            return 0;
        }

        public byte ReadByte()
        {
            return new byte();
        }

        public bool ReadBoolean()
        {
            return false;
        }

        public float ReadSingle()
        {
            return new float();
        }

        public uint ReadUInt32()
        {
            return new uint();
        }

        public void Close()
        { }
    }
}
