using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamProject.Migrations
{
    public partial class changingApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Locataire_LocataireId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Proprietaire_ProprietaireId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Locataire");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LocataireId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProprietaireId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "NomModel",
                table: "Models",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomMarque",
                table: "Marques",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProprietaireId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocataireId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomModel",
                table: "Models",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "NomMarque",
                table: "Marques",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ProprietaireId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LocataireId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Locataire",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locataire", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false),
                    LocataireId = table.Column<int>(nullable: true),
                    PrixTotal = table.Column<double>(nullable: false),
                    VoitureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Locataire_LocataireId",
                        column: x => x.LocataireId,
                        principalTable: "Locataire",
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
                name: "IX_AspNetUsers_LocataireId",
                table: "AspNetUsers",
                column: "LocataireId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProprietaireId",
                table: "AspNetUsers",
                column: "ProprietaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocataireId",
                table: "Location",
                column: "LocataireId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_VoitureId",
                table: "Location",
                column: "VoitureId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Locataire_LocataireId",
                table: "AspNetUsers",
                column: "LocataireId",
                principalTable: "Locataire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Proprietaire_ProprietaireId",
                table: "AspNetUsers",
                column: "ProprietaireId",
                principalTable: "Proprietaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
