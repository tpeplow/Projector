using System;

namespace Projector.Model
{
    public class ProjectInfo
    {
        public string Name { get; set; }

        public string OutputType { get; set; }

        public string Namespace { get; set; }

        public Guid Guid { get; set; }

        public string AssemblyName { get; set; }

        public string Extension { get; set; }

        public Guid ProjectTypeGuid { get; set; }
    }
}