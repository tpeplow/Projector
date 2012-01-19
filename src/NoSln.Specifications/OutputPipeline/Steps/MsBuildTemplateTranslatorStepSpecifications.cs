using Machine.Specifications;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline.Steps;
using NoSln.Specifications.Model;

namespace NoSln.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(MsBuildTemplateTranslatorStep))]
    public class when_translating_an_msbuild_template
    {
        static string tempate;
        static MsBuildTemplateTranslatorStep msBuildTemplateTranslatorStep;
        static string header;
        static Project project;
        static string footer;
        static Solution solution;
        static CodeDirectory codeDirectory;

        Establish context = () =>
        {
            header = @"<?xml version='1.0' encoding='utf-8'?>
                        <Project ToolsVersion='4.0' DefaultTargets='Build' xmlns='http://schemas.microsoft.com/developer/msbuild/2003'>
                          <PropertyGroup>
                            <AppDesignerFolder>Properties</AppDesignerFolder>
                            <RootNamespace>NoSln</RootNamespace>
                            <AssemblyName>NoSln</AssemblyName>
                            <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
                            <TargetFrameworkProfile>
                            </TargetFrameworkProfile>
                            <FileAlignment>512</FileAlignment>
                          </PropertyGroup>";
            footer = "</Project>";
            tempate = header + footer;
            msBuildTemplateTranslatorStep = new MsBuildTemplateTranslatorStep();
            solution = new Solution();
            project = new Project {AssemblyName = "a project"};
            solution.AddProject(project);
            codeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            codeDirectory.AddProject("a project").ProjectTemplate = tempate;
        };

        Because of = () => msBuildTemplateTranslatorStep.Execute(solution, codeDirectory);

        It should_end_the_header_before_last_element = () => project.ProjectTemplate.Header.ShouldEqual(header);

        It should_set_the_footer_to_the_last_element = () => project.ProjectTemplate.Footer.ShouldEqual(footer);
    }
}