using Machine.Specifications;
using Projector.Model;
using Projector.OutputPipeline;
using Projector.Specifications.Model;

namespace Projector.Specifications.OutputPipeline
{
    [Subject(typeof(FileTypeHierarchyBuilder))]
    public class when_building_file_type_hierarchy
    {
        static FileTypeHierarchyBuilder fileTypeHierarchyBuilder;
        protected static IFileTypeHierarchy result;
        protected static CodeDirectory createCodeDirectory;

        Establish context = () =>
        {
            createCodeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            fileTypeHierarchyBuilder = new FileTypeHierarchyBuilder();
        };

        Because of = () => result = fileTypeHierarchyBuilder.Generate(createCodeDirectory);
    }

    [Subject(typeof(FileTypeHierarchyBuilder))]
    public class when_there_is_a_single_file_type_file : when_building_file_type_hierarchy
    {
        Establish context = () => createCodeDirectory.FileTypes = new[] { new FileType { FileNameWildcard = "*.cs" } };

        It should_include_those_file_types = () => { };
    }
}