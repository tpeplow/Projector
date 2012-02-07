using System.Collections.Generic;
using Projector.Parser;
using Projector.Serializers;

namespace Projector.Conventions.ReferenceSync
{
    public class ReferenceSyncRegistrar : IConventionRegistrar
    {
        public IEnumerable<IOutputConvention> OutputConventions
        {
            get { yield break; }
        }

        public IEnumerable<IModifyFileSystemConvention> ModifyFileSystemConventions
        {
            get { yield return new ReferenceSyncConvention(new ReferenceSerializer(), new ReferenceParser()); }
        }
    }
}