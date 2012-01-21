using Projector.IO;

namespace Projector.Specifications.IO
{
    public class TestFile : IFile
    {
        public TestFile()
        {
        }

        public TestFile(string fileName)
        {
            FileName = fileName;
        }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string Contents { get; set; }
    }
}