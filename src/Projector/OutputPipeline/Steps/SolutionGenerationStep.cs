using System;
using System.IO;
using System.Text;
using Projector.Collections;
using Projector.IO;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
{
    public class SolutionGenerationStep : IOutputPipelineStep
    {
        readonly IFileSystem fileSystem;
        readonly IRelativePathGenerator relativePathGenerator;

        public SolutionGenerationStep(IFileSystem fileSystem, IRelativePathGenerator relativePathGenerator)
        {
            this.fileSystem = fileSystem;
            this.relativePathGenerator = relativePathGenerator;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Microsoft Visual Studio Solution File, Format Version 11.00");
            solution.Projects.Each(
                x => stringBuilder.AppendFormat("Project(\"{0:B}\") = \"{1}\", \"{2}\", \"{3:B}\"{4}EndProject{4}",
                                                x.ProjectTypeGuid, 
                                                x.Name, 
                                                relativePathGenerator.GeneratePath(solution.SolutionPath, x.GeneratedProjectFilePath), 
                                                x.Guid, 
                                                Environment.NewLine));

            fileSystem.WriteFile(Path.Combine(solution.SolutionPath, "All.sln"), stringBuilder.ToString());
        }
    }
}