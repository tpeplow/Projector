using System.Linq;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
{
    public class FileTypePiplineStep : IOutputPipelineStep
    {
        readonly IFileTypeHierarchyBuilder fileTypeHierarchyBuilder;

        public FileTypePiplineStep(IFileTypeHierarchyBuilder fileTypeHierarchyBuilder)
        {
            this.fileTypeHierarchyBuilder = fileTypeHierarchyBuilder;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            var fileTypeHierarchy = fileTypeHierarchyBuilder.Generate(codeDirectory);

            foreach (var file in solution.Projects.SelectMany(x => x.Files))
            {
                var fileType = fileTypeHierarchy.GetFileType(file.RelativePath);
                if (fileType == null)
                {
                    file.BuildAction = BuildAction.Compile;
                }
                else
                {
                    file.BuildAction = fileType.BuildAction;
                    file.DependentUpon = fileType.DependentUpon;
                }
            }
        }
    }
}