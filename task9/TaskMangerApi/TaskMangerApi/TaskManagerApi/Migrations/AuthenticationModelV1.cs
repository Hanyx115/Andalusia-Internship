using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Migrations;

// Frozen schema metadata for AddAuthentication. Never change this when adding future migrations.
internal static class AuthenticationModelV1
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.11")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);
        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("TaskManagerApi.Models.AppUser", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
            b.Property<string>("Email").IsRequired().HasMaxLength(254).HasColumnType("nvarchar(254)");
            b.Property<string>("NormalizedEmail").IsRequired().HasMaxLength(254).HasColumnType("nvarchar(254)");
            b.Property<string>("PasswordHash").IsRequired().HasMaxLength(100).HasColumnType("nvarchar(100)");
            b.Property<string>("Role").IsRequired().HasMaxLength(32).HasColumnType("nvarchar(32)");
            b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
            b.HasKey("Id");
            b.HasIndex("NormalizedEmail").IsUnique();
            b.ToTable("Users", (string?)null);
        });

        modelBuilder.Entity("TaskManagerApi.Models.TaskItem", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
            b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
            b.Property<string>("Description").HasMaxLength(2000).HasColumnType("nvarchar(2000)");
            b.Property<DateTime?>("DueDate").HasColumnType("datetime2");
            b.Property<bool>("IsCompleted").HasColumnType("bit");
            b.Property<string>("Title").IsRequired().HasMaxLength(200)
                .HasColumnType("nvarchar(200)").UseCollation("SQL_Latin1_General_CP1_CI_AS");
            b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");
            b.Property<int?>("UserId").HasColumnType("int");
            b.HasKey("Id");
            b.HasIndex("UserId", "Id");
            b.ToTable("Tasks", (string?)null);
        });

        modelBuilder.Entity("TaskManagerApi.Models.TaskItem", b =>
        {
            b.HasOne("TaskManagerApi.Models.AppUser", null)
                .WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);
        });
    }
}
