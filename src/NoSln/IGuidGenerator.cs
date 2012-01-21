using System;

namespace Projector
{
    public interface IGuidGenerator
    {
        Guid Generate();
    }

    public class GuidGenerator : IGuidGenerator
    {
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}