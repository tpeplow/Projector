﻿using System.Collections.Generic;
using Projector.Collections;
using Projector.IO;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline.OutputWriters;
using Projector.OutputPipeline.Steps;

namespace Projector.OutputPipeline
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
            steps.Add(new ReferenceStep(new RelativePathGenerator()));
            steps.Add(new AddFilesPiplineStep(new FileInclusionHierarchyBuilder(new WildcardMatcher()), new RelativePathGenerator()));
            steps.Add(new MsBuildTemplateTranslatorStep());
            steps.Add(new MsBuildFileGenerationStep(new OutputWriterResolver(), new FileSystem()));
        }

        public void Execute(CodeDirectory solutionCodeDirectory)
        {
            var solution = new Solution { SolutionPath = solutionCodeDirectory.Path };
            steps.Each(x => x.Execute(solution, solutionCodeDirectory));
        }
    }
}