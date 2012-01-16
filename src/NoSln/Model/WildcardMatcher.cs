using System.Text.RegularExpressions;

namespace NoSln.Model
{
    public class WildcardMatcher
    {
        public bool IsMatch(string filePath, string wildcard)
        {
            var regex = new Regex(CreateExpressionFromWildcard(wildcard), RegexOptions.IgnoreCase);

            return regex.IsMatch(filePath);
        }

        private string CreateExpressionFromWildcard(string wildcard)
        {
            return Regex.Escape(wildcard)
                .Replace("\\?", ".")
                .Replace("\\*", @".+");
        }
    }
}