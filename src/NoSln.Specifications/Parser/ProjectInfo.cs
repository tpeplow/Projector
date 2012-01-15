using System;
using Machine.Specifications.Annotations;

namespace NoSln.Specifications.Parser
{
    public class ProjectInfo
    {
        public ProjectInfo([NotNull] string name, [NotNull] string outputType, [NotNull] string @namespace, Guid guid)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (outputType == null) throw new ArgumentNullException("outputType");
            if (@namespace == null) throw new ArgumentNullException("namespace");
            Name = name;
            OutputType = outputType;
            Namespace = @namespace;
            Guid = guid;
        }

        public string Name { get; private set; }

        public string OutputType { get; private set; }

        public string Namespace { get; private set; }

        public Guid Guid { get; private set; }
    }
}