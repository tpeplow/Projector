using Projector.IO;

namespace Projector.Conventions
{
    public interface IModifyFileSystemConvention
    {
        void Update(IDirectory directory);
    }
}