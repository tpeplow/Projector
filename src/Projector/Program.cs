using System;
using Projector.Conventions;
using Projector.IO;
using Projector.Model.Validation;
using Projector.OutputPipeline;
using Projector.Parser;

namespace Projector
{
    static class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            var solutionProject = new SolutionProjector(
                new FileSystem(),
                DefaultConventions.CreateFileSystemConventions(),
                new SolutionBuilder(new ParserRegistry()), 
                new OutputPipeline.OutputPipeline(new OutputPipelineStepsBuilder()));

            try
            {
                solutionProject.ProjectFiles(path);
            }
            catch (SolutionValidationException validationException)
            {
                Console.WriteLine("Project structure failed validaiton with the following reasons:");
                foreach (var failureReason in validationException.FailureReasons)
                {
                    Console.WriteLine(failureReason.Message);
                }
            }
        }
    }
}
