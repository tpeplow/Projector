using System;
using System.Linq;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;
using Projector.Parser;

namespace Projector.Conventions.SuggestedStructure
{
    public class ProjectInfoByConventionStep : IOutputPipelineStep
    {
        readonly IProjectTypeNamingConvention projectTypeNamingConvention;

        public ProjectInfoByConventionStep(IProjectTypeNamingConvention projectTypeNamingConvention)
        {
            this.projectTypeNamingConvention = projectTypeNamingConvention;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            var sourceDirectory = codeDirectory.Directories.FirstOrDefault(x => IsSourceDirectory(x.Name));
            if (sourceDirectory == null) return;

            foreach (var projectFolder in sourceDirectory.Directories)
            {
                if (projectFolder.Files.Any(x => x.FileName.Equals(ParserRegistry.ProjectFileName, StringComparison.InvariantCultureIgnoreCase)))
                    continue;
                var projectType = projectTypeNamingConvention.GetProjectType(projectFolder.Name);
                solution.AddProject(new Project
                {
                    AssemblyName = projectFolder.Name,
                    Name = projectFolder.Name,
                    Namespace = projectFolder.Name,
                    OutputType = projectType.OutputType
                });
            }
        }

        static bool IsSourceDirectory(string directoryName)
        {
            return directoryName.Equals("src", StringComparison.InvariantCultureIgnoreCase)
                   || directoryName.Equals("source", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}