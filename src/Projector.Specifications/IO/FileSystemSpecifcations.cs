using System.IO;
using System.Linq;
using Machine.Specifications;
using Projector.IO;

namespace Projector.Specifications.IO
{
    [Subject(typeof(FileSystem))]
    public class when_reading_from_the_file_system
    {
        static FileSystem fileSystem;
        static IDirectory result;

        Establish context = () =>
                                        {
                                            fileSystem = new FileSystem();
                                            Directory.CreateDirectory("temp");
                                            File.WriteAllText("temp/hello.txt", "hello world");
                                            Directory.CreateDirectory("temp/child");
                                            File.WriteAllText("temp/child/child.txt", "i'm a child");
                                        };

        Because of = () => result = fileSystem.GetDirectories(".").FirstOrDefault(x => x.Path.EndsWith("temp\\"));

        It should_list_all_directories = () => result.ShouldNotBeNull();

        It should_list_directory_names = () => result.Name.ShouldEqual("temp");

        It should_list_sub_directories = () => result.Directories.First().Path.EndsWith("child\\").ShouldBeTrue();

        It should_list_files = () => result.Files.First().FilePath.EndsWith("hello.txt");

        It should_list_file_names = () => result.Files.First().FileName.Equals("hello.txt");

        It should_read_file_contents = () => result.Files.First().Contents.ShouldEqual("hello world");

        It should_list_files_in_sub_directories = () => result.Directories.First().Files.First().FilePath.EndsWith("child.txt");

        Cleanup remove_files = () => Directory.Delete("temp", true);
    }

    [Subject(typeof(FileSystem))]
    public class when_writing_a_file_to_the_file_system
    {
        static FileSystem fileSystem;
        Establish context = () =>
                                {
                                    Directory.CreateDirectory("temp");
                                    fileSystem = new FileSystem();
                                };

        Because of = () => fileSystem.WriteFile("temp/some file.txt", "hello world");

        It should_write_the_file_to_the_correct_path =
            () => File.ReadAllText("temp/some file.txt").ShouldEqual("hello world");

        Cleanup remove_files = () => Directory.Delete("temp", true);
    }
}