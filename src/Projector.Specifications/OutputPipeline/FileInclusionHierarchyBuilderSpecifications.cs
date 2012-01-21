using Auto.Moq;
using Machine.Specifications;
using Projector.Model;
using Projector.OutputPipeline;
using Projector.Specifications.Model;

namespace Projector.Specifications.OutputPipeline
{
    [Subject(typeof(FileInclusionHierarchyBuilder))]
    public class when_building_inclusion_policy_hierarchy
    {
        static IFileInclusionHierarchy initialHierarchy;
        static FileInclusionPolicy policyToCombine;
        static IFileInclusionHierarchy resultantHierarchy;
        static AutoMoq<FileInclusionHierarchyBuilder> fileInclusionHierarchyBuilder;

        Establish context = () =>
                                {
                                    fileInclusionHierarchyBuilder = new AutoMoq<FileInclusionHierarchyBuilder>();
                                    initialHierarchy = fileInclusionHierarchyBuilder.Object.Create(TestEntityFactory.CreateInclusionPolicy(new[] { "*.dll" }, new [] { "*.cs" }));
                                    policyToCombine = TestEntityFactory.CreateInclusionPolicy(new[] { "*.txt" }, new [] { "*.spark" });
                                };

        Because of = () => { resultantHierarchy = fileInclusionHierarchyBuilder.Object.Combine(initialHierarchy, policyToCombine); };

        It should_combine_excludes_with_parent = () => resultantHierarchy.Policy.ExcludeFilters.ShouldContainOnly("*.dll", "*.txt");

        It should_combine_includes_with_parent = () => resultantHierarchy.Policy.IncludeFilters.ShouldContainOnly("*.cs", "*.spark");
    }
}