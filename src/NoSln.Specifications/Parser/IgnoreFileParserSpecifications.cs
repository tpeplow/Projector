using System;
using System.Linq;
using Machine.Specifications;
using Projector.Model;
using Projector.Parser;

namespace Projector.Specifications.Parser
{
    [Subject(typeof(IgnoreFileParser))]
    public class when_parsing_ignore_file
    {
        static IgnoreFileParser ignoreFileParser;
        static FileInclusionPolicy fileInclusionPolicy;
        Establish context = () => 
                                {
                                    ignoreFileParser = new IgnoreFileParser();
                                };

        Because of = () => fileInclusionPolicy = ignoreFileParser.Parse(
            "*.dll" + Environment.NewLine +
            "*.dll" + Environment.NewLine + 
            "^dave.dll" + Environment.NewLine
            + Environment.NewLine
            + "# ignored line");

        It should_include_each_line_as_an_ignored_file = () => fileInclusionPolicy.ExcludeFilters.ShouldContain("*.dll");

        It should_exclude_duplicates = () => fileInclusionPolicy.ExcludeFilters.Count(x => x == "*.dll").ShouldEqual(1);

        It should_contain_files_to_explicitly_include = () => fileInclusionPolicy.IncludeFilters.ShouldContain("dave.dll");

        It should_ignore_commented_out_and_blank_lines = () => fileInclusionPolicy.ExcludeFilters.Union(fileInclusionPolicy.IncludeFilters).Count().ShouldEqual(2);
    }
}