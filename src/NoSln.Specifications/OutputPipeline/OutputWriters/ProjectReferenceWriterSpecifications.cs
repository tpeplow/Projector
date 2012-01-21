using System;
using System.Collections.Generic;
using Machine.Specifications;
using Projector.Model.Output;
using Projector.OutputPipeline.OutputWriters;

namespace Projector.Specifications.OutputPipeline.OutputWriters
{
    [Subject(typeof(ProjectReferenceWriter))]
    public class when_writing_project_references : when_writing_item_group<IEnumerable<ProjectReference>>
    {
        static Guid projectGuid;

        Establish context = () =>
        {
            writer = new ProjectReferenceWriter();
            projectGuid = Guid.NewGuid();
            part = new[]
            {
                new ProjectReference
                {
                    Project = new Project
                    {
                        Guid = projectGuid,
                        AssemblyName = "AssemblyName"
                    },
                    RelativePathToProject = "..\\aproj.csproj"
                }
            };
        };

        It should_write_the_project_reference_element = () => element.MsbuildElement("ProjectReference").ShouldNotBeNull();

        It should_write_the_include_attribute_pointing_to_the_relative_path_of_the_project = () => element.MsbuildElement("ProjectReference").Attribute("Include").Value.ShouldEqual("..\\aproj.csproj");

        It should_write_the_project_element_which_is_the_guid_of_the_referenced_project = () => element.MsbuildElement("ProjectReference").MsbuildElement("Project").Value.ShouldEqual(projectGuid.ToString("B"));

        It should_write_the_name_of_the_project = () => element.MsbuildElement("ProjectReference").MsbuildElement("Name").Value.ShouldEqual("AssemblyName");
    }
}