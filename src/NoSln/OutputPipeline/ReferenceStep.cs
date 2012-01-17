using NoSln.Collections;
using NoSln.Model;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline
{
    public class ReferenceStep : IOutputPipelineStep
    {
        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            foreach (var reference in codeDirectory.References)
            {
                var project = solution.GetProject(codeDirectory.Project.AssemblyName);
                var referencedProject = solution.FindProject(reference.Name);
                if (referencedProject != null)
                {
                    project.AddReference(new ProjectReference { Project = referencedProject });
                }
                else
                {
                    project.AddReference(new AssemblyReference { Name = reference.Name, HintPath = reference.HintPath });
                }
            }

            codeDirectory.Directories.Each(x => Execute(solution, x));
        }
    }
}