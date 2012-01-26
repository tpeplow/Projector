using System;
using System.Collections.Generic;
using Projector.Model.Output;

namespace Projector.OutputPipeline.OutputWriters
{
    public interface IOutputWriterResolver
    {
        IOutputXmlWriter Resolve<TPartType>();
    }

    public class OutputWriterResolver : IOutputWriterResolver
    {
        readonly Dictionary<Type, IOutputXmlWriter> writers = new Dictionary<Type, IOutputXmlWriter>
        {
            {typeof(Project), new ProjectWriter()},
            {typeof(IEnumerable<AssemblyReference>), new AssemblyReferenceWriter()},
            {typeof(IEnumerable<ProjectReference>), new ProjectReferenceWriter()},
            {typeof(IEnumerable<ProjectFile>), new FileWriter()},
        };

        public IOutputXmlWriter Resolve<TPartType>()
        {
            return writers[typeof (TPartType)];
        }
    }
}