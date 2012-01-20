using NoSln.IO;
using NoSln.Parser;

namespace NoSln
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            var solutionProject = new SolutionProjector(new SolutionBuilder(new FileSystem(), new ParserRegistry()), new OutputPipeline.OutputPipeline());

            solutionProject.ProjectFiles(path);
        }
    }
}
