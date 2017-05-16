using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    using System.IO;

    public class FileReader
    {
        private readonly string _path;

        public FileReader(string path)
        {
            this._path = path;
        }

        public IEnumerable<string> Read()
        {
            if(!Exists())
                throw new Exception("File does not exists.");

            if(!HasExtension(".txt"))
                   throw new Exception("Wrong extension, only '.txt' is supported.");

            if(IsEmpty())
                   throw new Exception("This file is empty.");

            string line;
            using (var reader = File.OpenText(_path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private bool HasExtension(string extension)
        {
            return Path.GetExtension(_path) == extension;
        }

        private bool IsEmpty()
        {
            return new FileInfo(_path).Length == 0;
        }

        private bool Exists()
        {
            return File.Exists(_path);
        }
    }
}
