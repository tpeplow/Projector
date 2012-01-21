using System.Text.RegularExpressions;

namespace Projector.OutputPipeline
{
    public interface IWildcardMatcher
    {
        bool IsMatch(string filePath, string wildcard);
    }

    public class WildcardMatcher : IWildcardMatcher
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