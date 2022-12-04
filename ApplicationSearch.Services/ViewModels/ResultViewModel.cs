namespace ApplicationSearch.Services.ViewModels
{
    public class ResultViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string Url { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public List<ResultMetaViewModel> Meta { get; set; } = new List<ResultMetaViewModel>();

        public List<ResultHeadingViewModel> Headings { get; set; } = new List<ResultHeadingViewModel>();

        public List<ResultPhraseViewModel> DuplicatePhrases { get; set; } = new List<ResultPhraseViewModel>();

        public List<ResultLinkViewModel> Links { get; set; } = new List<ResultLinkViewModel>();

        public List<ResultLinkViewModel> OutboundLinks { get; set; } = new List<ResultLinkViewModel>();

        public List<string> UrlSegments { get; set; } = new List<string>();

        public List<string> QueryStrings { get; set; } = new List<string>();

        public List<string> Connections { get; set; } = new List<string>();

        public string Content { get; set; } = string.Empty;
    }

    public class ResultHeadingViewModel
    {
        public string Heading { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;
    }

    public class ResultLinkViewModel
    {
        public string Text { get; set; } = string.Empty;

        public string Href { get; set; } = string.Empty;
    }

    public class ResultMetaViewModel
    {
        public string Content { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }

    public class ResultPhraseViewModel
    {
        public string Phrase { get; set; } = string.Empty;

        public int Count { get; set; } = 0;
    }
}
