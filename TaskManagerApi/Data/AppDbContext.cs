using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
namespace TaskManagerApi.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var task = modelBuilder.Entity<TaskItem>();
        task.ToTable("Tasks");
        task.HasKey(t => t.Id);
        task.Property(t => t.Title).IsRequired().HasMaxLength(200)
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        task.Property(t => t.Description).HasMaxLength(2000);
    }
}
