using System.IO;
using NoSln.Collections;
using NoSln.Model;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline.Steps
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
                    GeneratedProjectFilePath = Path.Combine(codeDirectory.Path, codeDirectory.Project.Name + codeDirectory.Project.Extension)
                });
            }

            codeDirectory.Directories.Each(x => Execute(solution, x));
        }
    }
}