using System.Collections.Generic;
using Machine.Specifications;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputWriters;

namespace NoSln.Specifications.OutputPipeline.OutputWriters
{
    [Subject(typeof(ProjectFileWriter))]
    public class when_writing_a_project_file : when_writing_item_group<IEnumerable<ProjectFile>>
    {
        Establish context = () => 
        {
            writer = new ProjectFileWriter();
            part = new[]
            {
                new ProjectFile
                {
                    RelativePath = "afile.cs"
                }
            };
        };
        
        It should_write_the_compile_element = () => element.Element("Compile").ShouldNotBeNull();

        It should_write_include_attribute = () => element.Element("Compile").Attribute("Include").Value.ShouldEqual("afile.cs");
    }
}