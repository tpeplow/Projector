using Projector.Model;

namespace Projector.OutputPipeline
{
    public interface IFileTypeHierarchyBuilder
    {
        IFileTypeHierarchy Generate(CodeDirectory codeDirectory);
    }

    public class FileTypeHierarchyBuilder : IFileTypeHierarchyBuilder
    {
        public IFileTypeHierarchy Generate(CodeDirectory codeDirectory)
        {
            throw new System.NotImplementedException();
        }
    }
}