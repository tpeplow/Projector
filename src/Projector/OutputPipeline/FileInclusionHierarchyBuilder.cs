using System;
using Projector.Collections;
using Projector.Model;

namespace Projector.OutputPipeline
{
    public interface IFileInclusionHierarchyBuilder
    {
        IFileInclusionHierarchy Create(FileInclusionPolicy initalPolicy);
        IFileInclusionHierarchy Combine(IFileInclusionHierarchy hierarchy, FileInclusionPolicy secondPolicy);
    }

    public class FileInclusionHierarchyBuilder : IFileInclusionHierarchyBuilder
    {
        readonly IWildcardMatcher wildcardMatcher;

        public FileInclusionHierarchyBuilder(IWildcardMatcher wildcardMatcher)
        {
            this.wildcardMatcher = wildcardMatcher;
        }

        public IFileInclusionHierarchy Create(FileInclusionPolicy initalPolicy)
        {
            return new FileInclusionHierarchy(wildcardMatcher, initalPolicy);
        }
        public IFileInclusionHierarchy Combine(IFileInclusionHierarchy hierarchy, FileInclusionPolicy secondPolicy)
        {
            if (hierarchy == null) throw new ArgumentNullException("hierarchy");
            if (secondPolicy == null) throw new ArgumentNullException("secondPolicy");

            var newPolicy = new FileInclusionPolicy();
            AddFrom(hierarchy.Policy, newPolicy);
            AddFrom(secondPolicy, newPolicy);

            return new FileInclusionHierarchy(wildcardMatcher, newPolicy);
        }

        static void AddFrom(FileInclusionPolicy from, FileInclusionPolicy to)
        {
            from.ExcludeFilters.Each(to.AddExclude);
            from.IncludeFilters.Each(to.AddInclude);
        }
    }
}