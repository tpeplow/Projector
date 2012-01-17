using NoSln.Model;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline
{
    public interface IOutputPipelineStep
    {
        void Execute(Solution solution, CodeDirectory codeDirectory);
    }
}