using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
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
                ProjectTemplate = new ProjectTemplate {Xml = XDocument.Parse("<root />")}
            };
            solution.AddProject(project);
            StubWriter<Project>("<n_1 />");
            StubWriter<IEnumerable<AssemblyReference>>("<n_2 />");
            StubWriter<IEnumerable<ProjectReference>>("<n_3 />");
            StubWriter<IEnumerable<ProjectFile>>("<n_4 />");

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

        It should_write_the_parts_in_order = () =>
            XDocument.Parse(writtenProjectFile).Root.Elements().Select(x => x.Name.LocalName).ToArray().ShouldBeEquivalentTo(new [] { "n_1", "n_2", "n_3", "n_4" });

        It should_write_the_project_file_to_the_project_folder = () => writtenProjectFilePath.ShouldEqual("c:\\project\\proj.ext");

        static void StubWriter<TPart>(string content)
        {
            var writer = new Mock<IOutputXmlWriter<TPart>>();
            writer
                .Setup(x => x.Write(Arg.IsAny<object>(), Arg.IsAny<XDocument>()))
                .Callback<object, XDocument>((p, d) => d.Root.Add(XDocument.Parse(content).FirstNode));
            msbuildFileGenerationStep.GetMock<IOutputWriterResolver>().Setup(x => x.Resolve<TPart>()).Returns(() => writer.Object);
        }
    }
}
