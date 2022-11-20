namespace ApplicationSearch.Services.ViewModels
{
    public class PageViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string Url { get; set; } = string.Empty;

        public string Html { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
