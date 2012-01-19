using System;
using System.Collections.Generic;

namespace NoSln.Model.Output
{
    public class Project
    {
        readonly List<AssemblyReference> assemblyReferences = new List<AssemblyReference>(); 
        readonly List<ProjectReference> projectReferences = new List<ProjectReference>(); 
        readonly List<ProjectFile> projectFiles = new List<ProjectFile>(); 

        public Project()
        {
            AssemblyReferences = assemblyReferences;
            ProjectReferences = projectReferences;
            Files = projectFiles;
        }

        public string Name { get; set; }

        public string AssemblyName { get; set; }

        public string Path { get; set; }

        public string Namespace { get; set; }

        public string OutputType { get; set; }

        public Guid Guid { get; set; }

        public IEnumerable<AssemblyReference> AssemblyReferences { get; private set; }

        public IEnumerable<ProjectReference> ProjectReferences { get; private set; }

        public IEnumerable<ProjectFile> Files { get; private set; }

        public ProjectTemplate ProjectTemplate { get; set; }

        public string Extension { get; set; }

        public void AddReference(AssemblyReference assemblyReference)
        {
            if (assemblyReference == null) throw new ArgumentNullException("assemblyReference");
            assemblyReferences.Add(assemblyReference);
        }

        public void AddReference(ProjectReference projectReference)
        {
            if (projectReference == null) throw new ArgumentNullException("projectReference");
            projectReferences.Add(projectReference);
        }

        public void AddFile(ProjectFile projectFile)
        {
            if (projectFile == null) throw new ArgumentNullException("projectFile");
            projectFiles.Add(projectFile);
        }
    }
}