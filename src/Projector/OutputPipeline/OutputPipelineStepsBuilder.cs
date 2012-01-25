using System.Collections.Generic;
using Projector.Collections;
using Projector.OutputPipeline.Conventions;
using Projector.OutputPipeline.Steps;

namespace Projector.OutputPipeline
{
    public interface IOutputPipelineStepsBuilder
    {
        IEnumerable<IOutputPipelineStep> BuildSteps();
    }

    public class OutputPipelineStepsBuilder : IOutputPipelineStepsBuilder
    {
         public IEnumerable<IOutputPipelineStep> BuildSteps()
         {
             var steps = DefaultPipelineSteps.Create();
             DefaultConventions.Create().Each(x => x.UpdateSteps(steps));
             return steps;
         }
    }
}