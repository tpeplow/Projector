using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Projector.Model;
using Projector.Model.Validation;

namespace Projector.Parser
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

            var failures = new List<SolutionValidationFailureReason>();
            var projectName = GetValue(values, "Name", failures);
            var project = new ProjectInfo
            {
                Name = projectName,
                OutputType = GetValue(values, "OutputType", failures, ifNull: "Library"),
                Namespace = GetValue(values, "Namespace", failures),
                Guid = Guid.Parse(GetValue(values, "ProjectGuid", failures, ifNull: guidGenerator.Generate().ToString())),
                AssemblyName = GetValue(values, "AssemblyName", failures, ifNull: projectName),
                Extension = GetValue(values, "Extension", failures, ifNull: ".csproj"),
                ProjectTypeGuid = Guid.Parse(GetValue(values, "ProjectTypeGuid", failures, ifNull: "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"))
            };

            if (failures.Any()) throw new SolutionValidationException(failures);

            return project;
        }

        private static string GetValue(IDictionary<string, string> values, string key, List<SolutionValidationFailureReason> failures, bool required = true, string ifNull = null)
        {
            string value;
            if (!values.TryGetValue(key, out value) && required && ifNull == null)
            {
                failures.Add(new SolutionValidationFailureReason(SolutionValidationFailureReasons.InvalidProjectFile, string.Format("Cannot find {0} in the project file, this field is required", key)));
            }
            return value ?? ifNull;
        }

        void IFileParser.Parse(string file, CodeDirectory codeDirectory)
        {
            codeDirectory.Project = Parse(file);
        }
    }
}