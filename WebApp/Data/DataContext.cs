using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        public DbSet<SuperHero> superHeroes { get; set; }
    }
}