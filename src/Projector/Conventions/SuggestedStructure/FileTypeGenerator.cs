using Projector.IO;
using Projector.Parser;

namespace Projector.Conventions.SuggestedStructure
{
    public class FileTypeGenerator : CreateFileIfNotExists<FileTypeGenerator>
    {
        public FileTypeGenerator(IFileSystem fileSystem, IResourceProvider resourceProvider) 
            : base(fileSystem, resourceProvider, ParserRegistry.FileTypesFileName, "Resources.FileTypes.txt")
        {
        }
    }
}