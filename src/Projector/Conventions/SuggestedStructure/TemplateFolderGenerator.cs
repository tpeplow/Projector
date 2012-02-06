using System;
using System.Linq;
using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class TemplateFolderGenerator : IModifyFileSystemConvention
    {
        readonly IResourceProvider resourceProvider;

        public TemplateFolderGenerator(IResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
        }

        public void Update(IDirectory directory)
        {
            if (directory.Directories.Any(x => x.Name.Equals("_templates", StringComparison.CurrentCultureIgnoreCase)))
                return;

            var templatesDirectory = directory.CreateChildDirectory("_templates");

            foreach (var projectTemplates in ProjectTypes.All)
            {
                templatesDirectory.WriteFile(projectTemplates.Name + ".nosln", resourceProvider.ReadResource<TemplateFolderGenerator>(projectTemplates.TemplateName));
            }
        }
    }
}