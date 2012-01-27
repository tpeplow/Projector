using System.Collections.Generic;
using Projector.Collections;
using Projector.Conventions;
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
             DefaultConventions.CreateOutputConventions().Each(x => x.UpdateSteps(steps));
             return steps;
         }
    }
}