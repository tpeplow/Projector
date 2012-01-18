using NoSln.Model;

namespace NoSln.OutputPipeline
{
    public class FileInclusionHierarchy : IFileInclusionHierarchy
    {
        public FileInclusionHierarchy(FileInclusionPolicy policy)
        {
            Policy = policy;
        }

        public FileInclusionPolicy Policy { get; private set; }
    }
}