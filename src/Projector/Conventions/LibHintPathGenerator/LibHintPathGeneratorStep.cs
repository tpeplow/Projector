using System;
using System.Linq;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;

namespace Projector.Conventions.LibHintPathGenerator
{
    public class LibHintPathGeneratorStep : IOutputPipelineStep, IOutputConvention
    {
        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            var libDirectory = codeDirectory.Directories.FirstOrDefault(x => x.Name.Equals("LIB", StringComparison.InvariantCultureIgnoreCase));

            if (libDirectory == null)
                return;

            var references = solution.Projects.SelectMany(x => x.AssemblyReferences.Where(y => string.IsNullOrEmpty(y.HintPath)));

            foreach (var assemblyReference in references)
            {
                assemblyReference.HintPath = FindFile(libDirectory, assemblyReference.Name);
            }
        }

        private static string FindFile(CodeDirectory directory, string referenceName)
        {
            var file = directory.Files.FirstOrDefault(x => x.FileName.StartsWith(referenceName, StringComparison.InvariantCultureIgnoreCase));

            if (file != null)
                return file.FilePath;

            foreach (var subdir in directory.Directories)
            {
                var path = FindFile(subdir, referenceName);

                if (!string.IsNullOrEmpty(path))
                    return path;
            }

            return string.Empty;
        }

        public void UpdateSteps(OutputPipelineStepCollection steps)
        {
            steps.InsertAfter<ReferenceStep>(this);
        }
    }
}