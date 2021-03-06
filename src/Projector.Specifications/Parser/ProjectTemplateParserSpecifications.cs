﻿using Machine.Specifications;
using Projector.Model;
using Projector.Parser;
using Projector.Specifications.Model;

namespace Projector.Specifications.Parser
{
    [Subject(typeof(ProjectTemplateParser))]
    public class when_parsing_a_template
    {
        static ProjectTemplateParser projectTemplateParser;
        static CodeDirectory codeDirectory;

        Establish context = () =>
                                {
                                    codeDirectory = TestEntityFactory.CreateCodeDirectory("test");
                                    projectTemplateParser = new ProjectTemplateParser();
                                };

        Because of = () => projectTemplateParser.Parse("file contents", codeDirectory);

        It should_add_the_template_to_the_code_directory = () => codeDirectory.ProjectTemplate.ShouldNotBeNull();

        It should_set_the_project_template_contents = () => codeDirectory.ProjectTemplate.ShouldEqual("file contents");
    }
}