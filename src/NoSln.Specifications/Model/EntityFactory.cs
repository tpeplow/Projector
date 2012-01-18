using System;
using System.Collections.Generic;
using NoSln.Collections;
using NoSln.Model;

namespace NoSln.Specifications.Model
{
    public static class EntityFactory
    {
        public static CodeDirectory CreateCodeDirectory(string name)
        {
            return new CodeDirectory(name, ".");
        }

        public static CodeDirectory AddProject(this CodeDirectory codeDirectory, string projectName, params string[] references)
        {
            var projectDirectory = new CodeDirectory(projectName, projectName + "\\path")
                                       {
                                           Project = new ProjectInfo(projectName, "exe", projectName + ".namespace", Guid.NewGuid(), projectName)
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