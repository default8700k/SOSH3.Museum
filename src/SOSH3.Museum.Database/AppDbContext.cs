using Microsoft.EntityFrameworkCore;
using SOSH3.Museum.Database.Models;

namespace SOSH3.Museum.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<RequestDbModel> Requests { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
