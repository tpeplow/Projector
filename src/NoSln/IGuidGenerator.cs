using System;

namespace NoSln
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