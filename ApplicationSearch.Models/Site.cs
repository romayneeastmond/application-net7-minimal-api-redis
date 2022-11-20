using System.ComponentModel.DataAnnotations;

namespace ApplicationSearch.Models
{
    public class Site
    {
        [Key]
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}
