using System.Collections.Generic;
using System.Linq;
using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;
using Projector.Model.Validation;

namespace Projector.OutputPipeline.Steps
{
    public class ValidationStep : IOutputPipelineStep
    {
        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            var failures = new List<SolutionValidationFailureReason>();
            
            ThereMustBeAProject(solution, failures);
            EachProjectMustHaveATemplate(solution, failures);

            if (failures.Count > 0) throw new SolutionValidationException(failures);
        }

        void EachProjectMustHaveATemplate(Solution solution, List<SolutionValidationFailureReason> failures)
        {
            solution.Projects.Where(x => x.ProjectTemplate == null).Each(x => failures.Add(
                new SolutionValidationFailureReason(SolutionValidationFailureReasons.MissingProjectTemplate,
                                                    string.Format("The project {0} is missing a template", (object) x.Name))));
        }

        static void ThereMustBeAProject(Solution solution, List<SolutionValidationFailureReason> failures)
        {
            if (!solution.Projects.Any())
            {
                failures.Add(new SolutionValidationFailureReason(SolutionValidationFailureReasons.NoProjects, "No projects could be found.  Remember to add a Project.nosln file"));
            }
        }
    }
}