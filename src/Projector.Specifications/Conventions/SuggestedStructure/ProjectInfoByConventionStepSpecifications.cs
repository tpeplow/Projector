using System;
using System.Linq;
using Auto.Moq;
using Machine.Specifications;
using Projector.Collections;
using Projector.Conventions.SuggestedStructure;
using Projector.Model;
using Projector.Model.Output;
using Projector.Parser;
using Projector.Specifications.IO;
using Projector.Specifications.Model;
using Arg = Moq.It;

namespace Projector.Specifications.Conventions.SuggestedStructure
{
    [Subject(typeof(ProjectInfoByConventionStep))]
    public abstract class ProjectInfoByConventionStepSpecificatoins
    {
        protected static AutoMoq<ProjectInfoByConventionStep> projectInfoByConventionStep;
        protected static CodeDirectory codeDirectory;
        protected static Solution solution;

        Establish context = () =>
        {
            projectInfoByConventionStep = new AutoMoq<ProjectInfoByConventionStep>(new GuidGenerator());
            codeDirectory = TestEntityFactory.CreateCodeDirectory("project");
            solution = new Solution();
        };

        Because of = () => projectInfoByConventionStep.Object.Execute(solution, codeDirectory);
    }

    [Subject(typeof(ProjectInfoByConventionStep))]
    public class when_there_is_a_source_folder : ProjectInfoByConventionStepSpecificatoins
    {
        protected static CodeDirectory sourceDirectory;
        static string[] expectedProjects = new [] { "Project1", "Project2" };

        Establish context = () =>
        {
            sourceDirectory = TestEntityFactory.CreateCodeDirectory("src");
            expectedProjects.Each(x => sourceDirectory.AddCodeDirectory(TestEntityFactory.CreateCodeDirectory(x)));
            codeDirectory.AddCodeDirectory(sourceDirectory);
            projectInfoByConventionStep.GetMock<IProjectTypeNamingConvention>().Setup(x => x.GetProjectType(Arg.IsAny<string>())).Returns(new ProjectTypes.Exe());
        };

        It should_treat_each_sub_folder_as_a_project = () => solution.Projects.Select(x => x.Name).ShouldContainOnly(expectedProjects);

        It should_use_folder_name_for_assembly_name = () => solution.Projects.Select(x => x.AssemblyName).ShouldContainOnly(expectedProjects);

        It should_use_folder_name_for_namespace = () => solution.Projects.Select(x => x.Namespace).ShouldContainOnly(expectedProjects);

        It should_set_the_output_type_by_convention = () => solution.Projects.First(x => x.Name == "Project1").OutputType.ShouldEqual(new ProjectTypes.Exe().OutputType);

        It should_set_the_guid = () => solution.Projects.Select(x => x.Guid).First().ShouldNotEqual(Guid.Empty);

        It should_set_extension_to_csproj = () => solution.Projects.Select(x => x.Extension).Distinct().ShouldContainOnly(".csproj");

        It should_set_project_type_guid = () => solution.Projects.Select(x => x.ProjectTypeGuid).Distinct().ShouldContainOnly(new ProjectTypes.Exe().ProjectTypeGuid);

        It should_set_the_path = () => solution.Projects.First().Path.ShouldNotBeNull();

        It should_set_the_generated_output_path = () => solution.Projects.First().GeneratedProjectFilePath.ShouldNotBeNull();

    }

    [Subject(typeof(ProjectInfoByConventionStep))]
    public class when_solution_alrady_contains_a_project : when_there_is_a_source_folder
    {
        Establish context = () =>
        {
            var project1 = sourceDirectory.Directories.First(x => x.Name == "Project1");
            project1.AddFile(new TestFile(ParserRegistry.ProjectFileName));
        };

        It should_ignore_that_project = () => solution.Projects.FirstOrDefault(x => x.AssemblyName == "Project1").ShouldBeNull();
    }
}