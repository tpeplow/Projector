using System.Xml.Linq;
using Projector.Model.Output;

namespace Projector.OutputPipeline.OutputWriters
{
    public class ProjectWriter : OutputXmlWriter<Project>
    {
        public override void Write(Project part, XDocument xml)
        {
            var propertyGroup 
                = CreateElement("PropertyGroup",
                    CreateElement("AssemblyName", new XText(part.AssemblyName)),
                    CreateElement("ProjectGuid", new XText(part.Guid.ToString())),
                    CreateElement("OutputType", new XText(part.OutputType)),
                    CreateElement("RootNamespace", new XText(part.Namespace)),
                    CreateElement("Configuration", 
                        new XAttribute("Condition", " '$(Configuration)' == '' "),
                        new XText("Debug")),
                    CreateElement("Platform",
                        new XAttribute("Condition", " '$(Platform)' == '' "),
                        new XText("AnyCPU")),
                    CreateElement("ProductVersion", new XText("8.0.30703")),
                    CreateElement("TargetFrameworkVersion", new XText("v4.0")),
                    CreateElement("FileAlignment", new XText("512")));
            xml.Root.Add(propertyGroup);
        }
    }
}