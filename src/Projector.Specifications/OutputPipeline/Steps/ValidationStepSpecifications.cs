using System.Linq;
using Machine.Specifications;
using Projector.Model;
using Projector.Model.Output;
using Projector.Model.Validation;
using Projector.OutputPipeline.Steps;

namespace Projector.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(ValidationStep))]
    public class when_invalid
    {
        protected static CodeDirectory codeDirectory;
        protected static Solution solution;
        protected static SolutionValidationException exception;
        static ValidationStep validationStep;
        Establish context = () => 
        {
            solution = new Solution();
            validationStep = new ValidationStep();
        };

        Because of = () => exception = (SolutionValidationException)Catch.Exception(() => validationStep.Execute(solution, codeDirectory));
        
        public static void ShouldContainFailureReason(SolutionValidationFailureReasons reason)
        {
            exception.FailureReasons.Select(x => x.Reason).ShouldContain(reason);
        }
    }

    [Subject(typeof(ValidationStep))]
    public class when_there_are_no_projects : when_invalid
    {
        It should_fail_validation = () => exception.ShouldNotBeNull();
        It should_inform_the_user_there_are_no_projects_found = () => ShouldContainFailureReason(SolutionValidationFailureReasons.NoProjects);
    }

    [Subject(typeof(ValidationStep))]
    public class when_a_project_is_missing_a_template : when_invalid
    {
        Establish context = () => solution.AddProject(new Project { AssemblyName = "a project"});
        It should_fail_validation = () => exception.ShouldNotBeNull();
        It should_inform_the_user_there_must_be_a_template_for_each_project = () => ShouldContainFailureReason(SolutionValidationFailureReasons.MissingProjectTemplate);
    }
}