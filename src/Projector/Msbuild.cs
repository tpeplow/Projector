using System.Xml.Linq;

namespace Projector
{
    public static class Msbuild
    {
        public static readonly XNamespace DefaultNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public static XElement MsbuildElement(this XElement parent, string name)
        {
            return parent.Element(DefaultNamespace + name);
        }
    }
}