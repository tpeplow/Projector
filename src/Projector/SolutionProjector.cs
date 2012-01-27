using System.Collections.Generic;
using Projector.Collections;
using Projector.Conventions;
using Projector.IO;
using Projector.OutputPipeline;
using Projector.Parser;

namespace Projector
{
    public class SolutionProjector
    {
        readonly IFileSystem fileSystem;
        readonly IEnumerable<IModifyFileSystemConvention> modifyFileSystemConventions;
        readonly ISolutionBuilder solutionBuilder;
        readonly IOutputPipeline outputPipeline;

        public SolutionProjector(IFileSystem fileSystem, IEnumerable<IModifyFileSystemConvention> modifyFileSystemConventions, ISolutionBuilder solutionBuilder, IOutputPipeline outputPipeline)
        {
            this.fileSystem = fileSystem;
            this.modifyFileSystemConventions = modifyFileSystemConventions;
            this.solutionBuilder = solutionBuilder;
            this.outputPipeline = outputPipeline;
        }

        public void ProjectFiles(string path)
        {
            var directory = fileSystem.GetDirectory(path);
            modifyFileSystemConventions.Each(x => x.Update(directory));
            var solutionCodeDirectory = solutionBuilder.BuildFromDirectory(directory);
            outputPipeline.Execute(solutionCodeDirectory);
        }
    }
}