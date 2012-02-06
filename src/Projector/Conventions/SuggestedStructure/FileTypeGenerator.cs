using Projector.IO;
using Projector.Parser;

namespace Projector.Conventions.SuggestedStructure
{
    public class FileTypeGenerator : CreateFileIfNotExists<FileTypeGenerator>
    {
        public FileTypeGenerator(IResourceProvider resourceProvider) 
            : base(resourceProvider, ParserRegistry.FileTypesFileName, "Resources.FileTypes.txt")
        {
        }
    }
}