using Projector.IO;
using Projector.Parser;

namespace Projector.Conventions.SuggestedStructure
{
    public class ProjectFileIgnoreGenerator : CreateFileIfNotExists<ProjectFileIgnoreGenerator>
    {
        public ProjectFileIgnoreGenerator(IFileSystem fileSystem, IResourceProvider resourceProvider) 
            : base(fileSystem, resourceProvider, ParserRegistry.IgnoreFileName, "Resources.Ignore.txt")
        {
        }
    }
}