using System.Collections.Generic;

namespace Projector.IO
{
    public interface IDirectory
    {
        string Path { get; }
        string Name { get; }
        IEnumerable<IFile> Files { get; }
        IEnumerable<IDirectory> Directories { get; }
        IDirectory CreateChildDirectory(string name);
        void WriteFile(string name, string contents);
    }
}