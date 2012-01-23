using Machine.Specifications;
using Projector.Parser;

namespace Projector.Specifications.Parser
{
    [Subject(typeof(ParserRegistry))]
    public class when_selecting_a_parser_to_use_for_a_file
    {
        static ParserRegistry parserRegistry;
        Establish context = () =>
                                {
                                    parserRegistry = new ParserRegistry();
                                };

        It should_not_return_a_parser_for_unkonwn_files = () => parserRegistry.GetParserForFile("whatever").ShouldBeNull();

        It should_select_a_project_info_parser_for_proj_dot_nosln_files = () => parserRegistry.GetParserForFile("proj.nosln").ShouldBeOfType<ProjectParser>();

        It should_select_a_reference_parser_for_references_dot_nosln_files = () => parserRegistry.GetParserForFile("references.nosln").ShouldBeOfType<ReferenceParser>();

        It should_select_a_ignore_file_parser_for_ignore_dot_nosln_files = () => parserRegistry.GetParserForFile("ignore.nosln").ShouldBeOfType<IgnoreFileParser>();

        It should_select_the_project_template_parser_for_project_templates = () => parserRegistry.GetParserForFile("template.nosln").ShouldBeOfType<ProjectTemplateParser>();

        It should_select_the_file_type_parser_for_file_type_files = () => parserRegistry.GetParserForFile("filetypes.nosln").ShouldBeOfType<FileTypeParser>();
    }
}