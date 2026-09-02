using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;
namespace TaskManagerApi.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<AppUser> Users => Set<AppUser>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var task = modelBuilder.Entity<TaskItem>();
        task.ToTable("Tasks");
        task.HasKey(t => t.Id);
        task.Property(t => t.Title).IsRequired().HasMaxLength(200)
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        task.Property(t => t.Description).HasMaxLength(2000);
        task.HasIndex(t => new { t.UserId, t.Id });
        task.HasOne<AppUser>().WithMany().HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        var user = modelBuilder.Entity<AppUser>();
        user.ToTable("Users");
        user.HasKey(u => u.Id);
        user.Property(u => u.Email).IsRequired().HasMaxLength(254);
        user.Property(u => u.NormalizedEmail).IsRequired().HasMaxLength(254);
        user.HasIndex(u => u.NormalizedEmail).IsUnique();
        user.Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
        user.Property(u => u.Role).IsRequired().HasMaxLength(32);
    }
}
