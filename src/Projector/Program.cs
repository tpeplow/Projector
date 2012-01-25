using Projector.IO;
using Projector.OutputPipeline;
using Projector.Parser;

namespace Projector
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            var solutionProject = new SolutionProjector(
                new SolutionBuilder(new FileSystem(), new ParserRegistry()), 
                new OutputPipeline.OutputPipeline(new OutputPipelineStepsBuilder()));

            solutionProject.ProjectFiles(path);
        }
    }
}
