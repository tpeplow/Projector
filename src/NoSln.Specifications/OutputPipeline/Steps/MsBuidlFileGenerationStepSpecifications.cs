using System.Collections.Generic;
using System.Text;
using Auto.Moq;
using Machine.Specifications;
using Moq;
using NoSln.IO;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline.OutputGenerator;
using NoSln.OutputPipeline.Steps;
using NoSln.Specifications.Model;
using It = Machine.Specifications.It;
using Arg = Moq.It;

namespace NoSln.Specifications.OutputPipeline.Steps
{
    [Subject(typeof (MsBuildFileGenerationStep))]
    public class when_generating_an_msbuild_file_for_a_project
    {
        static Solution solution;
        static CodeDirectory codeDirectory;
        static AutoMoq<MsBuildFileGenerationStep> msbuildFileGenerationStep;
        static string writtenProjectFile;
        static string writtenProjectFilePath;

        Establish context = () =>
        {
            msbuildFileGenerationStep = new AutoMoq<MsBuildFileGenerationStep>();
            codeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            solution = new Solution();
            var project = new Project
            {
                Name = "proj",
                AssemblyName = "ass",
                Path = "c:\\project",
                Extension = ".ext",
                ProjectTemplate = new ProjectTemplate {Header = "1"}
            };
            solution.AddProject(project);
            StubWriter<Project>("2");
            StubWriter<IEnumerable<AssemblyReference>>("3");
            StubWriter<IEnumerable<ProjectReference>>("4");
            StubWriter<IEnumerable<ProjectFile>>("5");
            project.ProjectTemplate.Footer = "6";

            msbuildFileGenerationStep
                .GetMock<IFileSystem>()
                .Setup(x => x.WriteFile(Arg.IsAny<string>(), Arg.IsAny<string>()))
                .Callback<string, string>((path, contents) =>
                                              {
                                                  writtenProjectFilePath = path;
                                                  writtenProjectFile = contents;
                                              });
        };

        Because of = () => msbuildFileGenerationStep.Object.Execute(solution, codeDirectory);

        It should_write_the_parts_in_order = () => writtenProjectFile.ShouldEqual("123456");

        It should_write_the_project_file_to_the_project_folder = () => writtenProjectFilePath.ShouldEqual("c:\\project\\proj.ext");

        static void StubWriter<TPart>(string content)
        {
            var writer = new Mock<IOutputWriter<TPart>>();
            writer
                .Setup(x => x.Write(Arg.IsAny<object>(), Arg.IsAny<StringBuilder>()))
                .Callback<object, StringBuilder>((p, s) => s.Append(content));
            msbuildFileGenerationStep.GetMock<IOutputWriterResolver>().Setup(x => x.Resolve<TPart>()).Returns(() => writer.Object);
        }
    }
}
