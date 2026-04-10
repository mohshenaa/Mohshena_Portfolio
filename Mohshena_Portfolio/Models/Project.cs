using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mohshena_Portfolio.Models
{
   
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string GitHubLink { get; set; }

        public string LiveLink { get; set; }
    }
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 

    }
    public class ContactMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
