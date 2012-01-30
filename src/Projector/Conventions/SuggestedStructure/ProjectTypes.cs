using System.Collections.Generic;
using Projector.Reflection;

namespace Projector.Conventions.SuggestedStructure
{
    public static class ProjectTypes
    {
        public static IEnumerable<ProjectType> All
        {
            get { return typeof(ProjectTypes).CreateInstancesOfNestedTypes<ProjectType>(); }
        }

        public static ProjectType Default { get { return new Assembly(); } }

        public class Exe : ProjectType
        {
            public override string OutputType { get { return "Exe"; } }

            public override string Name { get { return "Exe"; } }

            public override IEnumerable<string> NamingConventions
            {
                get
                {
                    yield return "Console";
                    yield return "Exe";
                }
            }
        }

        public class Assembly : ProjectType
        {
            public override string OutputType { get { return "Library"; } }

            public override string Name { get { return "Library"; } }

            public override IEnumerable<string> NamingConventions { get { yield break; } }
        }

        public class Web : ProjectType
        {
            public override string OutputType { get { return "Library"; } }

            public override string Name { get { return "Web"; } }

            public override IEnumerable<string> NamingConventions
            {
                get { yield return "Web"; }
            }
        }
    }
}