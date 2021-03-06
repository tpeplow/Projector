using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Projector.Model.Output;

namespace Projector.OutputPipeline.OutputWriters
{
    public class ProjectReferenceWriter : ItemGroupWriter<IEnumerable<ProjectReference>>
    {
        protected override IEnumerable<XElement> GetItems(IEnumerable<ProjectReference> part)
        {
            return part.Select(x => 
                CreateElement("ProjectReference", 
                            new XAttribute("Include", x.RelativePathToProject),
                            CreateElement("Project", new XText(x.Project.Guid.ToString("B"))),
                            CreateElement("Name", new XText(x.Project.AssemblyName))));
        }
    }
}