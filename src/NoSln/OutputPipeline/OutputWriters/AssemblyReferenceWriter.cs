using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline.OutputWriters
{
    public class AssemblyReferenceWriter : ItemGroupWriter<IEnumerable<AssemblyReference>>
    {
        protected override IEnumerable<XElement> GetItems(IEnumerable<AssemblyReference> part)
        {
            return part.Select(x => CreateElement("Reference", GetChildren(x)));
        }

        IEnumerable<object> GetChildren(AssemblyReference assemblyReference)
        {
            yield return new XAttribute("Include", assemblyReference.Name);
            if (!string.IsNullOrEmpty(assemblyReference.HintPath))
                yield return CreateElement("HintPath", new XText(assemblyReference.HintPath));
        }
    }
}