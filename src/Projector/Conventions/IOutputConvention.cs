using Projector.OutputPipeline;

namespace Projector.Conventions
{
    public interface IOutputConvention
    {
        void UpdateSteps(OutputPipelineStepCollection steps);
    }
}