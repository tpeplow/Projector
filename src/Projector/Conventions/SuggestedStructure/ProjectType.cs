using System;
using System.Collections.Generic;

namespace Projector.Conventions.SuggestedStructure
{
    public abstract class ProjectType
    {
        public abstract string OutputType { get; }
        public abstract string Name { get; }
        public abstract IEnumerable<string> NamingConventions { get; }

        public virtual string TemplateName { get { return "Resources.ProjectTemplate.txt"; } }

        public virtual Guid ProjectTypeGuid { get { return Guid.Parse("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"); } }
    }
}