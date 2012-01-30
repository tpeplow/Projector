using System.Collections.Generic;

namespace Projector.Conventions.SuggestedStructure
{
    public abstract class ProjectType
    {
        public abstract string OutputType { get; }
        public abstract string Name { get; }
        public abstract IEnumerable<string> NamingConventions { get; }
    }
}