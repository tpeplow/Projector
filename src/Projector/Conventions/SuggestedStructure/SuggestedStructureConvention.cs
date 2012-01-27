using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;

namespace Projector.Conventions.SuggestedStructure
{
    public class SuggestedStructureConvention : IOutputConvention
    {
        public void UpdateSteps(OutputPipelineStepCollection steps)
        {
            steps.InsertBefore<SolutionStructureStep>(new ProjectInfoByConventionStep());
        }
    }
}