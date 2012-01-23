using System.Linq;
using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
{
    public class AddFilesPiplineStep : IOutputPipelineStep
    {
        readonly IFileInclusionHierarchyBuilder fileInclusionHierarchyBuilder;
        readonly IRelativePathGenerator relativePathGenerator;

        public AddFilesPiplineStep(IFileInclusionHierarchyBuilder fileInclusionHierarchyBuilder, IRelativePathGenerator relativePathGenerator)
        {
            this.fileInclusionHierarchyBuilder = fileInclusionHierarchyBuilder;
            this.relativePathGenerator = relativePathGenerator;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            AddFiles(solution, codeDirectory);
        }

        void AddFiles(Solution solution, CodeDirectory currentDirectory, IFileInclusionHierarchy fileInclusionHierarchy = null, Project currentProject = null)
        {
            fileInclusionHierarchy = UpdateFileInclusionPolicy(currentDirectory, fileInclusionHierarchy);
            if (currentDirectory.Project != null)
            {
                currentProject = solution.GetProject(currentDirectory.Project.AssemblyName);
            }

            AddIncludedFiles(currentDirectory, fileInclusionHierarchy, currentProject);

            currentDirectory.Directories.Each(x => AddFiles(solution, x, fileInclusionHierarchy, currentProject));
        }

        void AddIncludedFiles(CodeDirectory currentDirectory, IFileInclusionHierarchy fileInclusionHierarchy, Project currentProject)
        {
            if (currentProject == null) return;
            foreach (var file in currentDirectory.Files.Where(x => fileInclusionHierarchy.ShouldInclude(x.FilePath)))
            {
                currentProject.AddFile(new ProjectFile
                {
                    RelativePath = relativePathGenerator.GeneratePath(currentProject.Path, file.FilePath)
                });
            }
        }

        IFileInclusionHierarchy UpdateFileInclusionPolicy(CodeDirectory currentDirectory, IFileInclusionHierarchy fileInclusionHierarchy)
        {
            if (fileInclusionHierarchy == null)
            {
                fileInclusionHierarchy =
                    fileInclusionHierarchyBuilder.Create(currentDirectory.FileInclusionPolicy ?? new FileInclusionPolicy());
            }
            else if (currentDirectory.FileInclusionPolicy != null)
            {
                fileInclusionHierarchy = fileInclusionHierarchyBuilder.Combine(fileInclusionHierarchy,
                                                                               currentDirectory.FileInclusionPolicy);
            }
            return fileInclusionHierarchy;
        }
    }
}