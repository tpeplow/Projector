using System.Collections.Generic;

namespace Projector.Conventions.LibHintPathGenerator
{
    public class LibHintPathConventionRegistrar : IConventionRegistrar
    {
        public IEnumerable<IOutputConvention> OutputConventions
        {
            get { yield return new LibHintPathGeneratorStep(); }
        }

        public IEnumerable<IModifyFileSystemConvention> ModifyFileSystemConventions
        {
            get { yield break; }
        }
    }
}