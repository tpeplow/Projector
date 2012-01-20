using System.IO;
using System.Text;
using System.Xml.Linq;
using NoSln.IO;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputWriters;

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