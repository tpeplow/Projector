using NoSln.Model;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline
{
    public interface IOutputPipelineContributor
    {
        void Execute(Solution solution, CodeDirectory codeDirectory);
    }
}