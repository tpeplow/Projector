using Projector.Model;

namespace Projector.OutputPipeline
{
    public interface IFileInclusionHierarchy
    {
        FileInclusionPolicy Policy { get; }
        bool ShouldInclude(string path);
    }
}