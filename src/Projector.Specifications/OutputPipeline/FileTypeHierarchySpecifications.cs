using Auto.Moq;
using Machine.Specifications;
using Projector.Model;
using Projector.OutputPipeline;
using Projector.Specifications.Model;
using Arg = Moq.It;

namespace Projector.Specifications.OutputPipeline
{
    [Subject(typeof(FileTypeHierarchyBuilder))]
    public class when_building_file_type_hierarchy
    {
        protected static AutoMoq<FileTypeHierarchyBuilder> fileTypeHierarchyBuilder;
        protected static IFileTypeHierarchy result;
        protected static CodeDirectory createCodeDirectory;

        Establish context = () =>
        {
            createCodeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            fileTypeHierarchyBuilder = new AutoMoq<FileTypeHierarchyBuilder>();
            fileTypeHierarchyBuilder.GetMock<IWildcardMatcher>().Setup(x => x.IsMatch(Arg.IsAny<string>(), "*.cs")).Returns(true);
        };

        Because of = () => result = fileTypeHierarchyBuilder.Object.Generate(createCodeDirectory);
    }

    [Subject(typeof(FileTypeHierarchyBuilder))]
    public class when_there_is_no_matching_file_type : when_building_file_type_hierarchy
    {
        It should_not_return_any_file_type = () => result.GetFileType("c:\\test\\path\\blah.cs").ShouldBeNull();
    }

    [Subject(typeof(FileTypeHierarchyBuilder))]
    public class when_there_is_a_single_file_type_file : when_building_file_type_hierarchy
    {
        Establish context = () => createCodeDirectory.FileTypes = new[] { new FileType { FileNameWildcard = "*.cs" } };

        It should_include_those_file_types = () => result.GetFileType("c:\\test\\path\\blah.cs").ShouldNotBeNull();
    }

    [Subject(typeof(FileTypeHierarchyBuilder))]
    public class when_there_is_a_overridden_file_type : when_building_file_type_hierarchy
    {
        Establish context = () =>
        {
            var codeDirectory = new CodeDirectory("dave", "c:\\test\\path\\dave")
            {
                FileTypes = new[] {new FileType {FileNameWildcard = "*.cs"}}
            };
            createCodeDirectory.AddCodeDirectory(codeDirectory);
        };

        It should_not_return_any_file_type_for_types_above_overriden_path = () => result.GetFileType("c:\\test\\path\\blah.cs").ShouldBeNull();

        It should_match_file_types_beneath_override = () => result.GetFileType("c:\\test\\path\\dave\\a file.cs").ShouldNotBeNull();
    }
}