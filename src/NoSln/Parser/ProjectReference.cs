using System;
using Machine.Specifications.Annotations;

namespace NoSln.Parser
{
    public class ProjectReference
    {
        public ProjectReference([NotNull] string name, string hintPath = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            Name = name;
            HintPath = hintPath;
        }

        public string Name { get; private set; }

        public string HintPath { get; private set; }
    }
}