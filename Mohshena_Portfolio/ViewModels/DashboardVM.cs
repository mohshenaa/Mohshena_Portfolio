using Mohshena_Portfolio.Models;

namespace Mohshena_Portfolio.ViewModels
{
    public class DashboardVM
    {
        public List<Project> Projects { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ContactMessage> ContactMessages { get; set; }
    }
}
