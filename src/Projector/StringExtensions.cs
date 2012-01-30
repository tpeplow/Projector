using System;
using System.Collections.Generic;
using System.Linq;

namespace Projector
{
    public static class StringExtensions
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

        public static TEnum ToEnum<TEnum>(this string value)
        {
            return (TEnum)Enum.Parse(typeof (TEnum), value);
        }

        public static bool ContainsIgnoreCase(this string lookIn, params string[] forAny)
        {
            return forAny.Any(x => lookIn.IndexOf(x, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }
    }
}