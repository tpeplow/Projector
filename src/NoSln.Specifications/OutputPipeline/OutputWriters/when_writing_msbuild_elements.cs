using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;
using Projector.OutputPipeline.OutputWriters;

namespace Projector.Specifications.OutputPipeline.OutputWriters
{
    [Subject(typeof(IOutputXmlWriter))]
    public abstract class when_writing_msbuild_elements<TPart>
    {
        protected static XDocument document;
        protected static TPart part;
        protected static XElement element;
        protected static IOutputXmlWriter<TPart> writer;

        Establish context = () => 
                                {
                                    document = new XDocument();
                                    document.Add(new XElement("Project"));
                                };

        Because of = () =>
                         {
                             writer.Write(part, document);
                             element = document.Root.Elements().First();
                         };
    }

    [Subject(typeof(IOutputXmlWriter))]
    public abstract class when_writing_item_group<TPart> : when_writing_msbuild_elements<TPart>
    {
        It should_write_item_group_element = () => element.Name.ShouldEqual("ItemGroup");
    }
}