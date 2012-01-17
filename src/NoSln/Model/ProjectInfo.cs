using System;
namespace NoSln.Model
{
    public class ProjectInfo
    {
        public ProjectInfo(string name, string outputType, string @namespace, Guid guid, string assemblyName)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (outputType == null) throw new ArgumentNullException("outputType");
            if (@namespace == null) throw new ArgumentNullException("namespace");
            if (assemblyName == null) throw new ArgumentNullException("assemblyName");
            Name = name;
            OutputType = outputType;
            Namespace = @namespace;
            Guid = guid;
            AssemblyName = assemblyName;
        }

        public string Name { get; private set; }

        public string OutputType { get; private set; }

        public string Namespace { get; private set; }

        public Guid Guid { get; private set; }

        public string AssemblyName { get; private set; }
    }
}