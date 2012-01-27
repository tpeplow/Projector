using Auto.Moq;
using Machine.Specifications;
using Projector.Conventions;
using Projector.Model;
using Projector.Model.Output;
using Projector.Specifications.IO;
using Projector.Specifications.Model;

namespace Projector.Specifications.Conventions
{
    [Subject(typeof(LibHintPathGeneratorStep))]
    public class when_missing_hint_paths
    {
        protected static AutoMoq<LibHintPathGeneratorStep> hintPathGeneratorStep;
        protected static CodeDirectory codeDirectory;
        static Solution solution;
        protected static AssemblyReference assemblyReference;

        Establish context = () =>
        {
            solution = new Solution { SolutionPath = @"c:\test" };
            var project = new Project { AssemblyName = "Assembly" };
            assemblyReference = new AssemblyReference { Name = "AReference" };

            project.AddReference(assemblyReference);
            solution.AddProject(project);

            codeDirectory = TestEntityFactory.CreateCodeDirectory("test");

            hintPathGeneratorStep = new AutoMoq<LibHintPathGeneratorStep>();
        };

        Because of = () => hintPathGeneratorStep.Object.Execute(solution, codeDirectory);
    }

    [Subject(typeof(LibHintPathGeneratorStep))]
    public class when_there_is_a_lib_folder : when_missing_hint_paths
    {
        Establish context = () =>
        {
            var directory = TestEntityFactory.CreateCodeDirectory("Lib");
            directory.AddFile(new TestFile("AReference.dll") { FilePath = @"c:\test\Lib\AReference.dll"});
            codeDirectory.AddCodeDirectory(directory);
        };

        private It should_update_hint_path_based_on_lib_folder = () => assemblyReference.HintPath.ShouldEqual(@"c:\test\Lib\AReference.dll");
    }

    [Subject(typeof(LibHintPathGeneratorStep))]
    public class when_no_lib_folder : when_missing_hint_paths
    {
        private It should_not_set_hint_paths = () => assemblyReference.HintPath.ShouldBeNull();
    }
}
