using System;
using System.Collections.Generic;
using System.Linq;

namespace NoSln.IO
{
    public class FileSystem : IFileSystem
    {
        public IEnumerable<IFile> GetFilesInFolder(string path)
        {
            return System.IO.Directory.GetFiles(path).Select(x => new File(x));
        }

        public IEnumerable<IDirectory> GetDirectories(string path)
        {
            return System.IO.Directory.GetDirectories(path).Select(x => new Directory(x));
        }

        private class Directory : IDirectory
        {
            public Directory(string path)
            {
                Path = path;
            }

            public string Path { get; private set; }

            public IEnumerable<IFile> Files
            {
                get { return new FileSystem().GetFilesInFolder(Path); }
            }

            public IEnumerable<IDirectory> Directories
            {
                get { return new FileSystem().GetDirectories(Path); }
            }
        }

        private class File : IFile
        {
            public File(string filePath)
            {
                if (filePath == null) throw new ArgumentNullException("filePath");
                FilePath = filePath;
            }

            public string FilePath { get; private set; }

            public string Contents
            {
                get { return System.IO.File.ReadAllText(FilePath); }
            }
        }
    }
}