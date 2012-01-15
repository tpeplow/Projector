using System;
using System.Collections.Generic;
using System.Linq;

namespace NoSln
{
    public static class StringExtenstions
    {
        public static IEnumerable<string> GetLines(this string s)
        {
            return s.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SkipEmptyOrCommentedLines(this IEnumerable<string> lines, char commentChar = '#')
        {
            return lines
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Where(x => !x.StartsWith("#"));
        }
    }
}