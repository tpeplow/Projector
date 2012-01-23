using System;
using System.Linq;
using Machine.Specifications;
using Projector.Model;
using Projector.Parser;
using Projector.Specifications.Model;

namespace Projector.Specifications.Parser
{
    [Subject(typeof(FileTypeParser))]
    public class when_parsing_file_types
    {
        static CodeDirectory codeDirectory;
        static FileTypeParser fileTypeParser;

        Establish context = () =>
        {
            codeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            fileTypeParser = new FileTypeParser();
        };

        Because of = () => fileTypeParser.Parse(
            "*.cs: BuildAction = Compile;" +  Environment.NewLine
            + "*.html: BuildAction = Content;", 
            codeDirectory);

        It should_update_code_directory_file_types = () => codeDirectory.FileTypes.ShouldNotBeNull();
        It should_set_build_action_to_compile = () => codeDirectory.FileTypes.First().BuildAction.ShouldEqual(BuildAction.Compile);
        It should_set_build_action_to_content = () => codeDirectory.FileTypes.Last().BuildAction.ShouldEqual(BuildAction.Content);
        It should_set_the_file_name_wildcard = () => codeDirectory.FileTypes.Select(x => x.FileNameWildcard).ShouldContain("*.cs", "*.html");
    }
}