using System;
using Auto.Moq;
using Machine.Specifications;
using Projector.Model;
using Projector.Model.Validation;
using Projector.Parser;

namespace Projector.Specifications.Parser
{
    [Subject(typeof(ProjectParser))]
    public class when_parsing_a_project
    {
        static ProjectParser projectParser;
        static ProjectInfo projectInfo;
        Establish context = () =>
                                {
                                    projectParser = new AutoMoq<ProjectParser>().Object;
                                };

        Because of = () => projectInfo = projectParser.Parse(
            "Name: Project" + Environment.NewLine
            + "OutputType: Exe" +  Environment.NewLine
            + "namespace: namespace.subnamespace" + Environment.NewLine
            + "ProjectGuid: {5F221CC0-0187-4FE4-AB18-5E310CC5F106}" + Environment.NewLine
            + "AssemblyName: Different.AssemblyName" + Environment.NewLine
            + "Extension: .ext" + Environment.NewLine
            + "ProjectTypeGuid: {8800F4C7-45B2-4A79-90E6-C6EF360FB9E3}");

        It should_set_the_project_name = () => projectInfo.Name.ShouldEqual("Project");
        
        It should_set_the_output_type = () => projectInfo.OutputType.ShouldEqual("Exe");

        It should_should_set_the_namespace = () => projectInfo.Namespace.ShouldEqual("namespace.subnamespace");

        It should_set_the_project_guid = () => projectInfo.Guid.ShouldEqual(Guid.Parse("5F221CC0-0187-4FE4-AB18-5E310CC5F106"));

        It should_set_the_assembly_name = () => projectInfo.AssemblyName.ShouldEqual("Different.AssemblyName");

        It should_set_the_extension = () => projectInfo.Extension.ShouldEqual(".ext");

        It should_set_the_project_type_guid = () => projectInfo.ProjectTypeGuid.ShouldEqual(Guid.Parse("8800F4C7-45B2-4A79-90E6-C6EF360FB9E3"));
    }

    [Subject(typeof(ProjectParser))]
    public class when_parsing_a_minimal_file
    {
        static ProjectInfo projectInfo;
        static AutoMoq<ProjectParser> projectParser;
        static Guid expectedGuid;

        Establish context = () =>
                                {
                                    expectedGuid = Guid.NewGuid();
                                    projectParser = new AutoMoq<ProjectParser>();
                                    projectParser.GetMock<IGuidGenerator>().Setup(x => x.Generate()).Returns(expectedGuid);
                                };

        Because of = () => projectInfo = projectParser.Object.Parse(
            "Name: Project" + Environment.NewLine
            + "namespace: namespace.subnamespace" + Environment.NewLine );

        It should_default_the_output_type_to_library = () => projectInfo.OutputType.ShouldEqual("Library");

        It should_generate_an_new_guid_for_the_project_guid = () => projectInfo.Guid.ShouldEqual(expectedGuid);

        It should_default_the_assembly_name_to_the_name_of_the_project = () => projectInfo.AssemblyName.ShouldEqual("Project");

        It should_defualt_the_extension_to_csproj = () => projectInfo.Extension.ShouldEqual(".csproj");

        It should_default_the_project_type_guid_standard_dot_net_project = () => projectInfo.ProjectTypeGuid.ShouldEqual(Guid.Parse("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"));
    }

    [Subject(typeof(ProjectParser))]
    public class when_parsing_an_invalid_project_file
    {
        static AutoMoq<ProjectParser> projectParser;
        static SolutionValidationException exception;

        Establish context = () => 
                                {
                                    projectParser = new AutoMoq<ProjectParser>();
                                };

        Because of = () => exception = (SolutionValidationException)Catch.Exception(() => projectParser.Object.Parse(""));

        It should_raise_a_validation_error_exception = () => exception.ShouldNotBeNull();
    }
}