using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamProject.Migrations
{
    public partial class locationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "locations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statut",
                table: "locations");
        }
    }
}
