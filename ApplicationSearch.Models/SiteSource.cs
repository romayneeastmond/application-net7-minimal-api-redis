using System.ComponentModel.DataAnnotations;

namespace ApplicationSearch.Models
{
    public class SiteSource
    {
        [Key]
        public Guid Id { get; set; }

        public required Guid SiteId { get; set; }

        public required string Url { get; set; }

        public required DateTime Created { get; set; }
    }
}
