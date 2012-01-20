using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline.OutputWriters
{
    public class ProjectReferenceWriter : ItemGroupWriter<IEnumerable<ProjectReference>>
    {
        protected override IEnumerable<XElement> GetItems(IEnumerable<ProjectReference> part)
        {
            return part.Select(x => 
                new XElement("ProjectReference", 
                            new XAttribute("Include", x.Project.SolutionRelativePath),
                            new XElement("Project", new XText(x.Project.Guid.ToString("B"))),
                            new XElement("Name", new XText(x.Project.AssemblyName))));
        }
    }
}