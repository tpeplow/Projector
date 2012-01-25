using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;

namespace Projector.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(RelativeReferencePathStep))]
    public class when_fixing_reference_paths
    {
        protected static AutoMoq<RelativeReferencePathStep> relativeReferencePathStep;
        protected static Solution solution;
        static CodeDirectory codeDirectory;

        Establish context = () =>
        {
            solution = new Solution();
            relativeReferencePathStep = new AutoMoq<RelativeReferencePathStep>();
        };

        Because of = () => relativeReferencePathStep.Object.Execute(solution, codeDirectory);
    }

    [Subject(typeof(RelativeReferencePathStep))]
    public class when_reference_path_is_absolute : when_fixing_reference_paths
    {
        Establish context = () =>
        {
            var project = new Project { AssemblyName = "AProject", Path = "c:\\projectpath"};
            project.AddReference(new AssemblyReference {HintPath = "c:\\abs\\path"});
            solution.AddProject(project);
            relativeReferencePathStep.GetMock<IRelativePathGenerator>().Setup(x => x.GeneratePath("c:\\projectpath", "c:\\abs\\path")).Returns("relative path");
        };

        It should_be_made_relative_to_the_solution = () => solution.GetProject("AProject").AssemblyReferences.First().HintPath.ShouldEqual("relative path");
    }

    [Subject(typeof(RelativeReferencePathStep))]
    public class when_reference_path_is_relative : when_fixing_reference_paths
    {
        Establish context = () =>
        {
            var project = new Project { AssemblyName = "AProject", Path = "c:\\projectpath" };
            project.AddReference(new AssemblyReference { HintPath = "rel\\path" });
            solution.AddProject(project);
        };

        It should_leave_the_hint_path_alone = () => solution.GetProject("AProject").AssemblyReferences.First().HintPath.ShouldEqual("rel\\path");
    }
}