using System.Text;

namespace NoSln.OutputPipeline.OutputGenerator
{
    public interface IOutputWriter
    {
        void Generate(object part, StringBuilder stringBuilder);
    }

    public interface IOutputWriter<in TPartType> : IOutputWriter
    {
        void Generate(TPartType partType, StringBuilder stringBuilder);
    }

    public abstract class OutputWriter<TPartyType> : IOutputWriter<TPartyType>
    {
        public abstract void Generate(TPartyType partType, StringBuilder stringBuilder);

        void IOutputWriter.Generate(object part, StringBuilder stringBuilder)
        {
            Generate((TPartyType) part, stringBuilder);
        }
    }
}