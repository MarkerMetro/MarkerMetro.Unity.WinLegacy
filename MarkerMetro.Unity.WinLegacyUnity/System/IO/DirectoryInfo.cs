using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.IO
{
    public class DirectoryInfo
    {
        private readonly string path;

        public string FullName
        {
            get
            {
                return path;
            }
        }

        public DirectoryInfo(string path)
        {
            this.path = path; 
        }

        public void Create()
        {
            Directory.CreateDirectory(path);
        }

        public void Refresh()
        {
            // No need to do anything
        }

        public bool Exists
        {
            get
            {
                return Directory.Exists(path);
            }
        }

        public FileInfo[] GetFiles()
        {
            var files = Directory.GetFiles(path);
            if (files != null && files.Count() > 0)
            {
                return files.Select(x => new FileInfo(x)).ToArray();
            }
            return null;
        }
    }

}
