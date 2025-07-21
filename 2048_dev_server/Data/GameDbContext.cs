using Microsoft.EntityFrameworkCore;
using _2048_dev_server.Models;

namespace _2048_dev_server.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<StageResult> StageResults { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // 테이블명 snake_case로 설정
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));

            // 각 컬럼명도 snake_case로 설정
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }
        }
    }

    private string ToSnakeCase(string input)
    {
        return string.Concat(
            input.Select((c, i) =>
                i > 0 && char.IsUpper(c)
                    ? "_" + char.ToLowerInvariant(c)
                    : char.ToLowerInvariant(c).ToString()
            )
        );
    }
}
