﻿using System;
using Auto.Moq;
using Machine.Specifications;

namespace NoSln.Specifications.Parser
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
            + "ProjectGuid: {5F221CC0-0187-4FE4-AB18-5E310CC5F106}" );

        It should_set_the_project_name = () => projectInfo.Name.ShouldEqual("Project");
        
        It should_set_the_output_type = () => projectInfo.OutputType.ShouldEqual("Exe");

        It should_should_set_the_namespace = () => projectInfo.Namespace.ShouldEqual("namespace.subnamespace");

        It should_set_the_project_guid = () => projectInfo.Guid.ShouldEqual(Guid.Parse("5F221CC0-0187-4FE4-AB18-5E310CC5F106"));
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

        It should_should_generate_an_new_guid_for_the_project_guid = () => projectInfo.Guid.ShouldEqual(expectedGuid);
    }
}