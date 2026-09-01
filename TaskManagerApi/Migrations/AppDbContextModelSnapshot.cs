using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TaskManagerApi.Data;
namespace TaskManagerApi.Migrations;
[DbContext(typeof(AppDbContext))]
public class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.11")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);
        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);
        modelBuilder.Entity("TaskManagerApi.Models.TaskItem", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("int");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
            b.Property<string>("Title").IsRequired().HasMaxLength(200)
                .HasColumnType("nvarchar(200)").UseCollation("SQL_Latin1_General_CP1_CI_AS");
            b.Property<string>("Description").HasMaxLength(2000).HasColumnType("nvarchar(2000)");
            b.Property<bool>("IsCompleted").HasColumnType("bit");
            b.Property<DateTime>("CreatedAt").HasColumnType("datetime2");
            b.Property<DateTime?>("UpdatedAt").HasColumnType("datetime2");

            b.HasKey("Id");
            b.ToTable("Tasks");
        });
}
}
