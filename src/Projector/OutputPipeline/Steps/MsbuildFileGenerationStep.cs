using System.Xml.Linq;
using Projector.IO;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline.OutputWriters;

namespace Projector.OutputPipeline.Steps
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
                var document = new XDocument(project.ProjectTemplate.Xml);
                WriteOutputForPart(project, document);
                WriteOutputForPart(project.AssemblyReferences, document);
                WriteOutputForPart(project.ProjectReferences, document);
                WriteOutputForPart(project.Files, document);

                fileSystem.WriteFile(project.GeneratedProjectFilePath, document.ToString());
            }
        }

        void WriteOutputForPart<TPart>(TPart part, XDocument xmlXDocument)
        {
            outputWriterResolver.Resolve<TPart>().Write(part, xmlXDocument);
        }
    }
}