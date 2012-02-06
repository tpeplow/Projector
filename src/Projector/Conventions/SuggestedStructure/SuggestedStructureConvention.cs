using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;

namespace Projector.Conventions.SuggestedStructure
{
    public class SuggestedStructureConvention : IOutputConvention
    {
        public void UpdateSteps(OutputPipelineStepCollection steps)
        {
            var projectTypeNamingConvention = new ProjectTypeNamingConvention();
            steps.InsertBefore<SolutionStructureStep>(new ProjectInfoByConventionStep(projectTypeNamingConvention));
            steps.InsertAfter<MsBuildTemplateTranslatorStep>(new ProjectTemplateSelectorStep(projectTypeNamingConvention));
        }
    }
}