using Machine.Specifications;
using NoSln.OutputPipeline;

namespace NoSln.Specifications.OutputPipeline
{
    [Subject(typeof(WildcardMatcher))]
    public class when_matching_a_wildcard_with_a_filepath
    {
        static WildcardMatcher wildcardMatcher;
        Establish context = () => 
                                {
                                    wildcardMatcher = new WildcardMatcher();
                                };

        It should_match_full_path = () => wildcardMatcher.IsMatch("c:\\afile.txt", "c:\\afile.txt").ShouldBeTrue();
        
        It should_match_if_path_ends_with_wildcard = () => wildcardMatcher.IsMatch("c:\\afile.txt", "afile.txt").ShouldBeTrue();

        It should_match_all_sub_folders_if_wildcard_has_trailing_slash = () => wildcardMatcher.IsMatch("c:\\somefolder\\somefile.txt", "somefolder\\").ShouldBeTrue();

        It should_match_if_any_file_matches_wildcard_that_uses_an_asterix = () => wildcardMatcher.IsMatch("c:\\somefolder\\dave.dll", "*.dll").ShouldBeTrue();

        It should_match_if_a_question_mark_is_used_to_denote_any_character = () => wildcardMatcher.IsMatch("c:\\afile.txt", "a?ile.txt").ShouldBeTrue();
    }
}