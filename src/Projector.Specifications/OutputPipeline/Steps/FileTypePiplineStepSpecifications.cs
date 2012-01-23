using Auto.Moq;
using Machine.Specifications;
using Moq;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;
using It = Machine.Specifications.It;
using Arg = Moq.It;

namespace Projector.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(FileTypePiplineStep))]
    public class when_there_are_no_file_types_specified : when_adding_file_types
    {
        It should_default_build_action_to_compile = () => file.BuildAction.ShouldEqual(BuildAction.Compile);
    }

    [Subject(typeof(FileTypePiplineStep))]
    public class when_there_are_matching_file_types_specified : when_adding_file_types
    {
        Establish with_file_types = () => fileType = new FileType { BuildAction = BuildAction.Content, DependentUpon = "blah" };

        It should_set_the_build_action = () => file.BuildAction.ShouldEqual(BuildAction.Content);
        It should_set_dependent_upon = () => file.DependentUpon.ShouldEqual("blah");
    }

    [Subject(typeof(FileTypePiplineStep))]
    public class when_adding_file_types
    {
        static CodeDirectory codeDirectory;
        static Solution solution;
        static AutoMoq<FileTypePiplineStep> fileTypePiplineStep;
        protected static FileType fileType;
        protected static ProjectFile file;

        Establish context = () =>
                                {
                                    fileTypePiplineStep = new AutoMoq<FileTypePiplineStep>();
                                    SetupFileType();
                                    SetupSolutionWithOneProjectFile();
                                };

        static void SetupSolutionWithOneProjectFile()
        {
            solution = new Solution();
            var project = new Project {AssemblyName = "name"};
            file = new ProjectFile { RelativePath = "relativePath"};
            project.AddFile(file);
            solution.AddProject(project);
        }

        static void SetupFileType()
        {
            var fileTypeHierarchy = new Mock<IFileTypeHierarchy>();
            fileTypeHierarchy.Setup(x => x.GetFileType("relativePath")).Returns(() => fileType);
            fileTypePiplineStep.GetMock<IFileTypeHierarchyBuilder>()
                .Setup(x => x.Generate(codeDirectory))
                .Returns(() => fileTypeHierarchy.Object);
        }

        Because of = () => fileTypePiplineStep.Object.Execute(solution, codeDirectory);
    }
}