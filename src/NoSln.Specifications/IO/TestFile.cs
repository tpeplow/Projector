using NoSln.IO;

namespace NoSln.Specifications.IO
{
    public class TestFile : IFile
    {
        public string FilePath { get; set; }
        public string Contents { get; set; }
    }
}