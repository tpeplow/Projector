using Machine.Specifications;
using Projector.IO;

namespace Projector.Specifications.IO
{
    [Subject(typeof(FilePathDictionary<string>))]
    public class when_finding_closest
    {
        protected static string pathToCompare;
        protected static FilePathDictionary<string> filePathDictionary;
        protected static string result;

        Establish context = () => 
                                {
                                    filePathDictionary = new FilePathDictionary<string>();
                                };

        Because of = () => result = filePathDictionary.FindClosest(pathToCompare);
    }

    [Subject(typeof(FilePathDictionary<string>))]
    public class when_file_path_is_exact_match : when_finding_closest
    {
        Establish context = () =>
        {
            pathToCompare = "c:\\test\\anitem";
            filePathDictionary.Add("c:\\test\\anitem", "the item");
        };

        It should_match_the_item = () => result.ShouldEqual("the item");
    }

    [Subject(typeof(FilePathDictionary<string>))]
    public class when_file_path_is_sub_directory : when_finding_closest
    {
        Establish context = () =>
        {
            pathToCompare = "c:\\test\\anitem";
            filePathDictionary.Add("c:\\test", "the item");
        };

        It should_match_the_item = () => result.ShouldEqual("the item");
    }

    [Subject(typeof(FilePathDictionary<string>))]
    public class when_there_are_multiple_parent_directories : when_finding_closest
    {
        Establish context = () =>
        {
            pathToCompare = "c:\\test\\anitem\\a test";
            filePathDictionary.Add("c:\\test", "the wrong item");
            filePathDictionary.Add("c:\\test\\anitem", "the item");
        };

        It should_return_the_nearest_parent = () => result.ShouldEqual("the item");
    }

    [Subject(typeof(FilePathDictionary<string>))]
    public class when_path_you_are_looking_for_matches_a_child_directory : when_finding_closest
    {
        Establish context = () =>
        {
            pathToCompare = "c:\\test\\anitem\\a test";
            filePathDictionary.Add("c:\\test", "the wrong item");
            filePathDictionary.Add("c:\\test\\anitem", "the item");
            filePathDictionary.Add("c:\\test\\anitem\\a test\\its child", "wrong item");
        };

        It it_should_only_match_parent_items = () => result.ShouldEqual("the item");
    }
}