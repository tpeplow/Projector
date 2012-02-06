using System.Collections.Generic;
using Projector.IO;

namespace Projector.Specifications.IO
{
    public class TestDirectory : IDirectory
    {
        public TestDirectory()
        {
            Directories = new List<IDirectory>();
            Files = new List<IFile>();
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public IEnumerable<IFile> Files { get; set; }

        public IEnumerable<IDirectory> Directories { get; set; }
        
        public IDirectory CreateChildDirectory(string name)
        {
            var directory = new TestDirectory {Name = name};
            var subDirectories =new List<IDirectory>(Directories ?? new IDirectory[0]) {directory};
            Directories = subDirectories;
            return directory;
        }

        public void WriteFile(string name, string contents)
        {
            Files = new List<IFile>(Files ?? new IFile[0]) { new TestFile(name) { Contents = contents }};
        }
    }
}