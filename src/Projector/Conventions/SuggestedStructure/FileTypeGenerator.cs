using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class FileTypeGenerator : CreateFileIfNotExists<FileTypeGenerator>
    {
        public FileTypeGenerator(IFileSystem fileSystem, IResourceProvider resourceProvider) 
            : base(fileSystem, resourceProvider, "FileTypes.nosln", "Resources.FileTypes.txt")
        {
        }
    }
}