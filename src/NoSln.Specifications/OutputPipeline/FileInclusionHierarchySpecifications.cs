using Auto.Moq;
using Machine.Specifications;
using NoSln.OutputPipeline;
using NoSln.Specifications.Model;

namespace NoSln.Specifications.OutputPipeline
{
    [Subject(typeof(FileInclusionHierarchy))]
    public class when_matching_a_file_with_excludes
    {
        static AutoMoq<FileInclusionHierarchy> fileInclusionHierarchy;
        Establish context = () => 
                                {
                                    fileInclusionHierarchy = new AutoMoq<FileInclusionHierarchy>(
                                        EntityFactory.CreateInclusionPolicy(new[] { "*.dll" , "abc"}, new [] { "*.cs", "abc"}));
                                    fileInclusionHierarchy.GetMock<IWildcardMatcher>().Setup(x => x.IsMatch("file1.dll", "*.dll")).Returns(true);
                                    fileInclusionHierarchy.GetMock<IWildcardMatcher>().Setup(x => x.IsMatch("file2.cs", "*.cs")).Returns(false);
                                    fileInclusionHierarchy.GetMock<IWildcardMatcher>().Setup(x => x.IsMatch("file3", "abc")).Returns(true);
                                };

        It should_not_match_an_excluded_file = () => fileInclusionHierarchy.Object.ShouldInclude("file1.dll").ShouldBeFalse();
        
        It should_match_an_included_file = () => fileInclusionHierarchy.Object.ShouldInclude("file2.cs").ShouldBeTrue();

        It should_include_a_excluded_file_when_there_is_an_include_filter = () => fileInclusionHierarchy.Object.ShouldInclude("file3").ShouldBeTrue();
    }

    [Subject(typeof(FileInclusionHierarchy))]
    public class when_matching_a_file_and_there_is_no_inclusion_policy
    {
        static bool result;
        static AutoMoq<FileInclusionHierarchy> fileInclusionHierarchy;

        Establish context = () => 
                                {
                                    fileInclusionHierarchy = new AutoMoq<FileInclusionHierarchy>();
                                };

        Because of = () => result = fileInclusionHierarchy.Object.ShouldInclude("dave");

        It should_include_all_files = () => result.ShouldBeTrue();
    }
}