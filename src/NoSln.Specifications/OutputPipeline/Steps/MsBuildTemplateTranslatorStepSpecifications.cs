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
        static string template;
        static MsBuildTemplateTranslatorStep msBuildTemplateTranslatorStep;
        static Project project;
        static Solution solution;
        static CodeDirectory codeDirectory;

        Establish context = () =>
        {
            template = @"<?xml version='1.0' encoding='utf-8'?>
                        <Project ToolsVersion='4.0' DefaultTargets='Build' xmlns='http://schemas.microsoft.com/developer/msbuild/2003'>
                          <PropertyGroup>
                            <AppDesignerFolder>Properties</AppDesignerFolder>
                            <RootNamespace>NoSln</RootNamespace>
                            <AssemblyName>NoSln</AssemblyName>
                            <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
                            <TargetFrameworkProfile>
                            </TargetFrameworkProfile>
                            <FileAlignment>512</FileAlignment>
                          </PropertyGroup>
                        </Project>";
            msBuildTemplateTranslatorStep = new MsBuildTemplateTranslatorStep();
            solution = new Solution();
            project = new Project {AssemblyName = "a project"};
            solution.AddProject(project);
            codeDirectory = TestEntityFactory.CreateCodeDirectory("test");
            codeDirectory.AddProject("a project").ProjectTemplate = template;
        };

        Because of = () => msBuildTemplateTranslatorStep.Execute(solution, codeDirectory);

        It should_parse_the_file_as_xml = () => project.ProjectTemplate.Xml.Root.Name.LocalName.ShouldEqual("Project");
    }
}