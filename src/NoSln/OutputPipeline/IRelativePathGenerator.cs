namespace NoSln.OutputPipeline
{
    public interface IRelativePathGenerator
    {
        string GeneratePath(string relativeTo, string fullPath);
    }
}