using System.Collections.Generic;

namespace NoSln.IO
{
    public interface IFileSystem
    {
        IEnumerable<IFile> GetFilesInFolder(string path);
        IEnumerable<IDirectory> GetDirectories(string path);
    }
}