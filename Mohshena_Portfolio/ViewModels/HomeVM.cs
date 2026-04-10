using Mohshena_Portfolio.Models;

namespace Mohshena_Portfolio.ViewModels
{
    public class HomeVM
    {
        public List<Project> Projects { get; set; }
        public List<Skill> Skills { get; set; }
      
        public ContactMessage Contact { get; set; } = new ContactMessage();
    }
 
}
