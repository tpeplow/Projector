using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline
{
    public interface IOutputPipelineStep
    {
        void Execute(Solution solution, CodeDirectory codeDirectory);
    }
}