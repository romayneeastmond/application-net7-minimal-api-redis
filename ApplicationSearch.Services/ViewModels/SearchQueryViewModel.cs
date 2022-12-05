namespace ApplicationSearch.Services.ViewModels
{
    public class SearchQueryViewModel
    {
        public Guid SiteId { get; set; }

        public string Query { get; set; } = string.Empty;
    }
}
