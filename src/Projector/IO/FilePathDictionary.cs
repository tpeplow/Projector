using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projector.IO
{
    public class FilePathDictionary<TItem>
    {
        readonly SortedList<string[], TItem> items = new SortedList<string[],TItem>(new PathComparer()); 
        
        public void Add(string path, TItem item)
        {
            var pathSegments = ToSegments(path);
            items.Add(pathSegments, item);
        }

        public TItem FindClosest(string path)
        {
            return FindAllInPath(path).FirstOrDefault();
        }

        public IEnumerable<TItem> FindAllInPath(string path)
        {
            var pathSegments = ToSegments(path);
            for (var i = items.Count - 1; i >= 0; i--)
            {
                if (IsSubPath(items.Keys[i], pathSegments))
                {
                    yield return items.Values[i];
                }
            }
        }

        static bool IsSubPath(IList<string> subPath, IList<string> fullPath)
        {
            if (subPath.Count > fullPath.Count) return false;
            for (var i = subPath.Count - 1; i >=0; i--)
            {
                if (fullPath[i] != subPath[i])
                {
                    return false;
                }
            }
            return true;
        }

        static string[] ToSegments(string path)
        {
            path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var pathSegments = path.Split(new [] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            return pathSegments;
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