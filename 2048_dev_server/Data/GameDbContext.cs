using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;

namespace _2048_dev_server.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Score> Scores { get; set; }
}
