using System.Collections.Generic;
using Machine.Specifications;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline.OutputWriters;

namespace Projector.Specifications.OutputPipeline.OutputWriters
{
    [Subject(typeof(ProjectFileWriter))]
    public class when_writing_a_compile_project_file : when_writing_item_group<IEnumerable<ProjectFile>>
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
        
        It should_write_the_compile_element = () => element.MsbuildElement("Compile").ShouldNotBeNull();

        It should_write_include_attribute = () => element.MsbuildElement("Compile").Attribute("Include").Value.ShouldEqual("afile.cs");
    }

    [Subject(typeof(ProjectFileWriter))]
    public class when_writing_a_content_project_file : when_writing_item_group<IEnumerable<ProjectFile>>
    {
        Establish context = () =>
        {
            writer = new ProjectFileWriter();
            part = new[]
            {
                new ProjectFile
                {
                    RelativePath = "afile.cs",
                    BuildAction = BuildAction.Content
                }
            };
        };

        It should_write_the_compile_element = () => element.MsbuildElement("Content").ShouldNotBeNull();

        It should_write_include_attribute = () => element.MsbuildElement("Content").Attribute("Include").Value.ShouldEqual("afile.cs");
    }

    [Subject(typeof(ProjectFileWriter))]
    public class when_writing_a_project_file_that_has_dependent : when_writing_item_group<IEnumerable<ProjectFile>>
    {
        Establish context = () =>
        {
            writer = new ProjectFileWriter();
            part = new[]
            {
                new ProjectFile
                {
                    RelativePath = "afile.cs",
                    DependentUpon = "dave.cs"
                }
            };
        };

        It should_write_the_compile_element = () => element.MsbuildElement("Compile").ShouldNotBeNull();

        It should_write_include_attribute = () => element.MsbuildElement("Compile").Attribute("Include").Value.ShouldEqual("afile.cs");

        It should_write_dependnt_element = () => element.MsbuildElement("Compile").MsbuildElement("DependentUpon").Value.ShouldEqual("dave.cs");
    }
}