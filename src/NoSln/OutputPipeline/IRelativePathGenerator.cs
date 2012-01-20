using System;
using System.IO;

namespace NoSln.OutputPipeline
{
    public interface IRelativePathGenerator
    {
        string GeneratePath(string relativeTo, string fullPath);
    }

    public class RelativePathGenerator : IRelativePathGenerator
    {
        public string GeneratePath(string relativeTo, string fullPath)
        {
            var relativeUri = new Uri(relativeTo).MakeRelativeUri(new Uri(fullPath));

            return Uri.UnescapeDataString(relativeUri.ToString()).Replace("/", "\\");
        }
    }
}