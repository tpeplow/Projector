using System.Linq;
using Machine.Specifications;
using NoSln.Model;
using NoSln.Model.Output;
using NoSln.OutputPipeline;
using NoSln.Specifications.Model;

namespace NoSln.Specifications.OutputPipeline
{
    [Subject(typeof(ReferenceContributor))]
    public class when_building_references
    {
        static ReferenceContributor referenceContributor;
        static CodeDirectory codeDirectory;
        static Solution solution;

        Establish context = () =>
                                {
                                    solution = new Solution();
                                    solution.AddProject(new Project { AssemblyName = "ProjectA" });
                                    solution.AddProject(new Project { AssemblyName = "ProjectB" });

                                    codeDirectory = EntityFactory.CreateCodeDirectory("Test");
                                    codeDirectory.AddProject("ProjectA", "External");
                                    codeDirectory.AddProject("ProjectB", "ProjectA");

                                    referenceContributor = new ReferenceContributor();
                                };

        Because of = () => referenceContributor.Execute(solution, codeDirectory);

        It should_add_assembly_reference_to_external_dependencies = ()
            => solution.GetProject("ProjectA").AssemblyReferences.Select(x => x.Name).ShouldContain("External");

        // It should map the hint path for assembly references
        It should_map_the_hint_path_for_assembly_references = () => solution.GetProject("ProjectA").AssemblyReferences.First().HintPath.ShouldEqual("External\\path");

        It should_add_project_references_when_assembly_matches_a_project_in_the_solution = () => 
            solution.GetProject("ProjectB").ProjectReferences.Select(x => x.Project.AssemblyName).ShouldContain("ProjectA");

    }
}