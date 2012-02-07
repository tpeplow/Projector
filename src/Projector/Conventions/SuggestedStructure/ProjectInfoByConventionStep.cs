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
        readonly IGuidGenerator guidGenerator;

        public ProjectInfoByConventionStep(IProjectTypeNamingConvention projectTypeNamingConvention, IGuidGenerator guidGenerator)
        {
            this.projectTypeNamingConvention = projectTypeNamingConvention;
            this.guidGenerator = guidGenerator;
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
                projectFolder.Project = (new ProjectInfo
                {
                    AssemblyName = projectFolder.Name,
                    Name = projectFolder.Name,
                    Namespace = projectFolder.Name,
                    OutputType = projectType.OutputType,
                    Guid = guidGenerator.Generate(),
                    Extension = ".csproj",
                    ProjectTypeGuid = projectType.ProjectTypeGuid,
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