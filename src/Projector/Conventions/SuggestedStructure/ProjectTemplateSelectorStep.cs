using System;
using System.Linq;
using System.Xml.Linq;
using Projector.Model;
using Projector.Model.Output;
using Projector.OutputPipeline;

namespace Projector.Conventions.SuggestedStructure
{
    public class ProjectTemplateSelectorStep : IOutputPipelineStep
    {
        readonly IProjectTypeNamingConvention projectTypeNamingConvention;

        public ProjectTemplateSelectorStep(IProjectTypeNamingConvention projectTypeNamingConvention)
        {
            this.projectTypeNamingConvention = projectTypeNamingConvention;
        }

        public void Execute(Solution solution, CodeDirectory codeDirectory)
        {
            var templateFolder = codeDirectory.Directories.FirstOrDefault(x => x.Name.Equals("_templates", StringComparison.InvariantCultureIgnoreCase));
            if (templateFolder == null) return;
            foreach (var project in solution.Projects.Where(x => x.ProjectTemplate == null))
            {
                var projectType = projectTypeNamingConvention.GetProjectType(project.Name);
                var xml = templateFolder.Files.FirstOrDefault(f => f.FileName.StartsWith(projectType.Name, StringComparison.InvariantCultureIgnoreCase));
                if (xml == null)
                {
                    continue;
                }
                project.ProjectTemplate = new ProjectTemplate { Xml = XDocument.Parse(xml.Contents) };
            }
        }
    }
}