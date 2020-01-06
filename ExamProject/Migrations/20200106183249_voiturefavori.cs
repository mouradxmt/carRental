using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamProject.Migrations
{
    public partial class voiturefavori : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocataireId",
                table: "Voiture",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voiture_LocataireId",
                table: "Voiture",
                column: "LocataireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voiture_locataires_LocataireId",
                table: "Voiture",
                column: "LocataireId",
                principalTable: "locataires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voiture_locataires_LocataireId",
                table: "Voiture");

            migrationBuilder.DropIndex(
                name: "IX_Voiture_LocataireId",
                table: "Voiture");

            migrationBuilder.DropColumn(
                name: "LocataireId",
                table: "Voiture");
        }
    }
}
