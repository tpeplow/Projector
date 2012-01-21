using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Projector.Model.Output;
using Projector.OutputPipeline.OutputWriters;

namespace Projector.Specifications.OutputPipeline.OutputWriters
{
    [Subject(typeof(AssemblyReferenceWriter))]
    public class when_writing_an_assembly_reference : when_writing_item_group<IEnumerable<AssemblyReference>>
    {
        Establish context = () => 
        {
            part = new [] { new AssemblyReference
            {
                Name = "SomeReference"
            }};

            writer = new AssemblyReferenceWriter();
        };
        
        It should_write_the_assembly_element = () => element.MsbuildElement("Reference").ShouldNotBeNull();

        It should_write_the_include_attribute = () => element.MsbuildElement("Reference").Attribute("Include").Value.ShouldEqual("SomeReference");
    }

    [Subject(typeof(AssemblyReferenceWriter))]
    public class when_writing_an_assembly_reference_with_hintpath : when_writing_an_assembly_reference
    {
        Establish context = () =>
        {
            part.First().HintPath = "..\\..\\someFolder.dll";
        };

        It should_write_hint_path = () => element.MsbuildElement("Reference").MsbuildElement("HintPath").Value.ShouldEqual("..\\..\\someFolder.dll");
    }
}