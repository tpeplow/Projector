using System.Collections.Generic;
using System.Linq;
using Projector.Conventions.SuggestedStructure;

namespace Projector.Conventions
{
    public static class DefaultConventions
    {
        static readonly IEnumerable<IConventionRegistrar> ConventionRegistrars; 
        static DefaultConventions()
        {
            ConventionRegistrars = new IConventionRegistrar[]
            {
                new SuggestedStructureConventionRegistrar(), 
            };
        }

        public static IEnumerable<IOutputConvention> CreateOutputConventions()
        {
            return ConventionRegistrars.SelectMany(x => x.OutputConventions);
        }

        public static IEnumerable<IModifyFileSystemConvention> CreateFileSystemConventions()
        {
            return ConventionRegistrars.SelectMany(x => x.ModifyFileSystemConventions);
        }
    }
}