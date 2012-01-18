using NoSln.Model;

namespace NoSln.OutputPipeline
{
    public interface IFileInclusionHierarchy
    {
        FileInclusionPolicy Policy { get; }
    }
}