using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projector.IO
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

        public IDirectory GetDirectory(string path)
        {
            return new Directory(path);
        }

        public void WriteFile(string path, string contents)
        {
            System.IO.File.WriteAllText(path, contents);
        }

        private class Directory : IDirectory
        {
            public Directory(string path)
            {
                Path = path;
            }

            public string Path { get; private set; }

            public string Name
            {
                get { return System.IO.Path.GetFileName(Path); }
            }

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

            public string FileName
            {
                get { return Path.GetFileName(FilePath); }
            } 

            public string Contents
            {
                get { return System.IO.File.ReadAllText(FilePath); }
            }
        }
    }
}