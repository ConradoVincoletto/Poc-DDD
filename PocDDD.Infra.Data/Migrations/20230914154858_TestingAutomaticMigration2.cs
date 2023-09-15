using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocDDD.Infra.Data.Migrations
{
    public partial class TestingAutomaticMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutomaticMigrationTest2",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticMigrationTest2",
                table: "Users");
        }
    }
}
