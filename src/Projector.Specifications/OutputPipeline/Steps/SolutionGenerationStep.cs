using System;
using Auto.Moq;
using Machine.Specifications;
using Projector.IO;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;
using Projector.OutputPipeline.Steps;
using Arg = Moq.It;

namespace Projector.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(SolutionGenerationStep))]
    public class when_generating_the_solution
    {
        static Solution solution;
        static CodeDirectory codeDirectory;
        static SolutionGenerationStep solutionGenerationStep;
        static Project project;
        static string solutionContents;

        Establish context = () =>
        {
            var solutionGenerationStepMock = new AutoMoq<SolutionGenerationStep>();
            solutionGenerationStepMock
                .GetMock<IFileSystem>()
                .Setup(x => x.WriteFile("c:\\solution\\All.sln", Arg.IsAny<string>()))
                .Callback<string,string>((path, contents) => solutionContents = contents);
            solutionGenerationStepMock
                .GetMock<IRelativePathGenerator>()
                .Setup(x => x.GeneratePath("c:\\solution", "non relative project path"))
                .Returns("project path");
            solutionGenerationStep = solutionGenerationStepMock.Object;

            project = new Project
            {
                AssemblyName = "assembly",
                Name = "name",
                GeneratedProjectFilePath = "non relative project path",
                Guid = Guid.NewGuid(),
                Extension = ".csproj",
                ProjectTypeGuid = Guid.NewGuid()
            };
            solution = new Solution();
            solution.AddProject(project);
            solution.SolutionPath = "c:\\solution";
        };

        Because of = () => solutionGenerationStep.Execute(solution, codeDirectory);

        It should_output_title = () => solutionContents.StartsWith("Microsoft Visual Studio Solution File, Format Version 11.00").ShouldBeTrue();

        It should_output_project = () => 
            solutionContents.Contains(string.Format("Project(\"{0:B}\") = \"{1}\", \"{2}\", \"{3:B}\"", project.ProjectTypeGuid, project.Name, "project path", project.Guid))
            .ShouldBeTrue();

        It should_output_end_project = () => solutionContents.EndsWith("EndProject" + Environment.NewLine).ShouldBeTrue();
    }
}