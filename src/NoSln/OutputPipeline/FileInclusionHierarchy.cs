using System.Linq;
using NoSln.Model;

namespace NoSln.OutputPipeline
{
    public class FileInclusionHierarchy : IFileInclusionHierarchy
    {
        readonly IWildcardMatcher wildcardMatcher;

        public FileInclusionHierarchy(IWildcardMatcher wildcardMatcher, FileInclusionPolicy policy)
        {
            this.wildcardMatcher = wildcardMatcher;
            Policy = policy;
        }

        public FileInclusionPolicy Policy { get; private set; }

        public bool ShouldInclude(string path)
        {
            var shouldExclude = Policy.ExcludeFilters.Any(exclude => wildcardMatcher.IsMatch(path, exclude));
            var shouldInclude = Policy.IncludeFilters.Any(include => wildcardMatcher.IsMatch(path, include));

            return shouldInclude || !shouldExclude;
        }
    }
}