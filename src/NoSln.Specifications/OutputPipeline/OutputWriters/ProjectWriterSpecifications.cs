using System;
using Machine.Specifications;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputWriters;

namespace NoSln.Specifications.OutputPipeline.OutputWriters
{
    [Subject(typeof(ProjectWriter))]
    public class when_writing_the_project_information : when_writing_msbuild_elements<Project>
    {
        Establish context = () => 
        {
            part = new Project
            {
                AssemblyName = "assemblyName",
                OutputType = "outputType",
                Guid = Guid.NewGuid(),
                Namespace = "namespace"
            };
            writer = new ProjectWriter();
        };

        It should_write_a_property_group = () => element.Name.LocalName.ShouldEqual("PropertyGroup");

        It should_write_the_assembly_name = () => element.MsbuildElement("AssemblyName").Value.ShouldEqual("assemblyName");

        It should_write_the_guid = () => element.MsbuildElement("ProjectGuid").Value.ShouldEqual(part.Guid.ToString());

        It should_write_the_project_type = () => element.MsbuildElement("OutputType").Value.ShouldEqual("outputType");

        It should_write_the_namespace = () => element.MsbuildElement("RootNamespace").Value.ShouldEqual("namespace");

        It should_write_the_default_configuration = () => element.MsbuildElement("Configuration").Value.ShouldEqual("Debug");


        // DEFAULT VALUES - some of these should move to configuration
        It should_write_the_configuration_condition =
            () => element.MsbuildElement("Configuration").Attribute("Condition").Value.ShouldEqual(" '$(Configuration)' == '' ");

        It should_write_the_platform = () => element.MsbuildElement("Platform").Value.ShouldEqual("AnyCPU");

        It should_write_the_platform_condition =
            () => element.MsbuildElement("Platform").Attribute("Condition").Value.ShouldEqual(" '$(Platform)' == '' ");

        It should_write_the_product_version = () => element.MsbuildElement("ProductVersion").Value.ShouldEqual("8.0.30703");

        It should_write_the_target_framework_version = () => element.MsbuildElement("TargetFrameworkVersion").Value.ShouldEqual("v4.0");

        It should_write_the_file_alignment = () => element.MsbuildElement("FileAlignment").Value.ShouldEqual("512");
    }
}