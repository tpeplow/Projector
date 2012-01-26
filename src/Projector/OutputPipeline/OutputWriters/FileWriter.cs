using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Projector.Model.Output;

namespace Projector.OutputPipeline.OutputWriters
{
    public class FileWriter : ItemGroupWriter<IEnumerable<ProjectFile>>
    {
        protected override IEnumerable<XElement> GetItems(IEnumerable<ProjectFile> part)
        {
            return part.Select(CreateFileElement);
        }

        XElement CreateFileElement(ProjectFile file)
        {
            var element = CreateElement(file.BuildAction.ToString(), new XAttribute("Include", file.RelativePath));

            if (!string.IsNullOrEmpty(file.DependentUpon))
            {
                element.Add(CreateElement("DependentUpon", new XText(file.DependentUpon)));
            }

            return element;
        }
    }
}