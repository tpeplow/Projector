using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NoSln.Parser
{
    public class ProjectParser
    {
        static readonly Regex ProjectRegex = new Regex(@"([a-z]+):\s*(.+)", RegexOptions.IgnoreCase);
        public ProjectInfo Parse(string projectFile)
        {
            var values = ProjectRegex.Matches(projectFile)
                .Cast<Match>()
                .Select(x => new {Name = x.Groups[1].Value, Value = x.Groups[2].Value.Trim()})
                .ToDictionary(x => x.Name, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

            return new ProjectInfo(GetValue(values, "Name"),
                                   GetValue(values, "OutputType"),
                                   GetValue(values, "Namespace"),
                                   Guid.Parse(GetValue(values, "ProjectGuid")));
        }

        private static string GetValue(IDictionary<string, string> values, string key, bool required = true, string ifNull = null)
        {
            string value;
            if (!values.TryGetValue(key, out value) && required)
            {
                throw new KeyNotFoundException(string.Format("Cannot find {0} in the project file, this field is required", key));
            }
            return value ?? ifNull;
        }
    }
}