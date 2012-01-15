using Machine.Specifications;

namespace NoSln.Specifications.Parser
{
    [Subject(typeof(ProjectParser))]
    public class when_parsing_a_project
    {
        static ProjectParser projectParser;
        Establish context = () => 
                                {
                                    projectParser = new ProjectParser();
                                };

        Because of = () => { };

        // when parsing a project it should set the project name
        It should_set_the_project_name = () => { };
    }

    internal class ProjectParser
    {
    }
}