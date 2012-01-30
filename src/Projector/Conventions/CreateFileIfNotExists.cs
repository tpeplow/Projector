using System.IO;
using System.Linq;
using Projector.IO;

namespace Projector.Conventions
{
    public class CreateFileIfNotExists<TResourceRelativeTo> : IModifyFileSystemConvention
    {
        readonly IFileSystem fileSystem;
        readonly IResourceProvider resourceProvider;
        readonly string fileName;
        readonly string resourceName;

        public CreateFileIfNotExists(IFileSystem fileSystem, IResourceProvider resourceProvider, string fileName, string resourceName)
        {
            this.fileSystem = fileSystem;
            this.resourceProvider = resourceProvider;
            this.fileName = fileName;
            this.resourceName = resourceName;
        }

        public virtual void Update(IDirectory directory)
        {
            if (directory.Files.Any(x => x.FileName == fileName))
            {
                return;
            }

            fileSystem.WriteFile(Path.Combine(directory.Path, fileName), resourceProvider.ReadResource<TResourceRelativeTo>(resourceName));
        }
    }
}