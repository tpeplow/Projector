using System;
using System.Linq;
using Machine.Specifications;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline.Steps;
using NoSln.Specifications.Model;

namespace NoSln.Specifications.OutputPipeline.Steps
{
    [Subject(typeof(SolutionStructureStep))]
    public class when_building_solution_structure_from_code_directory
    {
        static Solution solution;
        static Project mappedProject;
        static CodeDirectory codeDirectory;
        static SolutionStructureStep solutionStructureStep;

        Establish context = () => 
                                {
                                    codeDirectory = CreateDirectoryStructure();
                                    solution = new Solution();
                                    solutionStructureStep = new SolutionStructureStep();
                                };

        Because of = () =>
                         {
                             solutionStructureStep.Execute(solution, codeDirectory);
                             mappedProject = solution.Projects.First();
                         };

        It should_create_a_project_for_each_directory_with_a_project_file = () => solution.Projects.Select(x => x.Name).ShouldContainOnly("ProjectA", "ProjectB");

        It should_not_create_project_for_directory_without_project_file = () => solution.Projects.Select(x => x.Name).ShouldNotContain("some source folder");

        It should_map_the_project_name = () => mappedProject.Name.ShouldEqual("ProjectA");

        It should_map_the_project_path = () => mappedProject.Path.ShouldEqual("ProjectA\\path");

        It should_map_namespace = () => mappedProject.Namespace.ShouldEqual("ProjectA.namespace");

        It should_map_output_type = () => mappedProject.OutputType.ShouldEqual("exe");

        It should_map_assembly_name = () => mappedProject.AssemblyName.ShouldEqual("ProjectA");

        It should_map_guid = () => mappedProject.Guid.ShouldNotEqual(Guid.Empty);

        It should_map_solution_extenstion = () => mappedProject.Extension.ShouldEqual(".csproj");
        
        static CodeDirectory CreateDirectoryStructure()
        {
            var codeDirectory = new CodeDirectory("test", ".");
            var projectA = codeDirectory.AddProject("ProjectA");
            var directory = new CodeDirectory("some source folder", ".");
            projectA.AddCodeDirectory(directory);
            directory.AddProject("ProjectB");
            return codeDirectory;
        }
    }
}