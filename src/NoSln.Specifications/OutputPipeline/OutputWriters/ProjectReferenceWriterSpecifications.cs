using System;
using System.Collections.Generic;
using Machine.Specifications;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputWriters;

namespace NoSln.Specifications.OutputPipeline.OutputWriters
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

        It should_write_the_project_reference_element = () => element.Element("ProjectReference").ShouldNotBeNull();

        It should_write_the_include_attribute_pointing_to_the_relative_path_of_the_project = () => element.Element("ProjectReference").Attribute("Include").Value.ShouldEqual("..\\aproj.csproj");

        It should_write_the_project_element_which_is_the_guid_of_the_referenced_project = () => element.Element("ProjectReference").Element("Project").Value.ShouldEqual(projectGuid.ToString("B"));

        It should_write_the_name_of_the_project = () => element.Element("ProjectReference").Element("Name").Value.ShouldEqual("AssemblyName");
    }
}