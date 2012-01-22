using System.IO;
using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
{
    public class SolutionStructureStep : IOutputPipelineStep
    {
        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            if (codeDirectory.Project != null)
            {
                solution.AddProject(new Project
                {
                    Name = codeDirectory.Project.Name,
                    Path = codeDirectory.Path,
                    Guid = codeDirectory.Project.Guid,
                    Namespace = codeDirectory.Project.Namespace,
                    OutputType = codeDirectory.Project.OutputType,
                    AssemblyName = codeDirectory.Project.AssemblyName,
                    Extension = codeDirectory.Project.Extension,
                    GeneratedProjectFilePath = Path.Combine(codeDirectory.Path, codeDirectory.Project.Name + codeDirectory.Project.Extension),
                    ProjectTypeGuid = codeDirectory.Project.ProjectTypeGuid
                });
            }

            codeDirectory.Directories.Each(x => Execute(solution, x));
        }
    }
}