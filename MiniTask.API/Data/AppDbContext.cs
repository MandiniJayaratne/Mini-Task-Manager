using Microsoft.EntityFrameworkCore;
using MiniTask.API.Models;

namespace MiniTask.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tasks table
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
