using System.Linq;

namespace Projector.Conventions.SuggestedStructure
{
    public interface IProjectTypeNamingConvention
    {
        ProjectType GetProjectType(string directoryName);
    }

    public class ProjectTypeNamingConvention : IProjectTypeNamingConvention
    {
        public ProjectType GetProjectType(string directoryName)
        {
            return ProjectTypes.All.FirstOrDefault(x => directoryName.ContainsIgnoreCase(x.NamingConventions.ToArray())) ?? ProjectTypes.Default;
        }
    }
}