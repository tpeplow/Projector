using System.Collections.Generic;

namespace Projector.Model
{
    public class FileInclusionPolicy
    {
        readonly HashSet<string> excludeFilters = new HashSet<string>();
        readonly HashSet<string> includeFilters = new HashSet<string>(); 

        public FileInclusionPolicy()
        {
            ExcludeFilters = excludeFilters;
            IncludeFilters = includeFilters;
        }

        public IEnumerable<string> ExcludeFilters { get; private set; }
        public IEnumerable<string> IncludeFilters { get; private set; }

        public void AddExclude(string filter)
        {
            excludeFilters.Add(filter);
        }

        public void AddInclude(string filter)
        {
            includeFilters.Add(filter);
        }
    }
}