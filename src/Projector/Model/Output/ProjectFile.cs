namespace Projector.Model.Output
{
    public class ProjectFile
    {
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
        public BuildAction BuildAction { get; set; }
        public string DependentUpon { get; set; }
    }
}