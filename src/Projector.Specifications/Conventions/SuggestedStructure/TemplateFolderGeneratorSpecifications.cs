using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Projector.Conventions.SuggestedStructure;
using Projector.IO;
using Projector.Specifications.IO;

namespace Projector.Specifications.Conventions.SuggestedStructure
{
    [Subject(typeof(TemplateFolderGenerator))]
    public abstract class TemplateFolderGeneratorSpecifications
    {
        protected static TestDirectory directory;
        protected static AutoMoq<TemplateFolderGenerator> templateFolderGenerator;

        Establish context = () => 
        {
            templateFolderGenerator = new AutoMoq<TemplateFolderGenerator>();
            directory = new TestDirectory {Path = "c:\\test"};
        };

        Because of = () => templateFolderGenerator.Object.Update(directory);
    }

    [Subject(typeof(TemplateFolderGenerator))]
    public class when_folder_already_contains_a_templates_directory : TemplateFolderGeneratorSpecifications
    {
        static TestDirectory templateDir;

        Establish context = () =>
        {
            templateDir = new TestDirectory { Name = "_templates"};
            directory.Directories = new IDirectory[]
            {
                templateDir
            };
        };

        It should_not_modify_it = () => templateDir.Directories.Count().ShouldEqual(0);
    }

    [Subject(typeof(TemplateFolderGenerator))]
    public class when_template_folder_does_not_exist : TemplateFolderGeneratorSpecifications
    {
        It should_create_the_folder = () => directory.Directories.FirstOrDefault(x => x.Name == "_templates").ShouldNotBeNull();

        It should_add_templates_to_the_folder = () => directory.Directories.First(x => x.Name == "_templates").Files.Any().ShouldBeTrue();
    }
}