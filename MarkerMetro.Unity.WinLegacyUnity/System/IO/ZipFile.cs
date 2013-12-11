using System;
using System.Collections.Generic;
using System.IO;
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
        private List<ZipEntry> _zipEntries;

        #if NETFX_CORE
        StorageFile _storageFile;
        ZipArchive _zipArchive = null;
        //MemoryStream _stream;
        #endif

        public ZipFile()
        {
            //Create(fileName);
            throw new NotImplementedException();
        }

        public ZipEntry this[string key]
        {
            get
            {
                throw new NotImplementedException();
                //return _zipEntries[key];
            }
            set
            {
                throw new NotImplementedException();
                //_zipEntries[key] = value;
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

        // Open the existing zip file and add an entry...
        public async void AddEntry(string fileName, string key, byte[] bytes)
        {
            var zipFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            using (var zipStream = await zipFile.OpenStreamForWriteAsync())
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry(key);
                    //Stream byteStream = new MemoryStream(bytes);
                    using (StreamWriter writer = new StreamWriter(new MemoryStream(bytes)))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }
                }
            }

            var entry = _zipArchive.CreateEntry(key);

            using (var writer = new StreamWriter(entry.Open()))
                writer.Write(bytes);


        }

        

        public async void Read()
        {
            var zipFile = await ApplicationData.Current.LocalFolder.GetFileAsync("output.zip");
            using (var zipStream = await zipFile.OpenReadAsync())
            {
                using (var archive = new ZipArchive(zipStream.AsStream()))
                {
                    foreach (var archiveEntry in archive.Entries)
                    {
                        using (var outStream = archiveEntry.Open())
                        {
                            // unzip file to app's LocalFolder
                            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(archiveEntry.Name);
                            using (var inStream = await file.OpenStreamForWriteAsync())
                            {
                                await outStream.CopyToAsync(inStream);
                            }
                        }
                    }
                }
            }
        }

        public async void Save()
        {
            var zipFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("output.zip", CreationCollisionOption.ReplaceExisting);
            using (var zipStream = await zipFile.OpenStreamForWriteAsync())
            {
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }
                }
            }
        }

#endif
        // TODO : Make these methods use Task
        public void AddEntry(string key, byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void AddEntry()
        {
            throw new NotImplementedException();
        }

        public static ZipFile Read(MemoryStream stream)
        {
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

        public bool ContainsEntry(string entry)
        {
            throw new NotImplementedException();
        }

    }
}

