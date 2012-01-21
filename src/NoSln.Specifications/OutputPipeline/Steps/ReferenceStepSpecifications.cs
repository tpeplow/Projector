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
    public class when_building_references
    {
        static ReferenceStep referenceStep;
        static CodeDirectory codeDirectory;
        static Solution solution;

        Establish context = () =>
        {
            solution = new Solution();
            solution.AddProject(new Project { AssemblyName = "ProjectA", Path = "c:\\ProjectA", GeneratedProjectFilePath = "c:\\ProjectA\\Proj.csproj"});
            solution.AddProject(new Project { AssemblyName = "ProjectB", Path = "c:\\ProjectB"});

            codeDirectory = TestEntityFactory.CreateCodeDirectory("Test");
            codeDirectory.AddProject("ProjectA", "External");
            codeDirectory.AddProject("ProjectB", "ProjectA");

            var mock = new AutoMoq<ReferenceStep>();
            mock.GetMock<IRelativePathGenerator>().Setup(x => x.GeneratePath("c:\\ProjectB", "c:\\ProjectA\\Proj.csproj")).Returns("relative path to project");
            referenceStep = mock.Object;
        };

        Because of = () => referenceStep.Execute(solution, codeDirectory);

        It should_add_assembly_reference_to_external_dependencies = ()
            => solution.GetProject("ProjectA").AssemblyReferences.Select(x => x.Name).ShouldContain("External");

        It should_map_the_hint_path_for_assembly_references = () => solution.GetProject("ProjectA").AssemblyReferences.First().HintPath.ShouldEqual("External\\path");

        It should_add_project_references_when_assembly_matches_a_project_in_the_solution = () => 
            solution.GetProject("ProjectB").ProjectReferences.Select(x => x.Project.AssemblyName).ShouldContain("ProjectA");

        It should_set_the_project_reference_path_relative_to_the_current_project = () => solution.GetProject("ProjectB").ProjectReferences.First().RelativePathToProject.ShouldEqual("relative path to project");
    }
}