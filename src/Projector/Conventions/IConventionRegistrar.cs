using System.Collections.Generic;

namespace Projector.Conventions
{
    public interface IConventionRegistrar
    {
        IEnumerable<IOutputConvention> OutputConventions { get; }
        IEnumerable<IModifyFileSystemConvention> ModifyFileSystemConventions { get; } 
    }
}