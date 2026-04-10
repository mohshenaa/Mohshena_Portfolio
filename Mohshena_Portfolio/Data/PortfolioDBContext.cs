using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Models;

namespace Mohshena_Portfolio.Data
{
    public class PortfolioDBContext : IdentityDbContext
    {
        public PortfolioDBContext(DbContextOptions<PortfolioDBContext> options) : base(options)
        {
        }
        public PortfolioDBContext() 
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
