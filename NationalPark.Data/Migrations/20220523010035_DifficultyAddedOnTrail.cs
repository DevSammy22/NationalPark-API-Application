using Microsoft.EntityFrameworkCore.Migrations;

namespace NationalPark.Data.Migrations
{
    public partial class DifficultyAddedOnTrail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "Trails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Trails");
        }
    }
}
