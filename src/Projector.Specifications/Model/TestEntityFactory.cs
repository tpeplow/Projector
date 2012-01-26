using System;
using System.Collections.Generic;
using Projector.Collections;
using Projector.Model;

namespace Projector.Specifications.Model
{
    public static class TestEntityFactory
    {
        public static CodeDirectory CreateCodeDirectory(string name)
        {
            return new CodeDirectory(name, "c:\\" + name + "\\path");
        }

        public static CodeDirectory AddProject(this CodeDirectory codeDirectory, string projectName, params string[] references)
        {
            var projectDirectory = new CodeDirectory(projectName, "c:\\" + projectName + "\\path")
                                       {
                                           Project = new ProjectInfo
                                                         {
                                                             Name = projectName,
                                                             OutputType = "exe",
                                                             Namespace = projectName + ".namespace",
                                                             Guid = Guid.NewGuid(),
                                                             AssemblyName = projectName,
                                                             Extension = ".csproj",
                                                             ProjectTypeGuid = Guid.NewGuid()
                                                         }
                                       };
            foreach (var reference in references)
            {
                projectDirectory.References.Add(new ReferenceInformation(reference, reference + "\\path"));
            }
            codeDirectory.AddCodeDirectory(projectDirectory);
            return projectDirectory;
        }

        public static FileInclusionPolicy CreateInclusionPolicy(IEnumerable<string> excludes = null, IEnumerable<string> includes = null)
        {
            var policy = new FileInclusionPolicy();
            if (excludes != null) excludes.Each(policy.AddExclude);
            if(includes != null) includes.Each(policy.AddInclude);
            return policy;
        }
    }
}