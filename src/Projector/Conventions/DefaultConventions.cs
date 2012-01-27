using System.Collections.Generic;
using Projector.Conventions.SuggestedStructure;

namespace Projector.Conventions
{
    public static class DefaultConventions
    {
        public static IEnumerable<IOutputConvention> CreateOutputConventions()
        {
            return new IOutputConvention[]
            {
                new SuggestedStructureConvention(), 
                new LibHintPathGeneratorStep()
            };
        }

        public static IEnumerable<IModifyFileSystemConvention> CreateFileSystemConventions()
        {
            return new IModifyFileSystemConvention[]
            {
                new GitIgnoreGenerator()
            };
        }
    }
}