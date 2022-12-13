using System.ComponentModel.DataAnnotations;

namespace ApplicationSearch.Models
{
    public class Page
    {
        [Key]
        public Guid Id { get; set; }

        public required Guid SiteId { get; set; }

        public required string Url { get; set; }

        public string Html { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public required DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
