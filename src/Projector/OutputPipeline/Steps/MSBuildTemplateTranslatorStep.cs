using System.Xml.Linq;
using Projector.Collections;
using Projector.Model;
using Projector.Model.Output;

namespace Projector.OutputPipeline.Steps
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
            return new ProjectTemplate {Xml = XDocument.Parse(projectTemplate)};
        }
    }
}