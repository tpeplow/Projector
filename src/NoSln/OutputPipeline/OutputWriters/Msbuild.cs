using System.Xml.Linq;

namespace Projector.OutputPipeline.OutputWriters
{
    public static class Msbuild
    {
        public static readonly XNamespace DefualtNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public static XElement MsbuildElement(this XElement parent, string name)
        {
            return parent.Element(DefualtNamespace + name);
        }
    }
}