using System.IO;
using System.Text;
using NoSln.IO;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputGenerator;

namespace NoSln.OutputPipeline.Steps
{
    public class MsBuildFileGenerationStep : IOutputPipelineStep
    {
        readonly IOutputWriterResolver outputWriterResolver;
        readonly IFileSystem fileSystem;

        public MsBuildFileGenerationStep(IOutputWriterResolver outputWriterResolver, IFileSystem fileSystem)
        {
            this.outputWriterResolver = outputWriterResolver;
            this.fileSystem = fileSystem;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            foreach (var project in solution.Projects)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(project.ProjectTemplate.Header);
                WriteOutputForPart(project, stringBuilder);
                WriteOutputForPart(project.AssemblyReferences, stringBuilder);
                WriteOutputForPart(project.ProjectReferences, stringBuilder);
                WriteOutputForPart(project.Files, stringBuilder);
                stringBuilder.Append(project.ProjectTemplate.Footer);

                fileSystem.WriteFile(Path.Combine(project.Path, project.Name + project.Extension), stringBuilder.ToString());
            }
        }

        void WriteOutputForPart<TPart>(TPart part, StringBuilder stringBuilder)
        {
            outputWriterResolver.Resolve<TPart>().Write(part, stringBuilder);
        }
    }
}