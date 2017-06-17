using System;
using System.Collections.Generic;
using System.IO;

namespace Helpers
{
    public class FileReader
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public IEnumerable<string> ReadLine()
        {
            if (!Exists())
                throw new Exception("File does not exists.");

            // TODO ".txt" is specific to our implementation
            if (!HasExtension(".txt"))
                throw new Exception("Wrong extension, only '.txt' is supported.");

            if (IsEmpty())
                throw new Exception("This file is empty.");

            string line;
            using (var reader = File.OpenText(_path))
            {
                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }
        }

        public bool HasExtension(string extension)
        {
            return Path.GetExtension(_path) == extension;
        }

        public bool IsEmpty()
        {
            return new FileInfo(_path).Length == 0;
        }

        public bool Exists()
        {
            return File.Exists(_path);
        }
    }
}