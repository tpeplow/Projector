using System.Collections.Generic;

namespace NoSln.IO
{
    public interface IFileSystem
    {
        IEnumerable<IFile> GetFilesInFolder(string path);
        IEnumerable<IDirectory> GetDirectories(string path);
        IDirectory GetDirectory(string path);
        void WriteFile(string path, string contents);
    }
}