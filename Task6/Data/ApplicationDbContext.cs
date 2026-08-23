using Microsoft.EntityFrameworkCore;
using Task6.Data.Configurations;
using Task6.Models;

namespace Task6.Data
{
    public class ApplicationDbContext : DbContext
    {
        /* public DbSet<User> Users { get; set; }
         public DbSet<Task> Tasks { get; set; }
         public ApplicationDbContext(DbContextOptions options) : base(options) { }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<User>(entity =>
             {
                 entity.HasKey(t => t.Id);
                 entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
             });

             modelBuilder.Entity<Task>(entity =>
             {
                 entity.HasKey(t => t.Id);
                 entity.Property(t => t.Title).IsRequired().HasMaxLength(50);
                 entity.Property(t => t.Description).IsRequired().HasMaxLength(200);
                 entity.Property(t => t.CreatedAt).HasDefaultValueSql("GETDATE()");

                 entity.HasOne(t => t.User).WithMany(u => u.Tasks).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
             });

             base.OnModelCreating(modelBuilder);
         }
     }*/


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems => Set<TaskItem>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }

    }
}
