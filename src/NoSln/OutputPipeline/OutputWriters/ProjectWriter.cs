using System.Xml.Linq;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline.OutputWriters
{
    public class ProjectWriter : OutputXmlWriter<Project>
    {
        public override void Write(Project part, XDocument xml)
        {
            var propertyGroup 
                = new XElement("PropertyGroup",
                    new XElement("AssemblyName", new XText(part.AssemblyName)),
                    new XElement("ProjectGuid", new XText(part.Guid.ToString())),
                    new XElement("OutputType", new XText(part.OutputType)),
                    new XElement("RootNamespace", new XText(part.Namespace)),
                    new XElement("Configuration", 
                        new XAttribute("Condition", " '$(Configuration)' == '' "),
                        new XText("Debug")),
                    new XElement("Platform",
                        new XAttribute("Condition", " '$(Platform)' == '' "),
                        new XText("AnyCPU")),
                    new XElement("ProductVersion", new XText("8.0.30703")),
                    new XElement("TargetFrameworkVersion", new XText("v4.0")),
                    new XElement("FileAlignment", new XText("512")));
            xml.Root.Add(propertyGroup);
        }
    }
}