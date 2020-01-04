using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamProject.Migrations
{
    public partial class changingVoiture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voiture_Proprietaire_ProprietaireId",
                table: "Voiture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proprietaire",
                table: "Proprietaire");

            migrationBuilder.RenameTable(
                name: "Proprietaire",
                newName: "proprietaires");

            migrationBuilder.AlterColumn<int>(
                name: "ProprietaireId",
                table: "Voiture",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_proprietaires",
                table: "proprietaires",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "locataires",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locataires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocataireId = table.Column<int>(nullable: true),
                    VoitureId = table.Column<int>(nullable: true),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    PrixTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_locataires_LocataireId",
                        column: x => x.LocataireId,
                        principalTable: "locataires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Voiture_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "Voiture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocataireId",
                table: "Location",
                column: "LocataireId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_VoitureId",
                table: "Location",
                column: "VoitureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voiture_proprietaires_ProprietaireId",
                table: "Voiture",
                column: "ProprietaireId",
                principalTable: "proprietaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voiture_proprietaires_ProprietaireId",
                table: "Voiture");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "locataires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_proprietaires",
                table: "proprietaires");

            migrationBuilder.RenameTable(
                name: "proprietaires",
                newName: "Proprietaire");

            migrationBuilder.AlterColumn<int>(
                name: "ProprietaireId",
                table: "Voiture",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proprietaire",
                table: "Proprietaire",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voiture_Proprietaire_ProprietaireId",
                table: "Voiture",
                column: "ProprietaireId",
                principalTable: "Proprietaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
