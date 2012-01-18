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
            steps.Add(new SolutionStructureStep());
            steps.Add(new ReferenceStep());
            steps.Add(new AddFilesPiplineStep(new FileInclusionHierarchyBuilder(new WildcardMatcher()), new RelativePathGenerator()));
        }

        public void Execute(CodeDirectory solutionCodeDirectory)
        {
            var solution = new Solution();
            steps.Each(x => x.Execute(solution, solutionCodeDirectory));
        }
    }
}