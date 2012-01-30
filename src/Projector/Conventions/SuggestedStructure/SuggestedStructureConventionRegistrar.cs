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
                yield return new GitIgnoreGenerator(new FileSystem(), new ResourceProvider());
                yield return new ProjectFileIgnoreGenerator(new FileSystem(), new ResourceProvider());
                yield return new FileTypeGenerator(new FileSystem(), new ResourceProvider());
            }
        }
    }
}