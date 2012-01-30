using Auto.Moq;
using Machine.Specifications;
using Moq;
using Projector.Conventions.SuggestedStructure;
using Projector.IO;
using Projector.Specifications.IO;
using It = Machine.Specifications.It;

namespace Projector.Specifications.Conventions.SuggestedStructure
{
    [Subject(typeof(GitIgnoreGenerator))]
    public abstract class when_generatoring_git_ignore
    {
        protected static AutoMoq<GitIgnoreGenerator> gitIgnoreGenerator;
        protected static TestDirectory directory;

        Establish context = () => 
                                {
                                    gitIgnoreGenerator = new AutoMoq<GitIgnoreGenerator>();
                                    directory = new TestDirectory {Path = "c:\\"};
                                    gitIgnoreGenerator.GetMock<IResourceProvider>().Setup(x => x.ReadResource<GitIgnoreGenerator>("Resources.GitIgnore.txt")).Returns("GitIgnore");
                                };

        Because of = () => gitIgnoreGenerator.Object.Update(directory);
    }

    [Subject(typeof(GitIgnoreGenerator))]
    public class when_there_is_no_git_ignore_file_in_the_foot_folder : when_generatoring_git_ignore
    {
        It should_create_a_git_ignore_file = () => gitIgnoreGenerator.GetMock<IFileSystem>().Verify(x => x.WriteFile("c:\\.gitignore", "GitIgnore"));
    }

    [Subject(typeof(GitIgnoreGenerator))]
    public class when_there_a_git_ignore_file_in_the_foot_folder : when_generatoring_git_ignore
    {
        Establish context = () => directory.Files = new[] {new TestFile(".gitignore")};

        It should_create_a_git_ignore_file = () => gitIgnoreGenerator.GetMock<IFileSystem>().Verify(x => x.WriteFile("c:\\.gitignore", "GitIgnore"), Times.Never());
    }
}