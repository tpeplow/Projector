using Projector.IO;
using Projector.OutputPipeline.OutputWriters;

namespace Projector.OutputPipeline.Steps
{
    public static class DefaultPipelineSteps
    {
        public static OutputPipelineStepCollection Create()
        {
            var steps = new OutputPipelineStepCollection
            {
                new SolutionStructureStep(),
                new ReferenceStep(new RelativePathGenerator()),
                new RelativeReferencePathStep(new RelativePathGenerator()),
                new AddFilesPiplineStep(new FileInclusionHierarchyBuilder(new WildcardMatcher()), new RelativePathGenerator()),
                new FileTypePiplineStep(new FileTypeHierarchyBuilder(new WildcardMatcher())),
                new MsBuildTemplateTranslatorStep(), 
                new ValidationStep(),
                new MsBuildFileGenerationStep(new OutputWriterResolver(), new FileSystem()),
                new SolutionGenerationStep(new FileSystem(), new RelativePathGenerator())
            };

            return steps;
        }
    }
}