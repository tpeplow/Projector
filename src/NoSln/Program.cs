using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NoSln
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            var projFile = Directory.GetFiles(path, "Proj.nosln").First();
            var references = Directory.GetFiles(path, "References.nosln").First();


            BuildProjectDetails(projFile, references, path);
        }

        private static void BuildProjectDetails(string projFilePath, string referencesPath, string projDir)
        {
            var genedProjFile = new StringBuilder();
            genedProjFile.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");

            var parameters = (from match in Regex.Matches(File.ReadAllText(projFilePath), "([a-z]+):(.+)", RegexOptions.IgnoreCase).OfType<Match>()
                              select new
                                         {
                                             Name = match.Groups[1].Value,
                                             Value = match.Groups[2].Value.Replace("\r", string.Empty).Trim()
                                         }).ToDictionary(x => x.Name);

            genedProjFile.AppendFormat("<PropertyGroup>" +
            "<Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>" +
            "<Platform Condition=\" '$(Platform)' == '' \">x86</Platform>" +
            "<ProductVersion>8.0.30703</ProductVersion>" +
            "<SchemaVersion>2.0</SchemaVersion>" +
            "<ProjectGuid>{0}</ProjectGuid>" +
            "<OutputType>{1}</OutputType>" +
            "<AppDesignerFolder>Properties</AppDesignerFolder>" +
            "<RootNamespace>{2}</RootNamespace>" +
            "<AssemblyName>{3}</AssemblyName>" +
            "<TargetFrameworkVersion>v4.0</TargetFrameworkVersion>" +
            "<TargetFrameworkProfile>Client</TargetFrameworkProfile>" +
            "<FileAlignment>512</FileAlignment>" +
            "</PropertyGroup>",  parameters["ProjectGuid"].Value, parameters["OutputType"].Value, parameters["Namespace"].Value, parameters["Name"].Value);

            genedProjFile.Append(Stuff.DebugReleaseInfo);

            AddReferences(referencesPath, genedProjFile);

            AddFiles(projDir, genedProjFile);

            genedProjFile.Append("</Project>");

            File.WriteAllText(Path.Combine(projDir, parameters["Name"].Value + ".csproj"), genedProjFile.ToString());
        }

        private static void AddFiles(string projDir, StringBuilder genedProjFile)
        {
            genedProjFile.Append("<ItemGroup>");
            foreach (var file in Directory.GetFiles(projDir, "*.cs", SearchOption.AllDirectories))
            {
                genedProjFile.AppendFormat("<Compile Include=\"{0}\" />", file);
            }
            genedProjFile.Append("</ItemGroup>");
        }

        private static void AddReferences(string referencesPath, StringBuilder genedProjFile)
        {
            genedProjFile.Append("<ItemGroup>");
            foreach (var line in File.ReadLines(referencesPath))
            {
                genedProjFile.AppendFormat("<Reference Include=\"{0}\" />", line);
            }
            genedProjFile.Append("</ItemGroup>");
        }
    }
}
