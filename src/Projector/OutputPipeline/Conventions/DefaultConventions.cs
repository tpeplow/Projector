using System.Collections.Generic;

namespace Projector.OutputPipeline.Conventions
{
    public static class DefaultConventions
    {
        public static IEnumerable<IOutputConvention> Create()
        {
            return new IOutputConvention[]
            {
                new LibHintPathGeneratorStep()
            };
        }
    }
}