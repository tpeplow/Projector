using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class GitIgnoreGenerator : CreateFileIfNotExists<GitIgnoreGenerator>
    {
        public GitIgnoreGenerator(IResourceProvider resourceProvider)
            : base(resourceProvider, ".gitignore", "Resources.GitIgnore.txt")
        {
        }
    }
}