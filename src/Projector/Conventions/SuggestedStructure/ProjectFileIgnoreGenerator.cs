using Projector.IO;
using Projector.Parser;

namespace Projector.Conventions.SuggestedStructure
{
    public class ProjectFileIgnoreGenerator : CreateFileIfNotExists<ProjectFileIgnoreGenerator>
    {
        public ProjectFileIgnoreGenerator(IResourceProvider resourceProvider) 
            : base(resourceProvider, ParserRegistry.IgnoreFileName, "Resources.Ignore.txt")
        {
        }
    }
}