namespace Projector.OutputPipeline.Steps
{
    using System;
    using System.Linq;
    using Projector.Model;
    using Projector.Model.Output;

    public class HintPathGeneratorStep : IOutputPipelineStep
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

        private string FindFile(CodeDirectory directory, string referenceName)
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
    }
}