namespace ApplicationSearch.Services.ViewModels
{
    public class SiteViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public List<SiteSourceViewModel> Sources { get; set; } = new List<SiteSourceViewModel>();
    }
}
