using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamProject.Migrations
{
    public partial class changingModelLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_locataires_LocataireId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Voiture_VoitureId",
                table: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "VoitureId",
                table: "Location",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocataireId",
                table: "Location",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_locataires_LocataireId",
                table: "Location",
                column: "LocataireId",
                principalTable: "locataires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Voiture_VoitureId",
                table: "Location",
                column: "VoitureId",
                principalTable: "Voiture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_locataires_LocataireId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Voiture_VoitureId",
                table: "Location");

            migrationBuilder.AlterColumn<int>(
                name: "VoitureId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LocataireId",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Location_locataires_LocataireId",
                table: "Location",
                column: "LocataireId",
                principalTable: "locataires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Voiture_VoitureId",
                table: "Location",
                column: "VoitureId",
                principalTable: "Voiture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
