using System.Collections.Generic;

namespace NoSln.IO
{
    public interface IDirectory
    {
        string Path { get; }
        IEnumerable<IFile> Files { get; }
        IEnumerable<IDirectory> Directories { get; }
    }
}