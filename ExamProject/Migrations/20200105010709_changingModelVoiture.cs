using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamProject.Migrations
{
    public partial class changingModelVoiture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_locataires_LocataireId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Voiture_VoitureId",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "locations");

            migrationBuilder.RenameIndex(
                name: "IX_Location_VoitureId",
                table: "locations",
                newName: "IX_locations_VoitureId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_LocataireId",
                table: "locations",
                newName: "IX_locations_LocataireId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Voiture",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EstDisponible",
                table: "Voiture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                table: "locations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Voiture_ApplicationUserId",
                table: "Voiture",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_locations_locataires_LocataireId",
                table: "locations",
                column: "LocataireId",
                principalTable: "locataires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_locations_Voiture_VoitureId",
                table: "locations",
                column: "VoitureId",
                principalTable: "Voiture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voiture_AspNetUsers_ApplicationUserId",
                table: "Voiture",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locations_locataires_LocataireId",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "FK_locations_Voiture_VoitureId",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Voiture_AspNetUsers_ApplicationUserId",
                table: "Voiture");

            migrationBuilder.DropIndex(
                name: "IX_Voiture_ApplicationUserId",
                table: "Voiture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Voiture");

            migrationBuilder.DropColumn(
                name: "EstDisponible",
                table: "Voiture");

            migrationBuilder.RenameTable(
                name: "locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_locations_VoitureId",
                table: "Location",
                newName: "IX_Location_VoitureId");

            migrationBuilder.RenameIndex(
                name: "IX_locations_LocataireId",
                table: "Location",
                newName: "IX_Location_LocataireId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

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
    }
}
