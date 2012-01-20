using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputWriters;

namespace NoSln.Specifications.OutputPipeline.OutputWriters
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
        
        It should_write_the_assembly_element = () => element.Element("Reference").ShouldNotBeNull();

        It should_write_the_include_attribute = () => element.Element("Reference").Attribute("Include").Value.ShouldEqual("SomeReference");
    }

    [Subject(typeof(AssemblyReferenceWriter))]
    public class when_writing_an_assembly_reference_with_hintpath : when_writing_an_assembly_reference
    {
        Establish context = () =>
        {
            part.First().HintPath = "..\\..\\someFolder.dll";
        };

        It should_write_hint_path = () => element.Element("Reference").Element("HintPath").Value.ShouldEqual("..\\..\\someFolder.dll");
    }
}