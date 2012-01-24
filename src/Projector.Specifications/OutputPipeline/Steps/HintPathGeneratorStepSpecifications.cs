namespace Projector.Specifications.OutputPipeline.Steps
{
    using Auto.Moq;
    using Machine.Specifications;
    using Projector.Model;
    using Projector.Model.Output;
    using Projector.OutputPipeline;
    using Projector.OutputPipeline.Steps;
    using Projector.Specifications.IO;
    using Projector.Specifications.Model;

    [Subject(typeof(HintPathGeneratorStep))]
    public class When_missing_hint_paths
    {
        protected static AutoMoq<HintPathGeneratorStep> hintPathGeneratorStep;
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

            hintPathGeneratorStep = new AutoMoq<HintPathGeneratorStep>();
        };

        Because of = () => hintPathGeneratorStep.Object.Execute(solution, codeDirectory);
    }

    [Subject(typeof(HintPathGeneratorStep))]
    public class When_there_is_a_lib_folder : When_missing_hint_paths
    {
        Establish context = () =>
        {
            var directory = TestEntityFactory.CreateCodeDirectory("Lib");
            directory.AddFile(new TestFile("AReference.dll") { FilePath = @"c:\test\Lib\AReference.dll"});
            codeDirectory.AddCodeDirectory(directory);
        };

        private It should_update_hint_path_based_on_lib_folder = () => assemblyReference.HintPath.ShouldEqual(@"c:\test\Lib\AReference.dll");
    }

    [Subject(typeof(HintPathGeneratorStep))]
    public class When_no_lib_folder : When_missing_hint_paths
    {
        private It should_not_set_hint_paths = () => assemblyReference.HintPath.ShouldBeNull();
    }
}
