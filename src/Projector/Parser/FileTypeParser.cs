using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Projector.Collections;
using Projector.Model;

namespace Projector.Parser
{
    public class FileTypeParser : IFileParser
    {
        static readonly Regex FileTypeRegex = new Regex("([\\*\\./a-z0-9]+)\\s*:((?:\\s*[a-z]+\\s*=\\s*[^;]+;)+)", RegexOptions.IgnoreCase);
        static readonly Regex FileTypeOptionsRegex = new Regex("\\s*([a-z]+)\\s*=\\s*([^;]+);", RegexOptions.IgnoreCase);
        public void Parse(string file, CodeDirectory codeDirectory)
        {
            var fileTypes = new List<FileType>();
            var matches = FileTypeRegex.Matches(file);
            foreach (Match match in matches)
            {
                var fileType = new FileType {FileNameWildcard = match.Groups[1].Value};
                var options = FileTypeOptionsRegex.Matches(match.Groups[2].Value)
                    .Cast<Match>()
                    .ToDictionary(x => x.Groups[1].Value, x => x.Groups[2].Value, StringComparer.InvariantCultureIgnoreCase);
                fileType.BuildAction = options.FindItem("BuildAction", "Compile").ToEnum<BuildAction>();
                fileType.DependentUpon = options.FindItem("DependentUpon");
                fileTypes.Add(fileType);
            }
            codeDirectory.FileTypes = fileTypes;
        }
    }
}