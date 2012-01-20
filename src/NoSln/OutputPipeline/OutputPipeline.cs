using System.Collections.Generic;
using NoSln.Collections;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline.Steps;

namespace NoSln.OutputPipeline
{
    public interface IOutputPipeline
    {
        void Execute(CodeDirectory solutionCodeDirectory);
    }

    public class OutputPipeline : IOutputPipeline
    {
        readonly List<IOutputPipelineStep> steps = new List<IOutputPipelineStep>(); 
        public OutputPipeline()
        {
            steps.Add(new SolutionStructureStep(new RelativePathGenerator()));
            steps.Add(new ReferenceStep());
            steps.Add(new AddFilesPiplineStep(new FileInclusionHierarchyBuilder(new WildcardMatcher()), new RelativePathGenerator()));
            steps.Add(new MsBuildTemplateTranslatorStep());
        }

        public void Execute(CodeDirectory solutionCodeDirectory)
        {
            var solution = new Solution { SolutionPath = solutionCodeDirectory.Path };
            steps.Each(x => x.Execute(solution, solutionCodeDirectory));
        }
    }
}