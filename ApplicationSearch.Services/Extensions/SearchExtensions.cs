namespace ApplicationSearch.Services.Extensions
{
    public static class SearchExtensions
    {
        public static string ToLowerWithNoSpaces(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }


            return value.ToLower().Replace(" ", string.Empty);
        }

        public static List<string> ToIndividualWords(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new List<string>();
            }

            return value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static List<string> ToPyramidSearch(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains(' '))
            {
                return new List<string>();
            }

            var phrases = value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var queryPhrases = new List<string>();

            for (int i = 0; i <= phrases.Count - 1; i++)
            {
                if (i > 0)
                    queryPhrases.Add(queryPhrases[i - 1] + " " + phrases[i]);
                else
                    queryPhrases.Add(phrases[i]);
            }

            return queryPhrases.OrderByDescending(x => x).ToList();
        }
    }
}
