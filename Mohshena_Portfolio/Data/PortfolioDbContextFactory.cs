//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace Mohshena_Portfolio.Data
//{
//    public class PortfolioDbContextFactory:IDesignTimeDbContextFactory<PortfolioDBContext>
//    {
//        public PortfolioDBContext CreateDbContext(string[] args)
//        {
//            var opt = new DbContextOptionsBuilder<PortfolioDBContext>();
//            opt.UseSqlServer("server=DESKTOP-KG196VN\\SQLEXPRESS;Database=PortfolioDB;TrustServerCertificate=true;Trusted_Connection=true");
//            return new PortfolioDBContext(opt.Options);
//        }
//    }
//}




// it was done bcs i made mistake in program.cs that was preventing me to create migration