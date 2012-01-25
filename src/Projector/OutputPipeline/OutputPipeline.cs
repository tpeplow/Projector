using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline
{
    public interface IOutputPipeline
    {
        void Execute(CodeDirectory solutionCodeDirectory);
    }

    public class OutputPipeline : IOutputPipeline
    {
        readonly IOutputPipelineStepsBuilder stepsBuilder;

        public OutputPipeline(IOutputPipelineStepsBuilder stepsBuilder)
        {
            this.stepsBuilder = stepsBuilder;
        }

        public void Execute(CodeDirectory solutionCodeDirectory)
        {
            var solution = new Solution { SolutionPath = solutionCodeDirectory.Path };
            var steps = stepsBuilder.BuildSteps();
            steps.Each(x => x.Execute(solution, solutionCodeDirectory));
        }
    }
}