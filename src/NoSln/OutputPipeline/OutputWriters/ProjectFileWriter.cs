using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Projector.Model.Output;

namespace Projector.OutputPipeline.OutputWriters
{
    public class ProjectFileWriter : ItemGroupWriter<IEnumerable<ProjectFile>>
    {
        protected override IEnumerable<XElement> GetItems(IEnumerable<ProjectFile> part)
        {
            return part.Select(x => CreateElement("Compile", new XAttribute("Include", x.RelativePath)));
        }
    }
}