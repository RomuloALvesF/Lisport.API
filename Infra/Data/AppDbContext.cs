using Lisport.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Lisport.API.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
