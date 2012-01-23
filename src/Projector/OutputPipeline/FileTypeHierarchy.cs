using Projector.Model;

namespace Projector.OutputPipeline
{
    public interface IFileTypeHierarchy
    {
        FileType GetFileType(string relativePath);
    }

    public class FileTypeHierarchy : IFileTypeHierarchy
    {
        public FileType GetFileType(string relativePath)
        {
            throw new System.NotImplementedException();
        }
    }
}