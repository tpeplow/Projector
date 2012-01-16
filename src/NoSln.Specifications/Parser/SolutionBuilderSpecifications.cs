using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Moq;
using NoSln.IO;
using NoSln.Model;
using NoSln.Parser;
using NoSln.Specifications.IO;
using It = Machine.Specifications.It;
using Arg = Moq.It;

namespace NoSln.Specifications.Parser
{
    [Subject(typeof(SolutionBuilder))]
    public class when_building_a_solution
    {
        static CodeDirectory solution;
        static AutoMoq<SolutionBuilder> solutionBuilder;
        static Mock<IFileParser> parser;

        Establish context = () => 
                                {
                                    solutionBuilder = new AutoMoq<SolutionBuilder>();
                                    solutionBuilder.GetMock<IFileSystem>()
                                        .Setup(x => x.GetDirectory("projectPath"))
                                        .Returns(new TestDirectory
                                                     {
                                                         Name = "Some Project",
                                                         Path = "c:\\some project",
                                                         Directories = new []
                                                                           {
                                                                               new TestDirectory
                                                                                   {
                                                                                       Name = "sub directory",
                                                                                       Path = "c:\\some project\\sub directory",
                                                                                       Files = new [] { new TestFile("SomeClass.cs") }
                                                                                   }
                                                                           },
                                                         Files = new []
                                                                     {
                                                                         new TestFile("stuff.toparse") { Contents = "stuff to parse"}, 
                                                                         new TestFile("AClass.cs")
                                                                     }
                                                     });
                                    parser = new Mock<IFileParser>();
                                    solutionBuilder.GetMock<IParserRegistry>()
                                        .Setup(x => x.GetParserForFile("stuff.toparse"))
                                        .Returns(() => parser.Object);
                                };

        Because of = () => solution = solutionBuilder.Object.BuildFromPath("projectPath");

        It should_set_directory_path = () => solution.Path.ShouldEqual("c:\\some project");

        It should_set_directory_name = () => solution.Name.ShouldEqual("Some Project");

        It should_add_all_files = () => solution.Files.Any(x => x.FileName == "AClass.cs").ShouldBeTrue();

        It should_parse_files_with_a_matching_parser = () => 
            parser.Verify(x => x.Parse(Arg.Is<string>(s => s == "stuff to parse"), Arg.Is<CodeDirectory>(f => true)));

        It should_not_add_parsed_files_to_the_code_directory = () => solution.Files.Any(x => x.FileName == "stuff.topare").ShouldBeFalse();

        It should_recurse_all_sub_directories = () => solution.Directories.First().Name.ShouldEqual("sub directory");

        It should_include_files_from_sub_directories = () => solution.Directories.First().Files.Any(x => x.FileName == "SomeClass.cs").ShouldBeTrue();
    }
}