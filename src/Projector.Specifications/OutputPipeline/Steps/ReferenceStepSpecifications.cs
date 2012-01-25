using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;
using Projector.Specifications.Model;

namespace Projector.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(ReferenceStep))]
    public class When_building_references
    {
        protected static ReferenceStep referenceStep;
        protected static CodeDirectory codeDirectory;
        protected static Solution solution;

        Establish context = () =>
        {
            solution = new Solution();
            solution.AddProject(new Project { AssemblyName = "ProjectA", Path = "c:\\ProjectA", GeneratedProjectFilePath = "c:\\ProjectA\\Proj.csproj" });
            solution.AddProject(new Project { AssemblyName = "ProjectB", Path = "c:\\ProjectB" });

            codeDirectory = TestEntityFactory.CreateCodeDirectory("Test");

            mock = new AutoMoq<ReferenceStep>();
            referenceStep = mock.Object;
        };

        Because of = () => referenceStep.Execute(solution, codeDirectory);
        protected static AutoMoq<ReferenceStep> mock;
    }

    [Subject(typeof(ReferenceStep))]
    public class when_has_multiple_references : When_building_references
    {
        Establish context = () =>
        {
            codeDirectory.AddProject("ProjectA", "External");
            codeDirectory.AddProject("ProjectB", "ProjectA");

            mock.GetMock<IRelativePathGenerator>().Setup(x => x.GeneratePath("c:\\ProjectB", "c:\\ProjectA\\Proj.csproj")).Returns("relative path to project");
        };

        It should_add_assembly_reference_to_external_dependencies = ()
            => solution.GetProject("ProjectA").AssemblyReferences.Select(x => x.Name).ShouldContain("External");

        It should_map_the_hint_path_for_assembly_references = () => solution.GetProject("ProjectA").AssemblyReferences.First().HintPath.ShouldEqual("External\\path");

        It should_add_project_references_when_assembly_matches_a_project_in_the_solution = () => 
            solution.GetProject("ProjectB").ProjectReferences.Select(x => x.Project.AssemblyName).ShouldContain("ProjectA");

        It should_set_the_project_reference_path_relative_to_the_current_project = () => solution.GetProject("ProjectB").ProjectReferences.First().RelativePathToProject.ShouldEqual("relative path to project");
    }
}