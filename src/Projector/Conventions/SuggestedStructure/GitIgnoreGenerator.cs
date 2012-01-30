using System.IO;
using System.Linq;
using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class GitIgnoreGenerator : IModifyFileSystemConvention
    {
        readonly IFileSystem fileSystem;
        readonly IResourceProvider resourceProvider;

        public GitIgnoreGenerator(IFileSystem fileSystem, IResourceProvider resourceProvider)
        {
            this.fileSystem = fileSystem;
            this.resourceProvider = resourceProvider;
        }

        public void Update(IDirectory directory)
        {
            if (directory.Files.Any(x => x.FileName == ".gitignore"))
            {
                return;
            }

            fileSystem.WriteFile(Path.Combine(directory.Path, ".gitignore"), resourceProvider.ReadResource<GitIgnoreGenerator>("Resources.GitIgnore.txt"));
        }
    }
}