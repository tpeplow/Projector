using System.Collections.Generic;
using Projector.IO;

namespace Projector.Conventions.SuggestedStructure
{
    public class SuggestedStructureConventionRegistrar : IConventionRegistrar
    {
        public IEnumerable<IOutputConvention> OutputConventions
        {
            get 
            { 
                return new[]
                {
                    new SuggestedStructureConvention(),
                }; 
            }
        }

        public IEnumerable<IModifyFileSystemConvention> ModifyFileSystemConventions
        {
            get
            {
                return new IModifyFileSystemConvention[]
                {
                    new GitIgnoreGenerator(new FileSystem(), new ResourceProvider()),
                    new ProjectFileIgnoreGenerator(new FileSystem(), new ResourceProvider()), 
                    new FileTypeGenerator(new FileSystem(), new ResourceProvider()), 
                };
            }
        }
    }
}