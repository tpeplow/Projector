using System;
using System.IO;

namespace Projector.IO
{
    public interface IResourceProvider
    {
        string ReadResource<TRelativeTo>(string name);
    }

    public class ResourceProvider : IResourceProvider
    {
        public string ReadResource<TRelativeTo>(string name)
        {
            var relativeTo = typeof (TRelativeTo);
            using (var manifestResourceStream = relativeTo.Assembly.GetManifestResourceStream(relativeTo, name))
            {
                if (manifestResourceStream == null)
                {
                    return string.Empty;
                }
                using (var streamReader = new StreamReader(manifestResourceStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}