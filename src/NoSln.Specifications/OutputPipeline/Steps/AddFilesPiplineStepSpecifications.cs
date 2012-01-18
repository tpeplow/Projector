using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Moq;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline;
using NoSln.OutputPipeline.Steps;
using NoSln.Specifications.IO;
using NoSln.Specifications.Model;
using It = Machine.Specifications.It;

namespace NoSln.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(AddFilesPiplineStep))]
    public class when_adding_a_file
    {
        static Solution solution;
        static Project solutionProject;
        static CodeDirectory codeDirectory;
        static AutoMoq<AddFilesPiplineStep> addFilesPiplineStep;

        Establish context = () => 
        {
            solution = new Solution();
            solutionProject = new Project { AssemblyName = "A project" };
            solution.AddProject(solutionProject);

            codeDirectory = EntityFactory.CreateCodeDirectory("test");
            var rootInclusionPolicy = new FileInclusionPolicy();
            codeDirectory.FileInclusionPolicy = rootInclusionPolicy;
            codeDirectory.AddFile(new TestFile { FilePath = "not in a project" });

            var project = codeDirectory.AddProject("A project");
            project.AddFile(new TestFile { FilePath = "file in project"});
            project.AddFile(new TestFile { FilePath = "ignored file"});
            project.FileInclusionPolicy = new FileInclusionPolicy();
            var subDirectory = EntityFactory.CreateCodeDirectory("sub dir");
            subDirectory.AddFile(new TestFile { FilePath = "file in sub dir"});
            project.AddCodeDirectory(subDirectory);

            
            addFilesPiplineStep = new AutoMoq<AddFilesPiplineStep>();
            addFilesPiplineStep.GetMock<IRelativePathGenerator>()
                               .Setup(x => x.GeneratePath(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
                               .Returns<string, string>((relativeTo, fullPath) => fullPath);
            
            var fileInclusionHierarchy = new Mock<IFileInclusionHierarchy>();
            addFilesPiplineStep.GetMock<IFileInclusionHierarchyBuilder>()
                               .Setup(x => x.Create(rootInclusionPolicy))
                               .Returns(() => fileInclusionHierarchy.Object);


            var combinedInclusionHierarchy = new Mock<IFileInclusionHierarchy>();
            addFilesPiplineStep.GetMock<IFileInclusionHierarchyBuilder>()
                               .Setup(x => x.Combine(fileInclusionHierarchy.Object, project.FileInclusionPolicy))
                               .Returns(() => combinedInclusionHierarchy.Object);

            combinedInclusionHierarchy.Setup(x => x.ShouldInclude("ignored file")).Returns(false);
            combinedInclusionHierarchy.Setup(x => x.ShouldInclude("file in project")).Returns(true);
            combinedInclusionHierarchy.Setup(x => x.ShouldInclude("file in sub dir")).Returns(true);
        };

        Because of = () => addFilesPiplineStep.Object.Execute(solution, codeDirectory);

        It should_not_include_files_in_folders_which_do_not_have_a_parent_project
            = () => solutionProject.Files.Select(x => x.RelativePath).ShouldNotContain("not in a project");

        It should_add_files_not_ignored_which_are_part_of_a_project = () => solutionProject.Files.Select(x => x.RelativePath).ShouldContain("file in project");

        It should_add_files_in_sub_directory_of_a_project = () => solutionProject.Files.Select(x => x.RelativePath).ShouldContain("file in sub dir");

        It should_not_add_files_which_are_excluded = () => solutionProject.Files.Select(x => x.RelativePath).ShouldNotContain("ignored file");
    }
}