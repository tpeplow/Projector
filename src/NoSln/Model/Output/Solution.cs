using System;
using System.Collections.Generic;

namespace Projector.Model.Output
{
    public class Solution
    {
        readonly Dictionary<string, Project> projects;

        public Solution()
        {
            projects = new Dictionary<string, Project>();
            Projects = projects.Values;
        }

        public IEnumerable<Project> Projects { get; private set; }

        public string SolutionPath { get; set; }

        public Project GetProject(string assemblyName)
        {
            Project project;
            if (!projects.TryGetValue(assemblyName, out project))
            {
                throw new KeyNotFoundException(string.Format("Cannot find project called {0}", assemblyName));
            }
            return project;
        }

        public void AddProject(Project project)
        {
            if (project == null) throw new ArgumentNullException("project");
            projects.Add(project.AssemblyName, project);
        }

        public Project FindProject(string assemblyName)
        {
            Project project;
            projects.TryGetValue(assemblyName, out project);
            return project;
        }
    }
}