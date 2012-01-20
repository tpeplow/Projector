using System.Collections.Generic;
using System.Xml.Linq;

namespace NoSln.OutputPipeline.OutputWriters
{
    public abstract class ItemGroupWriter<TPart> : OutputXmlWriter<TPart>
    {
        public override void Write(TPart part, XDocument xml)
        {
            xml.Root.Add(new XElement("ItemGroup", GetItems(part)));
        }

        protected abstract IEnumerable<XElement> GetItems(TPart part);
    }
}