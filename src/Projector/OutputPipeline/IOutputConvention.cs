namespace Projector.OutputPipeline
{
    public interface IOutputConvention
    {
        void UpdateSteps(OutputPipelineStepCollection steps);
    }
}