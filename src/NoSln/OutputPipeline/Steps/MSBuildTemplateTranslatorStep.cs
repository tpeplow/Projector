using System;
using System.Xml.Linq;
using NoSln.Collections;
using NoSln.Model;
using NoSln.Model.Output;

namespace NoSln.OutputPipeline.Steps
{
    public class MsBuildTemplateTranslatorStep : IOutputPipelineStep
    {
        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            if (!string.IsNullOrEmpty(codeDirectory.ProjectTemplate))
            {
                var project = solution.GetProject(codeDirectory.Project.AssemblyName);
                project.ProjectTemplate = CreateTemplate(codeDirectory.ProjectTemplate);
            }

            codeDirectory.Directories.Each(x => Execute(solution, x));
        }

        ProjectTemplate CreateTemplate(string projectTemplate)
        {
            var endTagPos = projectTemplate.IndexOf("</Project>", System.StringComparison.InvariantCultureIgnoreCase);
            if (endTagPos < 0)
            {
                throw new Exception("Project file looks invalid...");
            }
            return new ProjectTemplate
                       {
                           Header = projectTemplate.Substring(0, endTagPos),
                           Footer = "</Project>"
                       };
        }
    }
}