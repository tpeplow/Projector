using System;
using NoSln.Collections;
using NoSln.Model;

namespace NoSln.OutputPipeline
{
    public class FileInclusionHierarchyBuilder
    {
        public IFileInclusionHierarchy Create(FileInclusionPolicy initalPolicy)
        {
            return new FileInclusionHierarchy(initalPolicy);
        }
        public IFileInclusionHierarchy Combine(IFileInclusionHierarchy hierarchy, FileInclusionPolicy secondPolicy)
        {
            if (hierarchy == null) throw new ArgumentNullException("hierarchy");
            if (secondPolicy == null) throw new ArgumentNullException("secondPolicy");

            var newPolicy = new FileInclusionPolicy();
            AddFrom(hierarchy.Policy, newPolicy);
            AddFrom(secondPolicy, newPolicy);

            return new FileInclusionHierarchy(newPolicy);
        }

        static void AddFrom(FileInclusionPolicy from, FileInclusionPolicy to)
        {
            from.ExcludeFilters.Each(to.AddExclude);
            from.IncludeFilters.Each(to.AddInclude);
        }
    }
}