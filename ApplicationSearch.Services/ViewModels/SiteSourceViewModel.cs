namespace ApplicationSearch.Services.ViewModels
{
    public class SiteSourceViewModel
    {
        public Guid Id { get; set; }

        public Guid SiteId { get; set; }

        public string Url { get; set; } = string.Empty;

        public DateTime Created { get; set; }
    }
}
