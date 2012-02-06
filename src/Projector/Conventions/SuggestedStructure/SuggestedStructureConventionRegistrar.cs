using System.Collections.Generic;
using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class SuggestedStructureConventionRegistrar : IConventionRegistrar
    {
        public IEnumerable<IOutputConvention> OutputConventions
        {
            get { yield return new SuggestedStructureConvention(); }
        }

        public IEnumerable<IModifyFileSystemConvention> ModifyFileSystemConventions
        {
            get
            {
                var resourceProvider = new ResourceProvider();
                yield return new GitIgnoreGenerator(resourceProvider);
                yield return new ProjectFileIgnoreGenerator(resourceProvider);
                yield return new FileTypeGenerator(resourceProvider);
                yield return new TemplateFolderGenerator(resourceProvider);
            }
        }
    }
}