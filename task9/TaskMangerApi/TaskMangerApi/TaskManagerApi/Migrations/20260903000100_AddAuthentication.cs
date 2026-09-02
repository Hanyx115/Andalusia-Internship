using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TaskManagerApi.Data;

namespace TaskManagerApi.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260903000100_AddAuthentication")]
public class AddAuthentication : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                NormalizedEmail = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Role = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

        migrationBuilder.CreateIndex(
            name: "IX_Users_NormalizedEmail",
            table: "Users",
            column: "NormalizedEmail",
            unique: true);

        // Preserve existing tasks; never assign legacy rows to an arbitrary user.
        migrationBuilder.AddColumn<int>(
            name: "UserId", table: "Tasks", type: "int", nullable: true);
        migrationBuilder.CreateIndex(
            name: "IX_Tasks_UserId_Id", table: "Tasks", columns: new[] { "UserId", "Id" });
        migrationBuilder.AddForeignKey(
            name: "FK_Tasks_Users_UserId",
            table: "Tasks", column: "UserId",
            principalTable: "Users", principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(name: "FK_Tasks_Users_UserId", table: "Tasks");
        migrationBuilder.DropIndex(name: "IX_Tasks_UserId_Id", table: "Tasks");
        migrationBuilder.DropColumn(name: "UserId", table: "Tasks");
        migrationBuilder.DropTable(name: "Users");
    }

    protected override void BuildTargetModel(ModelBuilder modelBuilder) =>
        AuthenticationModelV1.Build(modelBuilder);
}
