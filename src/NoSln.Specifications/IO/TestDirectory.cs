using System.Collections.Generic;
using NoSln.IO;

namespace NoSln.Specifications.IO
{
    public class TestDirectory : IDirectory
    {
        public string Path { get; set; }

        public IEnumerable<IFile> Files { get; set; }

        public IEnumerable<IDirectory> Directories { get; set; }
    }
}