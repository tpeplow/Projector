using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class ProjectFileIgnoreGenerator : CreateFileIfNotExists<ProjectFileIgnoreGenerator>
    {
        public ProjectFileIgnoreGenerator(IFileSystem fileSystem, IResourceProvider resourceProvider) 
            : base(fileSystem, resourceProvider, "Ignore.nosln", "Resources.Ignore.txt")
        {
        }
    }
}