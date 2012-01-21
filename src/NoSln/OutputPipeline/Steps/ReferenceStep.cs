using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
{
    public class ReferenceStep : IOutputPipelineStep
    {
        readonly IRelativePathGenerator relativePathGenerator;

        public ReferenceStep(IRelativePathGenerator relativePathGenerator)
        {
            this.relativePathGenerator = relativePathGenerator;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            foreach (var reference in codeDirectory.References)
            {
                var project = solution.GetProject(codeDirectory.Project.AssemblyName);
                var referencedProject = solution.FindProject(reference.Name);
                if (referencedProject != null)
                {
                    project.AddReference(new ProjectReference
                    {
                        Project = referencedProject, 
                        RelativePathToProject = relativePathGenerator.GeneratePath(project.Path, referencedProject.GeneratedProjectFilePath)
                    });
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