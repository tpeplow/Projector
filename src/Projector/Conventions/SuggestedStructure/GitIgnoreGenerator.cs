using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class GitIgnoreGenerator : CreateFileIfNotExists<GitIgnoreGenerator>
    {
        public GitIgnoreGenerator(IFileSystem fileSystem, IResourceProvider resourceProvider)
            : base(fileSystem, resourceProvider, ".gitignore", "Resources.GitIgnore.txt")
        {
        }
    }
}