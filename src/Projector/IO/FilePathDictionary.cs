using System.Collections.Generic;
using System.IO;

namespace Projector.IO
{
    public class FilePathDictionary<TItem>
    {
        readonly SortedList<string[], TItem> items = new SortedList<string[],TItem>(new PathComparer()); 
        
        public void Add(string path, TItem item)
        {
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var pathSegments = path.Split(Path.DirectorySeparatorChar);
            items.Add(pathSegments, item);
        }

        private class PathComparer : IComparer<string[]>
        {
            public int Compare(string[] x, string[] y)
            {
                if (x.Length > y.Length) return 1;
                if (x.Length == y.Length) return 0;
                return -1;
            }
        }
    }
}