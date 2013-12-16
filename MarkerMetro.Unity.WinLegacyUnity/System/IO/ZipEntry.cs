using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
#if NETFX_CORE
using Windows.Storage;
using System.IO.Compression;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class ZipEntry
    {
        public string Key { get; set; }
        public byte[] Bytes { get; set; }
        public MemoryStream MemoryStream { get; set; }

        public Encoding AlternateEncoding { get; set; }
        public ZipOption AlternateEncodingUsage { get; set; }

#if NETFX_CORE
        /// <summary>
        /// Extracts the contents of the ZipEntry to the specified stream
        /// </summary>
        /// <param name="stream"></param>
        //public async void Extract(MemoryStream stream)
        //{
        //    var zipFile = await ApplicationData.Current.LocalFolder.GetFileAsync("output.zip");
        //    using (var zipStream = await zipFile.OpenReadAsync())
        //    {
        //        using (var archive = new ZipArchive(zipStream.AsStream()))
        //        {
        //            foreach (var archiveEntry in archive.Entries)
        //            {
        //                if (archiveEntry.Name == Key)
        //                {
        //                    using (var outStream = archiveEntry.Open())
        //                    {
        //                        // Unzip entry to stream
        //                        var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(archiveEntry.Name);
        //                        using (var inStream = await file.OpenStreamForWriteAsync())
        //                        {
        //                            await stream.CopyToAsync(inStream);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
#endif

        public void Extract(MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        public void Extract(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
