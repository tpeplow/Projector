using System.IO;
using System.Linq;
using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
{
    public class RelativeReferencePathStep : IOutputPipelineStep
    {
        readonly IRelativePathGenerator relativePathGenerator;

        public RelativeReferencePathStep(IRelativePathGenerator relativePathGenerator)
        {
            this.relativePathGenerator = relativePathGenerator;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            solution.Projects.Each(FixReferencesForProject);
        }

        void FixReferencesForProject(Project project)
        {
            var absHintPaths = project.AssemblyReferences.Where(x => Path.IsPathRooted(x.HintPath));
            absHintPaths.Each(x => x.HintPath = relativePathGenerator.GeneratePath(project.Path, x.HintPath));
        }
    }
}