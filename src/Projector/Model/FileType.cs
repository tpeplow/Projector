namespace Projector.Model
{
    public class FileType
    {
        public string FileNameWildcard { get; set; }
        public BuildAction BuildAction { get; set; }
        public string DependentUpon { get; set; }
    }
}