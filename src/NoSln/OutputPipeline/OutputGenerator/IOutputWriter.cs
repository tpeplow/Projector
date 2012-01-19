using System.Text;

namespace NoSln.OutputPipeline.OutputGenerator
{
    public interface IOutputWriter
    {
        void Write(object part, StringBuilder stringBuilder);
    }

    public interface IOutputWriter<in TPartType> : IOutputWriter
    {
        void Write(TPartType partType, StringBuilder stringBuilder);
    }

    public abstract class OutputWriter<TPartyType> : IOutputWriter<TPartyType>
    {
        public abstract void Write(TPartyType partType, StringBuilder stringBuilder);

        void IOutputWriter.Write(object part, StringBuilder stringBuilder)
        {
            Write((TPartyType) part, stringBuilder);
        }
    }
}