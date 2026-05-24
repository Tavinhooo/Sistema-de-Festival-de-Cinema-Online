using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjetoES.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPremiosVotacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    FestivalId = table.Column<int>(type: "integer", nullable: false),
                    DataLimiteVotacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premios_Festivais_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festivais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VotosPremio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PremioId = table.Column<int>(type: "integer", nullable: false),
                    FilmeId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    DataVoto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotosPremio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotosPremio_Filmes_FilmeId",
                        column: x => x.FilmeId,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotosPremio_Premios_PremioId",
                        column: x => x.PremioId,
                        principalTable: "Premios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotosPremio_Utilizadores_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Premios_FestivalId",
                table: "Premios",
                column: "FestivalId");

            migrationBuilder.CreateIndex(
                name: "IX_VotosPremio_ClienteId",
                table: "VotosPremio",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_VotosPremio_FilmeId",
                table: "VotosPremio",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_VotosPremio_PremioId_ClienteId",
                table: "VotosPremio",
                columns: new[] { "PremioId", "ClienteId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotosPremio");

            migrationBuilder.DropTable(
                name: "Premios");
        }
    }
}
