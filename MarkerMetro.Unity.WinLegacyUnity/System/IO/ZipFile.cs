using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if NETFX_CORE
using System.IO.Compression;
using Windows.Storage;
using Windows.ApplicationModel;
#endif

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class ZipFile
    {
        public Encoding AlternateEncoding { get; set; }
        public ZipOption AlternateEncodingUsage { get; set; }
        public byte[] Bytes { get; set; }
        private List<ZipEntry> _zipEntries;

        #if NETFX_CORE
        StorageFile _storageFile;
        ZipArchive _zipArchive = null;
        //MemoryStream _stream;
        #endif


        // TODO : Create the zip file temp_name specific to the device

        public ZipFile()
        {
        }

        public ZipEntry this[string key]
        {
            get
            {
                return _zipEntries.Find(z => z.Key == key);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // NOTE: async methods CANNOT be exposed in the .net 3.5 unity assembly. 
        // You can use async methods internally in win 8.1 libraries, but must call them via a s
        // a standard .net method either with a .Wait on the Task to force syncrhonicity (see previous integration for File)
        // or using a standard callback approach

#if NETFX_CORE

        public async void Create(string fileName)
        {
            _storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("output.zip");
        }

        private async void AddEntryAsync(string key, byte[] bytes)
        {
            var zipFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("output.zip", CreationCollisionOption.OpenIfExists);
            using (var zipStream = await zipFile.OpenStreamForWriteAsync())
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry(key);
                    //Stream byteStream = new MemoryStream(bytes);
                    using (StreamWriter writer = new StreamWriter(new MemoryStream(bytes)))
                    {
                        writer.Write(bytes);
                        //_zipEntries.Add(new ZipEntry { Key = key, Bytes = bytes });
                    }
                }
            }

            var entry = _zipArchive.CreateEntry(key);

            using (var writer = new StreamWriter(entry.Open()))
                writer.Write(bytes);
        }


        /// <summary>
        /// Create and return a ZipFile from the zipped bytes
        /// </summary>
        /// <param name="compressedStream"></param>
        /// <returns></returns>
        //private static async ZipFile ReadAsync(MemoryStream stream)
        //{
        //    var zipArchiveFile = await ApplicationData.Current.LocalFolder.GetFileAsync("output.zip");
        //    using (var zipStream = await zipArchiveFile.OpenReadAsync())
        //    {
        //        using (var zipArchive = new ZipArchive(zipStream.AsStream()))
        //        {
        //            foreach (var archiveEntry in zipArchive.Entries)
        //            {
        //                using (var outStream = archiveEntry.Open())
        //                {
        //                    // unzip file to app's LocalFolder
        //                    var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(archiveEntry.Name);
        //                    using (var inStream = await file.OpenStreamForWriteAsync())
        //                    {
        //                        await outStream.CopyToAsync(inStream);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return zipArchiveFile;
        //}

        /// <summary>
        /// Pass compressed byte[] to the Zip library, read it and decompress then return the created ZipFile from this stream
        /// </summary>
        /// <param name="compressedStream"></param>
        public async void ReadAsync(MemoryStream compressedStream)
        {
            var zipArchiveFile = await ApplicationData.Current.LocalFolder.GetFileAsync("output.zip");
            var zipFile = new ZipFile();
            

            using (var zipStream = await zipArchiveFile.OpenStreamForReadAsync())
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
                {                    
                    //ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                    //using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    //{
                    //    writer.Write();
                    //    writer.WriteLine("Information about this package.");
                    //    writer.WriteLine("========================");
                    //}
                }
            }
        }
        //public async void Save(MemoryStream stream)
        //{
        //    var zipFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("output.zip", CreationCollisionOption.ReplaceExisting);
        //    using (var zipStream = await zipFile.OpenStreamForWriteAsync())
        //    {
        //        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
        //        {
        //            ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
        //            using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
        //            {
        //                writer.WriteLine("Information about this package.");
        //                writer.WriteLine("========================");
        //            }
        //        }
        //    }
        //}

#endif
        public void AddEntry(string key, byte[] bytes)
        {
            //_zipEntries.Add(new ZipEntry { Key = key, Bytes = bytes });
            //AddEntryAsync(key, bytes);
            throw new NotImplementedException();
        }

        public void AddEntry(string key, MemoryStream stream)
        {
            //_zipEntries.Add(new ZipEntry { Key = key, MemoryStream = stream });
            throw new NotImplementedException();
        }

        public ZipFile Read(MemoryStream stream)
        {
            //return ReadAsync(stream);
            throw new NotImplementedException();
        }

        public void Save(MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool ContainsEntry(string key)
        {
            return _zipEntries.FirstOrDefault(z => z.Key == key) != null;
        }

    }
}

