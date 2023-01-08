namespace ApplicationSearch.Services.ViewModels
{
    public class ResultSummaryViewModel
    {
        public string Key { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string Url { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
    }
}
