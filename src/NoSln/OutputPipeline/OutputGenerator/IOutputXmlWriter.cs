using System.Text;
using System.Xml.Linq;

namespace NoSln.OutputPipeline.OutputGenerator
{
    public interface IOutputXmlWriter
    {
        void Write(object part, XDocument xml);
    }

    public interface IOutputXmlWriter<in TPartType> : IOutputXmlWriter
    {
        void Write(TPartType partType, XDocument xml);
    }

    public abstract class OutputXmlWriter<TPartyType> : IOutputXmlWriter<TPartyType>
    {
        public abstract void Write(TPartyType partType, XDocument xml);

        void IOutputXmlWriter.Write(object part, XDocument xml)
        {
            Write((TPartyType) part, xml);
        }
    }
}