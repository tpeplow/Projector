using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NoSln.Model;

namespace NoSln.Parser
{
    public class ProjectParser : IFileParser
    {
        static readonly Regex ProjectRegex = new Regex(@"([a-z]+):\s*(.+)", RegexOptions.IgnoreCase);
        readonly IGuidGenerator guidGenerator;

        public ProjectParser(IGuidGenerator guidGenerator)
        {
            if (guidGenerator == null) throw new ArgumentNullException("guidGenerator");
            this.guidGenerator = guidGenerator;
        }

        public ProjectInfo Parse(string projectFile)
        {
            var values = ProjectRegex.Matches(projectFile)
                .Cast<Match>()
                .Select(x => new {Name = x.Groups[1].Value, Value = x.Groups[2].Value.Trim()})
                .ToDictionary(x => x.Name, x => x.Value, StringComparer.InvariantCultureIgnoreCase);

            var projectName = GetValue(values, "Name");
            return new ProjectInfo
                       {
                           Name = projectName,
                           OutputType = GetValue(values, "OutputType", ifNull: "Library"),
                           Namespace = GetValue(values, "Namespace"),
                           Guid = Guid.Parse(GetValue(values, "ProjectGuid", ifNull: guidGenerator.Generate().ToString())),
                           AssemblyName = GetValue(values, "AssemblyName", ifNull: projectName)
                       };
        }

        private static string GetValue(IDictionary<string, string> values, string key, bool required = true, string ifNull = null)
        {
            string value;
            if (!values.TryGetValue(key, out value) && required && ifNull == null)
            {
                throw new KeyNotFoundException(string.Format("Cannot find {0} in the project file, this field is required", key));
            }
            return value ?? ifNull;
        }

        void IFileParser.Parse(string file, CodeDirectory codeDirectory)
        {
            codeDirectory.Project = Parse(file);
        }
    }
}