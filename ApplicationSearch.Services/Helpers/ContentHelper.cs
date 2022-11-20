using System.Text.RegularExpressions;

namespace ApplicationSearch.Services.Helpers
{
    public static class ContentHelper
    {
        public static Dictionary<string, int> GetDuplicatePhrasesFromContent(string value, int limit = 0)
        {
            var duplicates = new Dictionary<string, int>();

            if (string.IsNullOrWhiteSpace(value))
                return duplicates;

            var words = value.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                int j = i;
                string s = words[i] + " ";

                while (i + j < words.Length)
                {
                    j++;
                    s += words[j] + " ";

                    try
                    {
                        var count = Regex.Matches(value.ToLower(), s).Count;

                        if (count < 2)
                            break;

                        duplicates[s] = count;
                    }
                    catch
                    {
                        //ignored
                    }
                }
            }

            duplicates = duplicates.Where(x => x.Key.Count(char.IsWhiteSpace) > 2).OrderByDescending(x => x.Value).ThenByDescending(x => x.Key.Count(char.IsWhiteSpace)).ToDictionary(x => x.Key, x => x.Value);

            return (limit <= 0) ? duplicates : duplicates.Take(limit).ToDictionary(x => x.Key, x => x.Value);
        }

        public static string GetStringWithoutHtml(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return Regex.Replace(value, "<.*?>", string.Empty);
        }

        public static string GetStringWithoutLineBreaks(string value, string lineBreakReplaceCharacter = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var breakingCharacters = new List<string>
            {
                Environment.NewLine,
                "<br />",
                "<br>"
            };

            foreach (var breakingCharacter in breakingCharacters)
                value = value.Replace(breakingCharacter, lineBreakReplaceCharacter);

            return value;
        }
    }
}
