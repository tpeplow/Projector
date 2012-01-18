using NoSln.OutputPipeline;
using NoSln.Parser;

namespace NoSln
{
    public class SolutionProjector
    {
        readonly ISolutionBuilder solutionBuilder;
        readonly IOutputPipeline outputPipeline;

        public SolutionProjector(ISolutionBuilder solutionBuilder, IOutputPipeline outputPipeline)
        {
            this.solutionBuilder = solutionBuilder;
            this.outputPipeline = outputPipeline;
        }

        public void ProjectFiles(string path)
        {
            var solutionCodeDirectory = solutionBuilder.BuildFromPath(path);
            outputPipeline.Execute(solutionCodeDirectory);
        }
    }
}