using System.Xml.Linq;

namespace Projector.OutputPipeline.OutputWriters
{
    public interface IOutputXmlWriter
    {
        void Write(object part, XDocument xml);
    }

    public interface IOutputXmlWriter<in TPartType> : IOutputXmlWriter
    {
        void Write(TPartType part, XDocument xml);
    }

    public abstract class OutputXmlWriter<TPartyType> : IOutputXmlWriter<TPartyType>
    {
        public abstract void Write(TPartyType part, XDocument xml);

        protected XElement CreateElement(string localName, params object[] content)
        {
            return new XElement(Msbuild.DefaultNamespace + localName, content);
        }

        void IOutputXmlWriter.Write(object part, XDocument xml)
        {
            Write((TPartyType) part, xml);
        }
    }
}