using System.Collections;
using System.Collections.Generic;

namespace Projector.OutputPipeline
{
    public class OutputPipelineStepCollection : IEnumerable<IOutputPipelineStep>
    {
        readonly List<IOutputPipelineStep> steps = new List<IOutputPipelineStep>(); 
        
        public IEnumerator<IOutputPipelineStep> GetEnumerator()
        {
            return steps.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IOutputPipelineStep step)
        {
            steps.Add(step);
        }

        public void InsertAfter<TStep>(IOutputPipelineStep step)
        {
            var index = steps.FindIndex(s => s.GetType() == typeof (TStep));
            steps.Insert(index + 1, step);
        }

        public void InsertBefore<TStep>(IOutputPipelineStep step)
        {
            var index = steps.FindIndex(s => s.GetType() == typeof(TStep));
            steps.Insert(index, step);
        }
    }
}