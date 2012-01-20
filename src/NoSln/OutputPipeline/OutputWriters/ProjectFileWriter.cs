using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline.OutputWriters
{
    public class ProjectFileWriter : ItemGroupWriter<IEnumerable<ProjectFile>>
    {
        protected override IEnumerable<XElement> GetItems(IEnumerable<ProjectFile> part)
        {
            return part.Select(x => CreateElement("Compile", new XAttribute("Include", x.RelativePath)));
        }
    }
}