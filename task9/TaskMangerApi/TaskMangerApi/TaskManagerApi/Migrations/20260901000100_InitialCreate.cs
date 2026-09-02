using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TaskManagerApi.Data;
namespace TaskManagerApi.Migrations;
[DbContext(typeof(AppDbContext))]
[Migration("20260901000100_InitialCreate")]
public class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    { 
        migrationBuilder.CreateTable(name: "Tasks", columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
            Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
            Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
            IsCompleted = table.Column<bool>(type: "bit", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Tasks", x => x.Id));
 }
    protected override void Down(MigrationBuilder migrationBuilder)
    { migrationBuilder.DropTable(name: "Tasks"); }
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    { 
        modelBuilder.HasAnnotation("ProductVersion", "8.0.24")
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

            b.HasKey("Id");
            b.ToTable("Tasks");
        });
 }
}
