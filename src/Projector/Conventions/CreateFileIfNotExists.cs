using System.IO;
using System.Linq;
using Projector.IO;

namespace Projector.Conventions
{
    public class CreateFileIfNotExists<TResourceRelativeTo> : IModifyFileSystemConvention
    {
        readonly IResourceProvider resourceProvider;
        readonly string fileName;
        readonly string resourceName;

        protected CreateFileIfNotExists(IResourceProvider resourceProvider, string fileName, string resourceName)
        {
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

            directory.WriteFile(fileName, resourceProvider.ReadResource<TResourceRelativeTo>(resourceName));
        }
    }
}